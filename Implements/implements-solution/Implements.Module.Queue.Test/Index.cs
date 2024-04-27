using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Implements.Module.Queue.Test
{
	[TestClass]
	public class Index
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			var batch = new Batch();

			// TODO: method to auto create test test based on count
			var sample = new List<string>()
			{
				"test_1",
				"test_2",
				"test_3"
			};

			var tracker = new List<string>();

			var queue = new QueueManager(10, 1000, CreateTestAction(tracker, batch));

			foreach (var item in sample)
			{
				queue.Enqueue(item);

				await Task.Delay(2000);
			}

			await Task.Delay(10000);

			// TODO: new tracker object to help check sample-to-tracker data, but keep batch data intact
			var test2 = tracker.All(x => sample.Contains(x) == true);

			var test = 1;
		}

		private Action<List<object>> CreateTestAction(List<string> tracker, Batch batch)
		{
			return (List<object> objs) =>
			{
				var currentBatch = GetBatch(batch);
				foreach (var item in objs)
				{
					var str = item.ToString();
					tracker.Add($"{currentBatch}-{item}");
				}
			};
		}

		private int GetBatch(Batch batch)
		{
			return batch.Next();
		}
	}

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