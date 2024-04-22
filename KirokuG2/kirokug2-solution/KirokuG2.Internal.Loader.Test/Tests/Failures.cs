using KirokuG2.Internal.Loader.Test.Data;

namespace KirokuG2.Internal.Loader.Test.Tests
{
	[TestClass]
	public class Failures
	{
		[TestMethod]
		public void Failure1()
		{
			var logId = Guid.NewGuid().ToString();
			Dictionary<string, string> logs = new()
			{
				[logId] = ExampleKLogs.InstanceLogFailure_1()
			};
			Dictionary<string, string> logTracker = [];
			Dictionary<string, string> sqlTracker = [];
			List<string> klogTracker = [];

			Utilities.TestProcessor(logs, logTracker, sqlTracker, klogTracker);
		}

		[TestMethod]
		public void Failure2()
		{
			var logId = Guid.NewGuid().ToString();
			Dictionary<string, string> logs = new()
			{
				[logId] = ExampleKLogs.InstanceLogFailure_2()
			};
			Dictionary<string, string> logTracker = [];
			Dictionary<string, string> sqlTracker = [];
			List<string> klogTracker = [];

			Utilities.TestProcessor(logs, logTracker, sqlTracker, klogTracker);
		}

		[TestMethod]
		public void Failure3()
		{
			var logId = Guid.NewGuid().ToString();
			Dictionary<string, string> logs = new()
			{
				[logId] = ExampleKLogs.InstanceLogFailure_3()
			};
			Dictionary<string, string> logTracker = [];
			Dictionary<string, string> sqlTracker = [];
			List<string> klogTracker = [];

			Utilities.TestProcessor(logs, logTracker, sqlTracker, klogTracker);
		}

		[TestMethod]
		public void Failure4()
		{
			var logId = Guid.NewGuid().ToString();
			Dictionary<string, string> logs = new()
			{
				[logId] = ExampleKLogs.InstanceLogFailure_4()
			};
			Dictionary<string, string> logTracker = [];
			Dictionary<string, string> sqlTracker = [];
			List<string> klogTracker = [];

			Utilities.TestProcessor(logs, logTracker, sqlTracker, klogTracker);
		}
	}
}
