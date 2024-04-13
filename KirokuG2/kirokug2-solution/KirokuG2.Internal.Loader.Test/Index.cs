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
            MockLogProvider mockLogProvider = new();

            MockSQLProvider mockSQLProvider = new();

            KLoaderManager.Configuration(mockLogProvider, mockSQLProvider);

            KLoaderManager.ProcessLogs();
        }
    }
}