using PlyQor.Storage.Enum;
using PlyQor.Storage.Interfaces;

namespace PlyQor.Storage.Model
{
    public class Validator : IRequestValidator
    {
        private readonly Dictionary<string, List<string>> _tokens;

        public Validator(IStorageManager storageManager)
        {
            _tokens = storageManager.GetTokens();
        }

        public (bool result, string message, string code) ValidateToken(Dictionary<string, string> request)
        {
            var container = request["Container"];
            var token = request["Token"];

            if (_tokens.TryGetValue(container, out List<string> container_tokens))
            {
                if (container_tokens.Contains(token))
                {
                    return (true, "Token matched", "OK");
                }
                else
                {
                    return (false, "Token did not match", "ERR02");
                }
            }
            else
            {
                return (false, "Container not found", "ERR01");
            }
        }

        public RequestOperation ValidateOperation(Dictionary<string, string> request)
        {
            var operation = request["Operation"];

            if (string.IsNullOrEmpty(operation))
            {
                throw new ArgumentException("missing operation type");
            }

            if (string.Equals("getconfiguration", operation, StringComparison.CurrentCultureIgnoreCase))
            {
                return RequestOperation.GetConfiguration;
            }

            if (string.Equals("modifyconfiguration", operation, StringComparison.CurrentCultureIgnoreCase))
            {
                return RequestOperation.ModifyConfiguration;
            }

            throw new ArgumentException($"invalid operation type {operation}");
        }

        public (bool result, string message, string code) ValidateConfiguration(Dictionary<string, Dictionary<string, string>> containers)
        {
            foreach(var container in containers.Values)
            {
                if (container.ContainsKey("name"))
                {
                    var name = container["name"];

                    if (string.IsNullOrEmpty(name))
                    {
                        return (false, "Container name is null or empty", "ERR");
                    }

                    if (name.Length > 20) 
                    {
                        return (false, "Container name is grather than 20 char", "ERR");
                    }
                }
            }

            return (true, "", "");
        }
    }
}
