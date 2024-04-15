using KirokuG2.Internal.Loader.Components;
using KirokuG2.Internal.Loader.Test.Data;
using KirokuG2.Internal.Loader.Test.Mocks;
using KirokuG2.Loader;
using System.Text;

namespace KirokuG2.Internal.Loader.Test
{
    [TestClass]
    public class Index
    {
        [TestMethod]
        public void BasicLog()
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
        
        [TestMethod]
        public void KLogSeralializer()
        {
            var seralializer = new KLogSeralializer();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("###start");
            sb.AppendLine("##1");
            sb.AppendLine("test1");
            sb.AppendLine("test2");
            sb.AppendLine("test3");
            sb.AppendLine("##2");
            sb.AppendLine("test1");
            sb.AppendLine("###end");


            var logSet = seralializer.DeseralizalizeLogSet("1234", sb.ToString());

            var test = 1;
        }
    }
}