using KirokuG2.Internal.Loader.Interface;
using PlyQor.Client;

namespace KirokuG2.Internal.Loader.Components
{
    public class LogProvider : ILogProvider
    {
        private static PlyClient _plyClient;

        public LogProvider(PlyClient plyClient)
        {
            _plyClient = plyClient;
        }

        public List<string> Select(string tag, int top)
        {
            return _plyClient.Select(tag, top).GetPlyList();
        }

        public string Select(string id)
        {
            return _plyClient.Select(id).GetPlyData();
        }

        public void UpdateTag(string id, string tag, string newTag)
        {
            _plyClient.UpdateTag(id, tag, newTag);
        }
    }
}
