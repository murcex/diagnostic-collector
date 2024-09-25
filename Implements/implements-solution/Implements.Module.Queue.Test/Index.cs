using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Implements.Module.Queue.Test
{
	[TestClass]
	public class Index
	{
		//[TestMethod]
		//[DynamicData(nameof(TestConfigurations.GetTestDataOne), typeof(TestConfigurations), DynamicDataSourceType.Method)]
		//public async Task QueueCoreTest(QueueTestConfig config)
		//{
		//    var results = await InternalExecutorAsync(config);

		//    Assert.IsTrue(Utilities.CheckTrackerContains(results.samples, results.objTracker));

		//    var records = QueueLoglizer.Execute(results.logTracker);

		//    var test = 1;
		//}

		/// <summary>
		/// This test checks the behavior of the queue when a single duration is specified.
		/// </summary>
		[TestMethod]
		public async Task Duration_Single()
		{
			var cfg = new QueueTestConfig(10, 5000, 5, 10, 10000);

			var results = await InternalExecutorAsync(cfg);

			Assert.IsTrue(Utilities.CheckTrackerContains(results.samples, results.objTracker));

			//var records = QueueLoglizer.Execute(results.logTracker);
		}

		[TestMethod]
		public async Task Duration_Multi()
		{
			var cfg = new QueueTestConfig(10, 5500, 1000, 10, 10000);

			var results = await InternalExecutorAsync(cfg);

			Assert.IsTrue(Utilities.CheckTrackerContains(results.samples, results.objTracker));

			//var records = QueueLoglizer.Execute(results.logTracker);
		}

		[TestMethod]
		public async Task Limit_Single()
		{
			var cfg = new QueueTestConfig(5, 1000, 9, 10, 10000);

			var results = await InternalExecutorAsync(cfg);

			Assert.IsTrue(Utilities.CheckTrackerContains(results.samples, results.objTracker));

			//var records = QueueLoglizer.Execute(results.logTracker);
		}

		[TestMethod]
		public async Task Limit_Multi()
		{
			var cfg = new QueueTestConfig(5, 1000, 11, 10, 10000);

			var results = await InternalExecutorAsync(cfg);

			Assert.IsTrue(Utilities.CheckTrackerContains(results.samples, results.objTracker));

			var records = QueueLoglizer.Execute(results.logTracker);
		}

		[TestMethod]
		public async Task Stress_Test()
		{
			var cfg = new QueueTestConfig(100, 5000, 1000, 10, 10000);

			var results = await InternalExecutorAsync(cfg);

			Assert.IsTrue(Utilities.CheckTrackerContains(results.samples, results.objTracker));

			var records = QueueLoglizer.Execute(results.logTracker);
		}

		[TestMethod]
		public async Task Exception_empty()
		{
			// Arrange
			var cfg = new QueueTestConfig(0, 0, 0, 0, 0);

			// Act
			var exceptionThrown = false;
			try
			{
				await InternalExecutorAsync(cfg);
			}
			catch
			{
				exceptionThrown = true;
			}

			// Assert
			Assert.IsTrue(exceptionThrown);
		}

		[TestMethod]
		public async Task Close_empty()
		{
		}


		/// <summary>
		/// Executes the queue test asynchronously.
		/// </summary>
		/// <param name="config">The configuration for the queue test.</param>
		/// <returns>A tuple containing the generated samples, object tracker, and log tracker.</returns>
		private async Task<(List<string> samples, List<string> objTracker, List<string> logTracker)> InternalExecutorAsync(QueueTestConfig config)
		{
			var samples = Utilities.SampleGenerator(config.SampleSize);

			var objTracker = new List<string>();
			var batch = new Batch();
			var logTracker = new List<string>();

			var queue = new QueueManager(config.Limit, config.Duration, Utilities.CreateTestAction(objTracker, batch), Utilities.CreateTestLogger(logTracker));

			foreach (var sample in samples)
			{
				queue.Enqueue(sample);

				await Task.Delay(config.EnqueueDelay);
			}

			await Task.Delay(config.DrainDelay);

			return (samples, objTracker, logTracker);
		}
	}
}