namespace KirokuG2.Internal.Loader.Interface
{
    public interface ILogProvider
    {
        string Select(string id);
        List<string> GetLogIds(string tag, int top);
        (string id, Dictionary<string, List<string>> logs) GetLogsById(string id);
        void UpdateTag(string id, string tag, string newTag);
    }
}