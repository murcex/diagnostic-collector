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
		private ConcurrentQueue _queue;

		private int _limit;

		private int _duration;

		private Action<object> _action;

		private Action<string> _report;

		public void Enqueue()
		{

		}

		public void Close()
		{

		}
	}
}
