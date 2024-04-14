using KirokuG2.Internal.Loader.Test.Mocks;
using KirokuG2.Loader;

namespace KirokuG2.Internal.Loader.Test
{
    [TestClass]
    public class Index
    {
        [TestMethod]
        public void TestMethod1()
        {
            var logId = Guid.NewGuid().ToString();
            Dictionary<string, string> logs = new()
            {
                [logId] = ExampleKLogs.BasicLog()
            };

            Dictionary<string, string> logTracker = [];

            MockLogProvider mockLogProvider = new(logs, logTracker);

            Dictionary<string, string> sqlTracker = [];

            MockSQLProvider mockSQLProvider = new(sqlTracker);

            KLoaderManager.Configuration(mockLogProvider, mockSQLProvider);

            KLoaderManager.ProcessLogs();

            var test = 1;
        }
    }
}