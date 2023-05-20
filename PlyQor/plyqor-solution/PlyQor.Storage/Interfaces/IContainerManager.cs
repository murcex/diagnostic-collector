namespace PlyQor.Storage.Interfaces
{
    public interface IContainerManager
    {
        public Dictionary<string, string> QueryContainerConfiguration(string request);

        public void ExecuteContainerMaintenance();
    }
}
