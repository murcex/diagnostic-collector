using KirokuG2.Internal.Loader.Test.Components;
using KirokuG2.Internal.Loader.Test.Data;

namespace KirokuG2.Internal.Loader.Test.Tests
{
    [TestClass]
	public class EdgeCases
	{
		[TestMethod]
		public void Failure_1_BlockMisMatch()
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

			Assert.AreEqual(1, sqlTracker.Count);
			Assert.AreEqual(3, klogTracker.Count);

			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Initialized", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Activation", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Block", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Critical", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Error", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Instance", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Metric", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Quarantine", 1));

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Quarantine", logId));

			Assert.IsTrue(klogTracker.Contains("Metric: kload_doc_cnt,1"));
			Assert.IsTrue(klogTracker.Contains("Metric: kload_log_cnt,1"));
			Assert.IsTrue(klogTracker.Any(x => x.Contains("Block not found during Stop Block dictionary lookup")));
		}

		// correctly just skips unknow types
		// TODO: should add error message, fail logs?
		[TestMethod]
		public void Failure_2_UnknownLogType()
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
		public void Failure_3_LineBreak()
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

			Assert.AreEqual(1, sqlTracker.Count);
			Assert.AreEqual(3, klogTracker.Count);

			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Initialized", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Activation", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Block", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Critical", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Error", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Instance", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Metric", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Quarantine", 1));

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Quarantine", logId));

			Assert.IsTrue(klogTracker.Contains("Metric: kload_doc_cnt,1"));
			Assert.IsTrue(klogTracker.Contains("Metric: kload_log_cnt,1"));
			Assert.IsTrue(klogTracker.Any(x => x.Contains("Index was outside the bounds of the array")));

			var test = 1;
		}

		// works, not failure
		// TODO: should not allow metric name to contain char other than letter, number, '-'?
		[TestMethod]
		public void Success_4_CommomInData()
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

			Assert.AreEqual(4, sqlTracker.Count);
			Assert.AreEqual(2, klogTracker.Count);

			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Initialized", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Activation", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Block", 1));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Critical", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Error", 1));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Instance", 1));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Metric", 1));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Quarantine", 0));

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Instance", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Block", logId, "TestBlock"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Error", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit", "tes,ting er,ror"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Metric", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit", "test me,tric", "99.99"));

			Assert.IsTrue(klogTracker.Contains("Metric: kload_doc_cnt,1"));
			Assert.IsTrue(klogTracker.Contains("Metric: kload_log_cnt,1"));
		}
	}
}
