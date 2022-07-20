namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using Implements.Utility;

    public class Configuration
    {
        /// <summary>
        /// Set Sensor app and logging configs.
        /// </summary>
        /// <param name="sensorConfig"></param>
        /// <param name="kirokuConfig"></param>
        public static bool SetConfigs(List<KeyValuePair<string, string>> sensorConfig, List<KeyValuePair<string, string>> kirokuConfig)
        {
            _sensorTagList = sensorConfig;

            _kirokuTagList = kirokuConfig;

            if (SetSensorConfig())
            {
                return SetKirokuConfig();
            }

            return false;
        }

        /// <summary>
        /// Load raw sensor config through switch tree, sorting and loading into the backing fields.
        /// </summary>
        private static bool SetSensorConfig()
        {
            foreach (var kvp in SensorTagList)
            {
                switch (kvp.Key.ToString())
                {
                    case s_source:
                        _source = kvp.Value;
                        break;
                    case s_sql:
                        SQLConnectionString = kvp.Value;
                        break;
                    case s_debug:
                        _debug = kvp.Value;
                        break;
                    case s_worker:
                        _worker = kvp.Value;
                        break;
                    case s_agent:
                        _agent = kvp.Value;
                        break;

                    default:
                        { }
                        break;
                }
            }

            return true;
        }

        private static bool SetKirokuConfig()
        {
            return true;
        }

        // Constants
        public const string IpAddress = "IpAddress";
        public const string UnknownDataCenter = "UNKNOWN";
        public const string UnknownDataCenterTag = "UNK";
        public const string StatusOnline = "ONLINE";
        public const string StatusNoMatch = "NOMATCH";
        public const string SensorLocation = "NOMATCH";

        private const string s_sensorConfig = "sensor";
        private const string s_kirokuConfig = "kiroku";
        private const string s_source = "source";
        private const string s_sql = "sql";
        private const string s_debug = "debug";
        private const string s_worker = "worker";
        private const string s_agent = "agent";

        private static readonly string s_localhost = "localhost";

        /// <summary>
        /// Public readonly properties, backing fields applied with proper conversion
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> SensorTagList { get { return _sensorTagList; } }
        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }

        // App settings
        public static readonly DateTime Session = DateTime.Now.ToUniversalTime();
        public static string Source
        {
            get
            {
                if (_source == s_localhost)
                {
                    var machineName = System.Environment.MachineName;
                    return machineName;
                }
                else if (string.IsNullOrEmpty(_source))
                {
                    var machineName = System.Environment.MachineName;
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
        public static bool Debug { get { return Conversion.ConvertValueToBool(_debug); } }
        public static bool Worker { get { return Conversion.ConvertValueToBool(_worker); } }
        public static bool Agent { get { return Conversion.ConvertValueToBool(_agent); } }

        /// <summary>
        /// Private backing fields
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> _sensorTagList;
        private static List<KeyValuePair<string, string>> _kirokuTagList;

        // Operations modes
        private static string _source;
        private static string _debug;
        private static string _worker;
        private static string _agent;
    }
}
