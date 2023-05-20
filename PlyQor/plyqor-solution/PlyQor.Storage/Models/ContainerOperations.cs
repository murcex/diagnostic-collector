using PlyQor.Storage.Interfaces;

namespace PlyQor.Storage.Model
{
    public class ContainerOperations : IContainerOperations
    {
        private readonly IStorageManager _storageManager;

        public ContainerOperations(IStorageManager storageManager)
        {
            _storageManager = storageManager;
        }

        public Dictionary<string, string> AddContainer(string container, Dictionary<string, string> settings)
        {
            // add future windows if retention > 0
            var retentionRange = settings["retention"];

            int.TryParse(retentionRange, out var reantionValue);

            if (reantionValue > 0)
            {
                // partition manager => add windows
            }
            else
            {
                // partition manager => create default window
            }

            throw new NotImplementedException();
        }

        public Dictionary<string, string> UpdateContainer(string container, Dictionary<string, string> settings)
        {
            // 

            throw new NotImplementedException();
        }

        public Dictionary<string, string> DeleteContainer(string container)
        {
            // create container entry with tag to delete later by maintenance job

            throw new NotImplementedException();
        }
    }
}
