using System.Collections.Concurrent;

namespace Implements.Module.Queue
{
	public class QueueManager
	{
		// --- queue configuration ---
		private readonly ConcurrentQueue<object> _queue;

		private readonly int _limit;

		private readonly int _duration;

		private readonly Action<List<object>> _action;

		private readonly Action<string> _logger;

		// --- state management ---
		private bool _active;

		private bool _processing;

		private CancellationTokenSource _token;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="limit"></param>
		/// <param name="duration"></param>
		/// <param name="action"></param>
		/// <param name="logger"></param>
		public QueueManager(int limit, int duration, Action<List<object>> action, Action<string>? logger = null)
		{
			_queue = new();
			_limit = limit;
			_duration = duration;
			_action = action;
			_logger = logger ?? ((_) => { });
			_active = false;
			_processing = false;
			_token = new();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool Enqueue(object obj)
		{
			_queue.Enqueue(obj);

			_logger($"t={DateTime.UtcNow},k=add_item,v={_queue.Count}");

			if (_active)
			{
				if (_queue.Count >= _limit)
				{
					_token.Cancel();

					var id = GetInstanceId();

					Task.Factory.StartNew(() => Trigger(id), TaskCreationOptions.None).ConfigureAwait(false);

					_logger($"t={DateTime.UtcNow},i={id},k=queue_limit_triggered,v={_queue.Count}");
				}
			}
			else
			{
				_token = new();

				var id = GetInstanceId();

				Task.Factory.StartNew(() => AsyncTrigger(id, _token.Token), TaskCreationOptions.LongRunning).ConfigureAwait(false);

				_active = true;

				_logger($"t={DateTime.UtcNow},i={id},k=active_async_trigger");
			}

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Close()
		{
			if (_active)
			{
				_token.Cancel();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		private async Task Trigger(string id)
		{
			await Task.Run(() => { ExecuteTrigger(id, TriggerType.Limit); });
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		private async Task AsyncTrigger(string id, CancellationToken token)
		{
			await Task.Delay(_duration, token).ContinueWith(_ => { ExecuteTrigger(id, TriggerType.Duration); }, token);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="type"></param>
		private void ExecuteTrigger(string id, TriggerType type)
		{
			_logger($"t={DateTime.UtcNow},i={id},k=execute_queue_processor,v={type}");

			if (_processing)
			{
				_logger($"t={DateTime.UtcNow},i={id},k=processor_status,v=locked");
				return;
			}
			else
			{
				_processing = true;
				_logger($"t={DateTime.UtcNow},i={id},k=processor_status,v=locking");
			}

			List<object> objs = new();

			while (_queue.Any())
			{
				if (_queue.TryDequeue(out var obj))
				{
					objs.Add(obj);
				}
			}

			_processing = false;

			_active = false;

			_logger($"t={DateTime.UtcNow},i={id},k=processor_status,v=released");

			_token = new();

			try
			{
				//Task.Run(() => { _action(clone); });

				_action(objs);

				_logger($"t={DateTime.UtcNow},i={id},k=action_stauts,v=completed");
				_logger($"t={DateTime.UtcNow},i={id},k=action_metadata,v={_queue.Count}/{objs.Count}");
			}
			catch (Exception ex)
			{
				var data = ex.ToString().Replace(",", "").Replace("=", "");

				_logger($"t={DateTime.UtcNow},i={id},k=action_stauts,v=exception");
				_logger($"t={DateTime.UtcNow},i={id},k=action_exception,v={data}");
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static string GetInstanceId()
		{
			return Guid.NewGuid().ToString().Split('-')[0].ToUpper();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	enum TriggerType
	{
		Limit,
		Duration
	}
}
