namespace Implements.Module.Queue.Test
{
	public class Utilities
	{
		public static List<string> SampleGenerator(int count)
		{
			List<string> samples = new();
			int sample = 0;
			while (sample < count)
			{
				samples.Add($"test_{sample}");
				sample++;
			}

			return samples;
		}

		public static Action<string> CreateTestLogger(List<string> tracker)
		{
			return (string msg) =>
			{
				tracker.Add(msg);
			};
		}

		public static Action<List<object>> CreateTestAction(List<string> tracker, Batch batch)
		{
			return (List<object> objs) =>
			{
				var currentBatch = GetBatch(batch);
				foreach (var item in objs)
				{
					var str = item.ToString();
					tracker.Add($"B{currentBatch}-{item}");
				}
			};
		}

		private static int GetBatch(Batch batch)
		{
			return batch.Next();
		}

		public static bool CheckTrackerContains(List<string> input, List<string> output)
		{
			return output.All(x => input.Contains(x.Split("-")[1]) == true);
		}

		public static async Task EnqueueAsync(QueueManager queue, string sample, int delay)
		{
			await Task.Delay(delay);

			queue.Enqueue(sample);
		}
	}
}
