namespace Implements.Module.Queue.Test
{
	public class Batch
	{
		private int current;

		public Batch()
		{
			current = 0;
		}

		public int Next()
		{
			return current++;
		}
	}
}
