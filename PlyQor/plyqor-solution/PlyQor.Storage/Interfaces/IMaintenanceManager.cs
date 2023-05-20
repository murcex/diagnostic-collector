namespace PlyQor.Storage.Interfaces
{
    public interface IMaintenanceManager
    {
        public bool Retention(Dictionary<string, int> containers);

        public bool PartitionCheck(Dictionary<string, int> containers);
    }
}
