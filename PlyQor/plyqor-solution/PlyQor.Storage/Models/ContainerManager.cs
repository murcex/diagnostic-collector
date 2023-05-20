using PlyQor.Storage.Interfaces;

namespace PlyQor.Storage.Model
{
    public class ContainerManager : IContainerManager
    {
        private const string data = "data";
        private readonly IRequestManager _requestManager;
        private readonly IRequestValidator _requestValidator;
        private readonly IOperationSelector _operationSelector;
        private readonly IContainerOperations _containerOperations;
        private readonly IStorageManager _storageManager;

        public ContainerManager(
            IRequestManager requestManager,
            IRequestValidator requestValidator,
            IOperationSelector operationSelector,
            IContainerOperations containerOperations,
            IStorageManager storageManager)
        {
            _requestManager = requestManager;
            _requestValidator = requestValidator;
            _operationSelector = operationSelector;
            _containerOperations = containerOperations;
            _storageManager = storageManager;
        }

        public Dictionary<string, string> QueryContainerConfiguration(string content)
        {
            var request = _requestManager.GetDictionaryFromString(content);

            _requestValidator.ValidateToken(request);

            var operation = _requestValidator.ValidateOperation(request);

            var currentContainConfig = _storageManager.GetContainerConfigs();

            if (operation == Enum.RequestOperation.GetConfiguration)
            {
                // create response
                // return currentContainerConfig
            }

            var containers = _requestManager.GetDictionariesFromDictionary(request, data);

            _requestValidator.ValidateConfiguration(containers);

            _operationSelector.SetCurrentContainerConfiguration(currentContainConfig);

            var containerStatus = _operationSelector.GetContainerOperations(containers);

            var upload = false;
            foreach (var container in containerStatus)
            {
                switch (container.Value)
                {
                    case Enum.ContainerOperation.NoOperation:
                        break;

                    case Enum.ContainerOperation.AddContainer:
                        _containerOperations.AddContainer(container.Key, containers[container.Key]);
                        upload = true;
                        break;

                    case Enum.ContainerOperation.UpdateContainer:
                        _containerOperations.UpdateContainer(container.Key, containers[container.Key]);
                        upload = true;
                        break;

                    case Enum.ContainerOperation.SoftDeleteContainer:
                        _containerOperations.DeleteContainer(container.Key, containers[container.Key]);
                        upload = true;
                        break;

                    case Enum.ContainerOperation.HardDeleteContainer:
                        _containerOperations.DeleteContainer(container.Key, containers[container.Key]);
                        break;

                    default:
                        break;
                }
            }

            if (upload)
            {
                _storageManager.UpsertContainerConfig(containers);

                _storageManager.BackupContainerConfig(currentContainConfig);
            }

            // create response
            return new Dictionary<string, string>();
        }

        public void ExecuteContainerMaintenance()
        {
            throw new NotImplementedException();
        }
    }
}
