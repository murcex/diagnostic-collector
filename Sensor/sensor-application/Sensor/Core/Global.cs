namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using Implements;

    public static class Global
    {
        /// <summary>
        /// Initialize Global -- ensure configs are loaded
        /// </summary>
        static Global()
        {
            GetConfigs();
            SetConfig();
        }

        /// <summary>
        /// Deserialize Config.ini, load kflow and kiroku objects into backing fields
        /// </summary>
        public static void GetConfigs()
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                deserilaizer.Execute();

                SensorTagList = deserilaizer.GetTag(s_sensorConfig);

                KirokuTagList = deserilaizer.GetTag(s_kirokuConfig);
            }
        }

        /// <summary>
        /// Load raw kflow config through switch tree, sorting and loading into the backing fields
        /// </summary>
        private static void SetConfig()
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
                        {
                            Log.Error($"Not Hit: {kvp.Value}");
                        }
                        break;
                }
            }
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
        private static List<KeyValuePair<string, string>> SensorTagList { get; set; }
        public static List<KeyValuePair<string, string>> KirokuTagList { get; private set; }

        // Core
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
        public static bool Debug { get { return Utility.ConvertValueToBool(_debug); } }
        public static bool Worker { get { return Utility.ConvertValueToBool(_worker); } }
        public static bool Agent { get { return Utility.ConvertValueToBool(_agent); } }

        /// <summary>
        /// Private backing fields
        /// </summary>

        private static string _source;
        private static string _debug;
        private static string _worker;
        private static string _agent;

        /// <summary>
        /// Console Debug Check
        /// </summary>
        public static void CheckDebug()
        {
            if (Global.Debug)
            {
                Console.WriteLine("-- Operation Complete --");
                Console.Read();
            }
        }
    }
}
