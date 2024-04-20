using KirokuG2.Internal.Loader.Components;
using KirokuG2.Internal.Loader.Test.Data;
using KirokuG2.Internal.Loader.Test.Mocks;
using KirokuG2.Internal.Loader.Test.Models;
using KirokuG2.Loader;
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

            MockLogProvider mockLogProvider = new(logs, logTracker);

            Dictionary<string, string> sqlTracker = [];

            MockSQLProvider mockSQLProvider = new(sqlTracker);

            KLoaderManager.Configuration(mockLogProvider, mockSQLProvider);

            List<string> klogTracker = new();

            MockKLog mockKLog = new MockKLog(klogTracker);

            KLoaderManager.ProcessLogs(mockKLog);

            Assert.AreEqual(4, sqlTracker.Count);
            Assert.AreEqual(2, klogTracker.Count);

            var results = Utilities.GetResults(sqlTracker);

            var test = 1;
        }

        [TestMethod]
        public void ProcessLogs_MultiInstance()
        {
            var logId = Guid.NewGuid().ToString();
            var logId2 = Guid.NewGuid().ToString();
            Dictionary<string, string> logs = new()
            {
                [logId] = ExampleKLogs.BasicInstanceLog(),
                [logId2] = ExampleKLogs.MultiInstanceLog()
            };

            Dictionary<string, string> logTracker = [];

            MockLogProvider mockLogProvider = new(logs, logTracker);

            Dictionary<string, string> sqlTracker = [];

            MockSQLProvider mockSQLProvider = new(sqlTracker);

            KLoaderManager.Configuration(mockLogProvider, mockSQLProvider);

            List<string> klogTracker = new();

            MockKLog mockKLog = new MockKLog(klogTracker);

            KLoaderManager.ProcessLogs(mockKLog);

            var test = 1;
        }

        [TestMethod]
        public void Component_KLogSeralializer()
        {
            var seralializer = new KLogSeralializer();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("##multi-log-start");
            sb.AppendLine("#index=1");
            sb.AppendLine("test1");
            sb.AppendLine("test2");
            sb.AppendLine("test3");
            sb.AppendLine("#index=2");
            sb.AppendLine("test1");
            sb.AppendLine("##multi-log-end");


            var logSet = seralializer.DeseralizalizeLogSet(sb.ToString());

            var test = 1;
        }
    }
}