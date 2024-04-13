namespace KirokuG2.Internal.Loader.Interface
{
    public interface ILogProvider
    {
        string Select(string id);
        List<string> Select(string tag, int top);
        void UpdateTag(string id, string tag, string newTag);
    }
}