namespace PlyQor.Storage.Interfaces
{
    public interface IPartitionManager
    {
        public List<int> FutureWindows();

        public int CutOffWindow(int retentionRange);

        public List<int> FindMissingWindows(List<int> windows, List<int> futureWindows);

        public List<int> SelectRetentionWindows(List<int> partitions, int cutoff);
    }
}
