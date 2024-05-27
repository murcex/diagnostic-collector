namespace Implements.Module.Queue.Test
{
	public class TestConfigurations
	{
		public static IEnumerable<object[]> GetTestDataOne()
		{
			yield return new[] { new QueueTestConfig(10, 1000, 5, 10, 10000) };
			yield return new[] { new QueueTestConfig(10, 1000, 11, 10, 10000) };
			yield return new[] { new QueueTestConfig(10, 1000, 10, 10, 10000) };
			yield return new[] { new QueueTestConfig(2, 1000, 10, 10, 10000) };
		}
	}
}
