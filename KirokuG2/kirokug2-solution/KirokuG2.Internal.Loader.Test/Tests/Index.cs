using KirokuG2.Internal.Loader.Components;
using KirokuG2.Internal.Loader.Test.Data;
using System.Text;

namespace KirokuG2.Internal.Loader.Test.Tests
{
	[TestClass]
	public class Index
	{
		[TestMethod]
		public void ProcessLogs_SingleInstance()
		{
			var logId = Guid.NewGuid().ToString();
			Dictionary<string, string> logs = new()
			{
				[logId] = ExampleKLogs.BasicInstanceLog()
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
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Error", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit", "testing error"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Metric", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit", "test metric", "99.99"));

			Assert.IsTrue(klogTracker.Contains("Metric: kload_doc_cnt,1"));
			Assert.IsTrue(klogTracker.Contains("Metric: kload_log_cnt,1"));
		}

		[TestMethod]
		public void ProcessLogs_MultiInstance()
		{
			var logId2 = Guid.NewGuid().ToString();
			Dictionary<string, string> logs = new()
			{
				[logId2] = ExampleKLogs.MultiInstanceLog()
			};

			Dictionary<string, string> logTracker = [];
			Dictionary<string, string> sqlTracker = [];
			List<string> klogTracker = [];

			Utilities.TestProcessor(logs, logTracker, sqlTracker, klogTracker);

			Assert.AreEqual(8, sqlTracker.Count);
			Assert.AreEqual(2, klogTracker.Count);

			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Initialized", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Activation", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Block", 2));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Critical", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Error", 2));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Instance", 2));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Metric", 2));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Quarantine", 0));

			var logIdIndex1 = $"{logId2}.1";
			var logIdIndex2 = $"{logId2}.2";

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Instance", logIdIndex1, "test-kiroku-injektr-wus3", "Kiroku-Audit"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Block", logIdIndex1, "TestBlock"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Error", logIdIndex1, "test-kiroku-injektr-wus3", "Kiroku-Audit", "testing error"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Metric", logIdIndex1, "test-kiroku-injektr-wus3", "Kiroku-Audit", "test metric", "99.99"));

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Instance", logIdIndex2, "test-kiroku-injektr-eus2", "Kiroku-Audit2"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Block", logIdIndex2, "TestBlock2"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Error", logIdIndex2, "test-kiroku-injektr-eus2", "Kiroku-Audit2", "testing error2"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Metric", logIdIndex2, "test-kiroku-injektr-eus2", "Kiroku-Audit2", "test metric2", "99.98"));

			Assert.IsTrue(klogTracker.Contains("Metric: kload_doc_cnt,1"));
			Assert.IsTrue(klogTracker.Contains("Metric: kload_log_cnt,2"));
		}

		[TestMethod]
		public void ProcessLogs_MixedInstance()
		{
			var logId = Guid.NewGuid().ToString();
			var logId2 = Guid.NewGuid().ToString();
			Dictionary<string, string> logs = new()
			{
				[logId] = ExampleKLogs.BasicInstanceLog(),
				[logId2] = ExampleKLogs.MultiInstanceLog()
			};
			Dictionary<string, string> logTracker = [];
			Dictionary<string, string> sqlTracker = [];
			List<string> klogTracker = [];

			Utilities.TestProcessor(logs, logTracker, sqlTracker, klogTracker);

			Assert.AreEqual(12, sqlTracker.Count);
			Assert.AreEqual(2, klogTracker.Count);

			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Initialized", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Activation", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Block", 3));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Critical", 0));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Error", 3));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Instance", 3));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Metric", 3));
			Assert.IsTrue(sqlTracker.CheckTrackerTypeCount("Quarantine", 0));

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Instance", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Block", logId, "TestBlock"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Error", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit", "testing error"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Metric", logId, "test-kiroku-injektr-wus3", "Kiroku-Audit", "test metric", "99.99"));

			var logIdIndex1 = $"{logId2}.1";
			var logIdIndex2 = $"{logId2}.2";

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Instance", logIdIndex1, "test-kiroku-injektr-wus3", "Kiroku-Audit"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Block", logIdIndex1, "TestBlock"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Error", logIdIndex1, "test-kiroku-injektr-wus3", "Kiroku-Audit", "testing error"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Metric", logIdIndex1, "test-kiroku-injektr-wus3", "Kiroku-Audit", "test metric", "99.99"));

			Assert.IsTrue(sqlTracker.CheckTrackerContains("Instance", logIdIndex2, "test-kiroku-injektr-eus2", "Kiroku-Audit2"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Block", logIdIndex2, "TestBlock2"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Error", logIdIndex2, "test-kiroku-injektr-eus2", "Kiroku-Audit2", "testing error2"));
			Assert.IsTrue(sqlTracker.CheckTrackerContains("Metric", logIdIndex2, "test-kiroku-injektr-eus2", "Kiroku-Audit2", "test metric2", "99.98"));

			Assert.IsTrue(klogTracker.Contains("Metric: kload_doc_cnt,2"));
			Assert.IsTrue(klogTracker.Contains("Metric: kload_log_cnt,3"));

			var test = 1;
		}

		[TestMethod]
		public void Component_KLogSeralializer()
		{
			var seralializer = new KLogSeralializer();

			StringBuilder sb = new();
			sb.AppendLine("##multi-log-start");
			sb.AppendLine("#index=1");
			sb.AppendLine("testline1");
			sb.AppendLine("testline2");
			sb.AppendLine("testline3");
			sb.AppendLine("#index=2");
			sb.AppendLine("testline4");
			sb.AppendLine("##multi-log-end");

			var logSet = seralializer.DeseralizalizeLogSet(sb.ToString());

			Assert.AreEqual(2, logSet.Keys.Count);

			Assert.IsTrue(logSet.CheckTrackContains("1", "testline1", "testline2", "testline3"));
			Assert.IsTrue(logSet.CheckTrackContains("2", "testline4"));
		}
	}
}