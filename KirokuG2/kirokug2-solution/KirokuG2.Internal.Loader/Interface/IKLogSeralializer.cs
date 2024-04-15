namespace KirokuG2.Internal.Loader.Interface
{
    public interface IKLogSeralializer
    {
        Dictionary<string, (List<string> logs, string index)> DeseralizalizeLogSet(string id, string rawLog);
    }
}