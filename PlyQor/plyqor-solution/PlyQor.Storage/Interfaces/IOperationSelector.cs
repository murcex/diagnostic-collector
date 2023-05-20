using PlyQor.Storage.Enum;

namespace PlyQor.Storage.Interfaces
{
    public interface IOperationSelector
    {
        public void SetCurrentContainerConfiguration(Dictionary<string, Dictionary<string, string>> requestContainers);

        public Dictionary<string, ContainerOperation> GetContainerOperations(Dictionary<string, Dictionary<string, string>> requestContainers);

        //public string SelectContainerStatus(string container, Dictionary<string, string> settings);

        //public List<string> GetContainersToDelete();
    }
}
