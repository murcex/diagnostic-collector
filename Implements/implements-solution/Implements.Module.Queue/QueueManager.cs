using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		private Action<string> _report;

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
				// no action required
			}
			else
			{
				_cancellationTokenSource = new CancellationTokenSource();

				Task.Factory.StartNew(() => AsyncTrigger(_cancellationTokenSource.Token), TaskCreationOptions.LongRunning).ConfigureAwait(false);
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

			_action(clone);
		}
	}
}
