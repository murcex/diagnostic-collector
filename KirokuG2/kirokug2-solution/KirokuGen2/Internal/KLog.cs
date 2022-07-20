namespace KirokuG2.Internal
{
    using System.Globalization;
    using System.Text;

    public class KLog : IDisposable, IKLog
    {
        private bool dispose = false;

        private string _delimiter = "$";

        private StringBuilder _logs;

        private KStorage _storage;

        private int _errors;

        private Action<string, string> _injector;

        public KLog(KStorage storage, string source, string function)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(function))
            {
                throw new ArgumentNullException(nameof(function));
            }

            _storage = storage;

            _injector = new Action<string, string>(Injector);

            _errors = 0;

            _logs = new StringBuilder();

            var instance_data = $"{source}${function}";

            Injector("I", instance_data);
        }

        /// <summary>
        /// Create a new KBlock
        /// </summary>
        public KBlock NewBlock(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var KBlock = new KBlock(name, _injector);

            return KBlock;
        }

        /// <summary>
        /// Add Trace event
        /// </summary>
        public void Trace(string data)
        {
            Injector("T", data);
        }

        /// <summary>
        /// Add Info event
        /// </summary>
        public void Info(string data)
        {
            Trace(data);
        }

        /// <summary>
        /// Add Error event
        /// </summary>
        public void Error(string data)
        {
            _errors++;

            Injector("E", data);
        }

        /// <summary>
        /// Add Int Metric
        /// </summary>
        public void Metric(string key, int value)
        {
            CheckMetricKey(key);
            
            var data = $"{1}${key}${value}";
            Injector("M", data);
        }

        /// <summary>
        /// Add Double Metric
        /// </summary>
        public void Metric(string key, double value)
        {
            CheckMetricKey(key);

            var data = $"{2}${key}${value}";
            Injector("M", data);
        }

        /// <summary>
        /// Add Float Metric
        /// </summary>
        public void Metric(string key, float value)
        {
            CheckMetricKey(key);

            var data = $"{3}${key}${value}";
            Injector("M", data);
        }

        /// <summary>
        /// Add Bool Metric
        /// </summary>
        public void Metric(string key, bool value)
        {
            CheckMetricKey(key);

            var data = $"{4}${key}${value}";
            Injector("M", data);
        }

        /// <summary>
        /// Add String Metric (Tag)
        /// </summary>
        public void Metric(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                value = "Value is IsNullOrEmpty";
            }

            CheckMetricKey(key);

            var data = $"{5}${key}${value}";
            Injector("M", data);
        }

        private void CheckMetricKey(string key)
        {
            if (key.Contains(_delimiter))
            {
                throw new ArgumentException($"Invalid delimiter: {_delimiter}");
            }
        }

        private void Injector(string type, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                data = "Data IsNullOrEmpty";
            }
            else
            {
                data = Scrubber(data);
            }

            _logs.AppendLine($"{DateTime.UtcNow.ToString("o", DateTimeFormatInfo.InvariantInfo)},{type},{data}");
        }

        private string Scrubber(string input)
        {
            string clean = "";
            var output = input.Replace("\r\n", clean).Replace("\n", clean).Replace("\r", clean);

            return output;
        }




        // ----




        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    var instance_result = _errors > 0 ? "1" : "0";

                    Injector("SI", instance_result);

                    _storage.AddLogs(_logs);
                }
            }

            //dispose unmanaged resources
            dispose = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
