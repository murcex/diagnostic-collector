using System.Collections.Concurrent;

namespace Implements.Module.Queue
{
	public class QueueManager
	{
		private ConcurrentQueue<object> _queue;

		private bool _active;

		private int _limit;

		private int _duration;

		private CancellationTokenSource _cancellationTokenSource;

		private Action<List<object>> _action;

		//private Action<string> _report;

		public QueueManager(int limit, int duration, Action<List<object>> action)
		{
			_limit = limit;
			_duration = duration;
			_action = action;
			_queue = new ConcurrentQueue<object>();
		}

		public void Enqueue(object obj)
		{
			_queue.Enqueue(obj);

			if (_active)
			{
				if (_queue.Count >= _limit)
				{
					_cancellationTokenSource.Cancel();
				}
			}
			else
			{
				_cancellationTokenSource = new CancellationTokenSource();

				Task.Factory.StartNew(() => AsyncTrigger(_cancellationTokenSource.Token), TaskCreationOptions.LongRunning).ConfigureAwait(false);

				_active = true;
			}
		}

		public void Close()
		{
			if (_active)
			{
				_cancellationTokenSource.Cancel();
			}
		}

		private async Task AsyncTrigger(CancellationToken token)
		{
			await Task.Delay(_duration, token).ContinueWith(_ => { ExecuteTrigger(); }, token);
		}

		private void ExecuteTrigger()
		{
			List<object> clone = new();

			while (_queue.Any())
			{
				if (_queue.TryDequeue(out var obj))
				{
					clone.Add(obj);
				}
			}

			_active = false;

			_cancellationTokenSource = new CancellationTokenSource();

			try
			{
				_action(clone);
			}
			catch (Exception ex)
			{
				// swallow? log?
			}
		}
	}
}
