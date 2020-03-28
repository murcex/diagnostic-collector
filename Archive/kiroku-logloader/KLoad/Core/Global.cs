namespace KLoad
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    // Kiroku
    using Kiroku;

    // Implements Utility Library
    using Implements;

    /// <summary>
    /// Global Configuration.
    /// </summary>
    public static class Global
    {
        static Global()
        {
            GetConfigs();
            SetConfig();
        }

        /// <summary>
        /// Read Config.ini, staged results.
        /// </summary>
        public static void GetConfigs()
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Core\Config.ini";

                deserilaizer.Execute(_file);

                _kloaderTagList = deserilaizer.GetTag("kloader");

                _kirokuTagList = deserilaizer.GetTag("kiroku");
            }
        }

        /// <summary>
        /// Sort staged Config.ini results into backing fields.
        /// </summary>
        private static void SetConfig()
        {
            foreach (var kvp in KLoaderTagList)
            {
                switch (kvp.Key.ToString())
                {
                    case "debug":
                        _debug = kvp.Value;
                        break;
                    case "azureContainer":
                        _container = kvp.Value;
                        break;
                    case "retentionDays":
                        _retentionDays = kvp.Value;
                        break;
                    case "messageLength":
                        _messageLength = kvp.Value;
                        break;
                    case "storage":
                        _storage = kvp.Value;
                        break;
                    case "sql":
                        _sql = kvp.Value;
                        break;
                    case "instance":
                        _instance = kvp.Value;
                        break;
                    case "block":
                        _block = kvp.Value;
                        break;
                    case "trace":
                        _trace = kvp.Value;
                        break;
                    case "info":
                        _info = kvp.Value;
                        break;
                    case "warning":
                        _warning = kvp.Value;
                        break;
                    case "error":
                        _error = kvp.Value;
                        break;
                    case "metric":
                        _metric = kvp.Value;
                        break;
                    case "result":
                        _result = kvp.Value;
                        break;

                    default:
                        {
                            Log.Error($"Not Hit: {kvp.Key}");
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Private backing fields.
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> _kloaderTagList;
        private static List<KeyValuePair<string, string>> _kirokuTagList;

        // Loader settings
        private static string _debug;
        private static string _container;
        private static string _storage;
        private static string _sql;
        private static string _retentionDays;
        private static string _messageLength;

        // Event Switch settings
        private static string _instance;
        private static string _block;
        private static string _trace;
        private static string _info;
        private static string _warning;
        private static string _error;
        private static string _metric;
        private static string _result;

        /// <summary>
        /// Public read-only properties, backing fields applied with proper conversion.
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> KLoaderTagList { get { return _kloaderTagList; } }
        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }

        // Loader settings
        public static String Debug { get { return _debug; } }
        public static String AzureContainer { get { return _container; } }
        public static String AzureStorage { get { return _storage; } }
        public static String SqlConnetionString { get { return _sql; } }
        public static Double RetentionDays { get { return Convert.ToDouble(_retentionDays); } }
        public static int MessageLength { get { return Convert.ToInt32(_messageLength); } }

        // Event Switch settings
        public static bool Instance { get { return Utility.ConvertValueToBool(_instance); } }
        public static bool Block { get { return Utility.ConvertValueToBool(_block); } }
        public static bool Trace { get { return Utility.ConvertValueToBool(_trace); } }
        public static bool Info { get { return Utility.ConvertValueToBool(_info); } }
        public static bool Warning { get { return Utility.ConvertValueToBool(_warning); } }
        public static bool Error { get { return Utility.ConvertValueToBool(_error); } }
        public static bool Metric { get { return Utility.ConvertValueToBool(_metric); } }
        public static bool Result { get { return Utility.ConvertValueToBool(_result); } }

        /// <summary>
        /// Start Kiroku Logging Instance.
        /// </summary>
        public static void StartLogging()
        {
            KManager.Online(KirokuTagList);
        }

        /// <summary>
        /// Stop Kiroku Logging Instance.
        /// </summary>
        public static void StopLogging()
        {
            KManager.Offline();
        }

        /// <summary>
        /// Check for local debugging.
        /// </summary>
        public static void CheckDebug()
        {
            if (Debug == "1")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tDEBUG DETECTED, PRESS ANY KEY");
                Console.ReadKey();
            }
        }
    }
}
