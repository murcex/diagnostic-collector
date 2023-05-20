using PlyQor.Storage.Interfaces;

namespace PlyQor.Test.Mock
{
    public class MockStorage : IStorageManager
    {
        private static Dictionary<string, Dictionary<string, string>> _containerConfigs;

        private static Dictionary<string, List<int>> _containerWindows;

        private static Dictionary<string, List<string>> _containerTokens;

        public MockStorage(Dictionary<string, List<int>> windows = null)
        {
            _containerConfigs = MockData.SetupContainersConfig();

            _containerWindows = windows == null ? MockData.SetupContainerWindows() : windows;

            _containerTokens = MockData.SetupContainerTokens();
        }

        public bool AddWindows(string container, List<int> windows)
        {
            var containerWindows = _containerWindows[container];

            foreach (var window in windows)
            {
                containerWindows.Add(window);
            }

            return true;
        }

        public Dictionary<string, Dictionary<string, string>> GetContainerConfigs()
        {
            return _containerConfigs;
        }

        public List<int> GetPartitionWindows(string container)
        {
            _containerWindows.TryGetValue(container, out var windows);

            return windows;
        }

        public Dictionary<string, List<string>> GetTokens()
        {
            return _containerTokens;
        }

        public bool TruncateAndRemoveWindow(string container, List<int> windows)
        {
            _containerWindows.TryGetValue(container, out var totalWindows);

            foreach (var window in windows)
            {
                totalWindows.Remove(window);
            }

            return true;
        }
    }
}
