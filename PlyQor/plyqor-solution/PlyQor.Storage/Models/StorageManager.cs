using PlyQor.Storage.Interfaces;
using PlyQor.Storage.ProtoPylon;

namespace PlyQor.Storage.Model
{
    public class StorageManager : IStorageManager
    {
        private readonly string _database;

        public StorageManager(IProtoPylon pylon)
        {
            pylon.GetConfiguration("storage").TryGetValue("user1234", out var key);

            _database = key;
        }

        public bool AddWindows(string container, List<int> windows)
        {
            throw new NotImplementedException();
        }

        public void BackupContainerConfig(Dictionary<string, Dictionary<string, string>> containerConfig)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Dictionary<string, string>> GetContainerConfigs()
        {
            throw new NotImplementedException();
        }

        public List<int> GetPartitionWindows(string container)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<string>> GetTokens()
        {
            throw new NotImplementedException();
        }

        public bool TruncateAndRemoveWindow(string container, List<int> windows)
        {
            throw new NotImplementedException();
        }

        public void UpsertContainerConfig(Dictionary<string, Dictionary<string, string>> containerConfig)
        {
            throw new NotImplementedException();
        }
    }
}
