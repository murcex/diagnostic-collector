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

                SensorTagList = deserilaizer.GetTag(SensorConfig);

                KirokuTagList = deserilaizer.GetTag(KirokuConfig);
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
                    case "source":
                        _source = kvp.Value;
                        break;
                    case "sql":
                        SQLConnectionString = kvp.Value;
                        break;
                    case "debug":
                        _debug = kvp.Value;
                        break;
                    case "worker":
                        _worker = kvp.Value;
                        break;
                    case "agent":
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
        public static readonly string IpAddress = "IpAddress";
        public static readonly string UnknownDataCenter = "UNKNOWN";
        public static readonly string UnknownDataCenterTag = "UNK";
        public static readonly string StatusOnline = "ONLINE";
        public static readonly string StatusNoMatch = "NOMATCH";
        public static readonly string SensorLocation = "NOMATCH";

        private static readonly string SensorConfig = "sensor";
        private static readonly string KirokuConfig = "kiroku";

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
                if (_source == "localhost")
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
