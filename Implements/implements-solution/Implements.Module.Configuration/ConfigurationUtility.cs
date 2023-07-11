using Implements.Configuration.Internal;

namespace Implements.Configuration
{
    public class ConfigurationUtility : IDisposable
    {
        private Deserializer _deserializer;
        private Serializer _serializer;
        private bool disposedValue;

        public ConfigurationUtility()
        {
            _deserializer = new();
            _serializer = new();
        }

        public Dictionary<string, Dictionary<string, string>> Deserialize(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("input is NullOrEmpty");
            }

            List<string> lines = new();
            using (StringReader reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return _deserializer.Execute(lines);
        }

        public Dictionary<string, Dictionary<string, string>> Deserialize(List<string> input)
        {
            return _deserializer.Execute(input);
        }

        public string Serialize(Dictionary<string, Dictionary<string, string>> input)
        {
            if (!input.Any())
            {
                throw new Exception("input Dictionary is empty");
            }

            return _serializer.Execute(input);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                _deserializer = null;
                _serializer = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
