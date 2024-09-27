using System.Collections.Concurrent;

namespace Implements.Module.Queue
{
	/// <summary>
	// This class represents a queue manager that allows enqueueing objects and executing actions on the enqueued objects based on certain triggers.
	/// </summary>
	public class QueueManager
	{
		// --- Queue Configuration ---
		// Represents the queue that stores the enqueued objects.
		private readonly ConcurrentQueue<object> _queue;

		// Represents the maximum number of items allowed in the queue.
		private readonly int _limit;

		// Represents the duration in milliseconds after which the queue processor will be triggered.
		private readonly int _duration;

		// Represents the action to be executed on the items in the queue.
		private readonly Action<List<object>> _action;

		// Represents the optional logger function to log messages.
		private readonly Action<string> _logger;

		// --- State Management ---
		// Represents the state of the queue manager indicating if it is active or not.
		private bool _active;

		// Represents the state of the queue manager indicating if it is currently processing items.
		private bool _processing;

		// Represents the cancellation token source used to cancel the queue processor.
		private CancellationTokenSource _token;

		/// <summary>
		/// Initializes a new instance of the QueueManager class.
		/// </summary>
		/// <param name="limit">The maximum number of items allowed in the queue.</param>
		/// <param name="duration">The duration in milliseconds after which the queue processor will be triggered.</param>
		/// <param name="action">The action to be executed on the items in the queue.</param>
		/// <param name="logger">The optional logger function to log messages.</param>
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
		/// Enqueues an object to the queue.
		/// </summary>
		/// <param name="obj">The object to enqueue.</param>
		/// <returns>True if the object was successfully enqueued, false otherwise.</returns>
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

				_logger($"t={DateTime.UtcNow},k=queue_async_triggered,v={id}");
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
		/// Executes the trigger for a specific ID.
		/// </summary>
		/// <param name="id">The ID of the trigger.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		private async Task Trigger(string id)
		{
			await Task.Run(() => { ExecuteTrigger(id, TriggerType.Limit); });
		}

		/// <summary>
		/// Asynchronously triggers the execution of the queue processor after a specified duration.
		/// </summary>
		/// <param name="id">The ID of the trigger.</param>
		/// <param name="token">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		private async Task AsyncTrigger(string id, CancellationToken token)
		{
			await Task.Delay(_duration, token).ContinueWith(_ => { ExecuteTrigger(id, TriggerType.Duration); }, token);
		}

		/// <summary>
		/// Executes the trigger for a specific ID.
		/// </summary>
		/// <param name="id">The ID of the trigger.</param>
		/// <param name="type">The type of the trigger.</param>
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

			_token = new();

			_logger($"t={DateTime.UtcNow},i={id},k=processor_status,v=unlocked");
			_logger($"t={DateTime.UtcNow},i={id},k=processor_action_count,v={objs.Count}");
			_logger($"t={DateTime.UtcNow},i={id},k=processor_queue_count,v={_queue.Count}");

			try
			{
				_logger($"t={DateTime.UtcNow},i={id},k=action_status,v=executing");

				//Task.Run(() => { _action(clone); });

				_action(objs);

				_logger($"t={DateTime.UtcNow},i={id},k=action_status,v=completed");
			}
			catch (Exception ex)
			{
				var data = ex.ToString().Replace(",", "").Replace("=", "");

				_logger($"t={DateTime.UtcNow},i={id},k=action_status,v=exception");
				_logger($"t={DateTime.UtcNow},i={id},k=action_exception,v={data}");
			}
		}

		/// <summary>
		/// Generates a unique instance ID.
		/// </summary>
		/// <returns>The generated instance ID.</returns>
		private static string GetInstanceId()
		{
			return Guid.NewGuid().ToString().Split('-')[0].ToUpper();
		}
	}

	/// <summary>
	/// Represents the type of trigger for the queue manager.
	/// </summary>
	enum TriggerType
	{
		Limit,
		Duration
	}
}
