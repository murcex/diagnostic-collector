using KirokuG2.Internal.Loader.Interface;

namespace KirokuG2.Internal.Loader.Test.Mocks
{
    public class MockLogProvider : ILogProvider
    {
        public MockLogProvider()
        {

        }

        public string Select(string id)
        {
            throw new NotImplementedException();
        }

        public List<string> Select(string tag, int top)
        {
            throw new NotImplementedException();
        }

        public void UpdateTag(string id, string tag, string newTag)
        {
            throw new NotImplementedException();
        }
    }
}
