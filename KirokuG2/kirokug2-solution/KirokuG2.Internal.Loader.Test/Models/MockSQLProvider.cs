using KirokuG2.Internal.Loader.Interface;
using KirokuG2.Loader;

namespace KirokuG2.Internal.Loader.Test.Mocks
{
    public class MockSQLProvider : ISQLProvider
    {
        public MockSQLProvider()
        {

        }

        public bool Initialized(string sqlConnection)
        {
            throw new NotImplementedException();
        }

        public bool InsertActivation(DateTime session, string record_id, string source)
        {
            throw new NotImplementedException();
        }

        public bool InsertBlock(LogBlock logBlock)
        {
            throw new NotImplementedException();
        }

        public bool InsertCritical(LogError logError)
        {
            throw new NotImplementedException();
        }

        public bool InsertError(LogError logError)
        {
            throw new NotImplementedException();
        }

        public bool InsertInstance(LogInstance logInstance)
        {
            throw new NotImplementedException();
        }

        public bool InsertMetric(LogMetric logMetric)
        {
            throw new NotImplementedException();
        }

        public bool InsertQuarantine(DateTime session, string record_id)
        {
            throw new NotImplementedException();
        }
    }
}
