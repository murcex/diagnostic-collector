namespace PlyQor.Storage.Interfaces
{
    public interface IContainerOperations
    {
        public Dictionary<string, string> AddContainer(string container, Dictionary<string, string> settings);

        public Dictionary<string, string> UpdateContainer(string container, Dictionary<string, string> settings);

        public Dictionary<string, string> DeleteContainer(string container);
    }
}
