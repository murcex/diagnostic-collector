namespace PlyQor.Storage.Interfaces
{
    public interface IStorageManager
    {
        public List<int> GetPartitionWindows(string container);

        public bool AddWindows(string container, List<int> windows);

        public bool TruncateAndRemoveWindow(string container, List<int> windows);

        public Dictionary<string, Dictionary<string, string>> GetContainerConfigs();

        public Dictionary<string, List<string>> GetTokens();

        public void UpsertContainerConfig(Dictionary<string, Dictionary<string, string>> containerConfig);

        public void BackupContainerConfig(Dictionary<string, Dictionary<string, string>> containerConfig);
    }
}
