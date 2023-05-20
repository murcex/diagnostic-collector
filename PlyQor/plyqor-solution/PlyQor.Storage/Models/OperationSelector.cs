using Newtonsoft.Json;
using PlyQor.Storage.Enum;
using PlyQor.Storage.Interfaces;

namespace PlyQor.Storage.Models
{
    public class OperationSelector : IOperationSelector
    {
        private Dictionary<string, Dictionary<string, string>>? _containers;

        private List<string>? _deleteContainer;

        public void SetCurrentContainerConfiguration(Dictionary<string, Dictionary<string, string>> requestContainers)
        {
            _containers = requestContainers;

            _deleteContainer = requestContainers.Keys.ToList();
        }

        private bool CheckTokens(string currentValue, string newValue)
        {
            var currentTokens = JsonConvert.DeserializeObject<List<string>>(currentValue);

            var newTokens = JsonConvert.DeserializeObject<List<string>>(newValue);

            if (currentTokens.Count != newTokens.Count)
            {
                return true;
            }

            foreach (var currentToken in currentTokens)
            {
                if (!newTokens.Contains(currentToken))
                {
                    return true;
                }
            }

            return false;
        }

        public Dictionary<string, ContainerOperation> GetContainerOperations(Dictionary<string, Dictionary<string, string>> containers)
        {
            Dictionary<string, ContainerOperation> containerOperations = new();

            foreach (var container in containers)
            {
                var containerName = container.Key;

                var settings = container.Value;

                var modified = false;

                _deleteContainer.Remove(containerName);

                var currentSettings = _containers[containerName];

                foreach (var settingKey in settings.Keys)
                {
                    var currentSettingValue = currentSettings[settingKey];

                    var newSettingValue = settings[settingKey];

                    switch (settingKey)
                    {
                        case "name":
                            if (!string.Equals(currentSettingValue, newSettingValue, StringComparison.OrdinalIgnoreCase))
                            {
                                modified = true;
                            }

                            break;

                        case "retention":
                            if (!string.Equals(currentSettingValue, newSettingValue, StringComparison.OrdinalIgnoreCase))
                            {
                                int.TryParse(currentSettingValue, out var currentRetentionRange);

                                int.TryParse(newSettingValue, out var newRetentionRange);

                                if (currentRetentionRange == 0)
                                {
                                    throw new InvalidOperationException("currentRetentionRange == 0");
                                }

                                if (newRetentionRange < 1 && currentRetentionRange > 0)
                                {
                                    throw new InvalidOperationException("newRetentionRange < 1 && currentRetentionRange > 0");
                                }

                                if (currentRetentionRange > 0 && newRetentionRange > 0)
                                {
                                    modified = true;
                                }
                            }

                            break;

                        case "tokens":
                            if (CheckTokens(currentSettingValue, newSettingValue))
                            {
                                modified = true;
                            }

                            break;

                        default:
                            throw new ArgumentException();
                    }
                }

                if (modified)
                {
                    containerOperations.Add(containerName, ContainerOperation.UpdateContainer);
                }
                else
                {
                    containerOperations.Add(containerName, ContainerOperation.NoOperation);
                }
            }

            foreach (var deleteContainer in _deleteContainer)
            {
                containerOperations.Add(deleteContainer, ContainerOperation.SoftDeleteContainer);
            }

            return containerOperations;
        }
    }
}
