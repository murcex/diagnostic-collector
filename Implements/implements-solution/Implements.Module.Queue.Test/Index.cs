using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Implements.Module.Queue.Test
{
	[TestClass]
	public class Index
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			var queue = new QueueManager(10, 5000, TestFunc);

			queue.Enqueue("testing1");
			queue.Enqueue("testing2");
			queue.Enqueue("testing3");

			await Task.Delay(10000);

			var test = 1;
		}

		// TODO: add func to create func with embedded list for tracking test results

		private void TestFunc(List<object> obj)
		{
			foreach (var item in obj)
			{
				var item2 = (string)item;

				Console.WriteLine(item2);
			}
		}
	}
}