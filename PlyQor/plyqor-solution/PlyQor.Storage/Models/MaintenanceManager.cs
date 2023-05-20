using PlyQor.Storage.Interfaces;

namespace PlyQor.Storage.Model
{
    public class MaintenanceManager : IMaintenanceManager
    {
        private IStorageManager _storageManager;

        private IPartitionManager _partitionManager;

        public MaintenanceManager(IStorageManager storageManager, IPartitionManager partitionManger)
        {
            _storageManager = storageManager;
            _partitionManager = partitionManger;
        }

        public bool PartitionCheck(Dictionary<string, int> containers)
        {
            foreach (var container in containers)
            {
                if (container.Value > 0)
                {
                    var windows = _storageManager.GetPartitionWindows(container.Key);

                    var futureWindows = _partitionManager.FutureWindows();

                    var missingWindows = _partitionManager.FindMissingWindows(windows, futureWindows);

                    var result = _storageManager.AddWindows(container.Key, missingWindows);
                }
            }

            return true;
        }

        public bool Retention(Dictionary<string, int> containers)
        {
            foreach (var container in containers)
            {
                if (container.Value > 0)
                {
                    var partitions = _storageManager.GetPartitionWindows(container.Key);

                    var cutoff = _partitionManager.CutOffWindow(container.Value);

                    var windows = _partitionManager.SelectRetentionWindows(partitions, cutoff);

                    var result = _storageManager.TruncateAndRemoveWindow(container.Key, windows);
                }
            }

            return true;
        }
    }
}
