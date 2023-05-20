using PlyQor.Storage.Enum;

namespace PlyQor.Storage.Interfaces
{
    public interface IRequestValidator
    {
        public (bool result, string message, string code) ValidateToken(Dictionary<string, string> request);

        public RequestOperation ValidateOperation(Dictionary<string, string> request);

        public (bool result, string message, string code) ValidateConfiguration(Dictionary<string, Dictionary<string, string>> containers);
    }
}
