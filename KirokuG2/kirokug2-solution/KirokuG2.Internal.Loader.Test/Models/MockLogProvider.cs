using KirokuG2.Internal.Loader.Interface;

namespace KirokuG2.Internal.Loader.Test.Mocks
{
    public class MockLogProvider : ILogProvider
    {
        private Dictionary<string, string> _logs;

        private Dictionary<string, string> _tracker;

        public MockLogProvider(Dictionary<string, string> logs, Dictionary<string, string> tracker)
        {
            _logs = logs;
            _tracker = tracker;
        }

        public string Select(string id)
        {
            if (_logs.TryGetValue(id, out var value))
            {
                return value;
            }

            throw new Exception($"{id} Not Found");
        }

        public List<string> Select(string tag, int top)
        {
            return _logs.Keys.ToList();
        }

        public void UpdateTag(string id, string tag, string newTag)
        {
            _tracker[id] = $"{tag}=>{newTag}";
        }
    }
}
