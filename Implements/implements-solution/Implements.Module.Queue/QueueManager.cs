using System.Collections.Concurrent;

namespace Implements.Module.Queue
{
	public class QueueManager
	{
		private ConcurrentQueue<object> _queue;

		private bool _active;

		private bool _processing;

		private int _limit;

		private int _duration;

		private CancellationTokenSource _cancellationTokenSource;

		private Action<List<object>> _action;

		private Action<string> _logger;

		public QueueManager(int limit, int duration, Action<List<object>> action, Action<string> logger = null)
		{
			_active = false;
			_cancellationTokenSource = new CancellationTokenSource();
			_limit = limit;
			_duration = duration;
			_action = action;
			_queue = new ConcurrentQueue<object>();
			_logger = logger == null ? (_) => { } : logger;
		}

		public bool Enqueue(object obj)
		{
			_queue.Enqueue(obj);

			_logger($"Adding Item Queue - Count: {_queue.Count}");

			if (_active)
			{
				if (_queue.Count >= _limit)
				{
					_cancellationTokenSource.Cancel();

					var id = GetInstanceId();

					_logger($"{id} Queue Limit Triggered - Count: {_queue.Count}");

					Task.Factory.StartNew(() => Trigger(id), TaskCreationOptions.None).ConfigureAwait(false);
				}
			}
			else
			{
				_cancellationTokenSource = new CancellationTokenSource();

				var id = GetInstanceId();

				Task.Factory.StartNew(() => AsyncTrigger(id, _cancellationTokenSource.Token), TaskCreationOptions.LongRunning).ConfigureAwait(false);

				_active = true;

				_logger($"{id} Activating Async Trigger");
			}

			return true;
		}

		public void Close()
		{
			if (_active)
			{
				_cancellationTokenSource.Cancel();
			}
		}

		private async Task Trigger(string id)
		{
			await Task.Run(() => { ExecuteTrigger(id); });
		}

		private async Task AsyncTrigger(string id, CancellationToken token)
		{
			await Task.Delay(_duration, token).ContinueWith(_ => { ExecuteTrigger(id); });
		}

		private void ExecuteTrigger(string id)
		{
			_logger($"{id} Queue Triggered");

			if (_processing)
			{
				_logger($"{id} Processor Locked");
				return;
			}
			else
			{
				_processing = true;
				_logger($"{id} Locking Processor");
			}

			List<object> clone = new();

			while (_queue.Any())
			{
				if (_queue.TryDequeue(out var obj))
				{
					clone.Add(obj);
				}
			}

			_processing = false;

			_active = false;

			_logger($"{id} Processor Released - Count: {_queue.Count}");

			_cancellationTokenSource = new CancellationTokenSource();

			try
			{
				_action(clone);

				_logger($"{id} Action Completed");
			}
			catch (Exception ex)
			{
				_logger($"{id} Trigger Action Exception: {ex}");
			}
		}

		private static string GetInstanceId()
		{
			return Guid.NewGuid().ToString().Split('-')[0].ToUpper();
		}
	}
}
