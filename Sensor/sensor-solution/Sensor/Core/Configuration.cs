namespace Sensor
{
    using Implements.Utility;
    using System;
    using System.Collections.Generic;

    public class Configuration
    {
        /// <summary>
        /// Load raw sensor config through switch tree, sorting and loading into the backing fields.
        /// </summary>
        public static bool SetConfigs(Dictionary<string, string> sensorConfig)
        {
            foreach (var kvp in sensorConfig)
            {
                switch (kvp.Key.ToString())
                {
                    case s_source:
                        _source = kvp.Value;
                        break;
                    case s_sql:
                        SQLConnectionString = kvp.Value;
                        break;
                    case s_worker:
                        _worker = kvp.Value;
                        break;

                    default:
                        { }
                        break;
                }
            }

            return true;
        }

        // Constants
        public const string IpAddress = "IpAddress";
        public const string UnknownDataCenter = "UNKNOWN";
        public const string UnknownDataCenterTag = "UNK";
        public const string StatusOnline = "ONLINE";
        public const string StatusNoMatch = "NOMATCH";
        public const string SensorLocation = "NOMATCH";

        private const string s_source = "source";
        private const string s_sql = "sql";
        private const string s_worker = "worker";

        private static readonly string s_localhost = "localhost";

        /// <summary>
        /// Public readonly properties, backing fields applied with proper conversion
        /// </summary>

        // App settings
        public static readonly DateTime Session = DateTime.Now.ToUniversalTime();

        public static string Source
        {
            get
            {
                if (_source == s_localhost)
                {
                    var machineName = Environment.MachineName;
                    return machineName;
                }
                else if (string.IsNullOrEmpty(_source))
                {
                    var machineName = Environment.MachineName;
                    return machineName;
                }
                else
                {
                    return _source;
                }
            }
        }

        public static string SQLConnectionString { get; private set; }

        // Operation Modes
        public static bool Worker { get { return Conversion.ConvertValueToBool(_worker); } }

        /// <summary>
        /// Private backing fields
        /// </summary>

        // Operations modes
        private static string _source;
        private static string _worker;
    }
}
