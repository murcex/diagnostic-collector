using PlyQor.Storage.Interfaces;

namespace PlyQor.Storage.Test
{
    public class MockStorage : IStorageManager
    {
        public bool AddWindows(List<string> windows)
        {
            throw new NotImplementedException();
        }

        public bool GetContainerConfigs()
        {
            throw new NotImplementedException();
        }

        public List<string> GetPartitionWindows(string container)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<string>> GetTokens()
        {
            Dictionary<string, List<string>> tokens = new();

            tokens["TestContainer"] = new List<string>() { "password1234" };

            return tokens;
        }

        public bool TruncateAndRemoveWindow(List<string> windows)
        {
            throw new NotImplementedException();
        }
    }
}
