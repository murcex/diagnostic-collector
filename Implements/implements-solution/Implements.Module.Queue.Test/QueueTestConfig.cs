namespace Implements.Module.Queue.Test
{
	public class QueueTestConfig
	{
		public int Limit { get; private set; }

		public int Duration { get; private set; }

		public int SampleSize { get; private set; }

		public int EnqueueDelay { get; private set; }

		public int DrainDelay { get; private set; }

		public QueueTestConfig(int limit, int duration, int sampleSize, int enqueueDelay, int drainDelay)
		{
			Limit = limit;
			Duration = duration;
			SampleSize = sampleSize;
			EnqueueDelay = enqueueDelay;
			DrainDelay = drainDelay;
		}
	}
}
