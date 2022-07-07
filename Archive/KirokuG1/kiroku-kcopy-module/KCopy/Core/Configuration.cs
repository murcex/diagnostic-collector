namespace KCopy
{
    using System;
    using System.Collections.Generic;
    using Kiroku;

    static class Configuration
    {
        /// <summary>
        /// Set KCopy app and logging configs.
        /// </summary>
        /// <param name="kcopyConfig"></param>
        /// <param name="kirokuConfig"></param>
        public static bool SetConfigs(List<KeyValuePair<string, string>> kcopyConfig, List<KeyValuePair<string, string>> kirokuConfig)
        {
            _kcopyTagList = kcopyConfig;

            _kirokuTagList = kirokuConfig;

            if (SetKCopyConfig())
            {
                return SetKirokuConfig();
            }

            return false;
        }

        /// <summary>
        /// Parse and set KCopy config package.
        /// </summary>
        private static bool SetKCopyConfig()
        {
            foreach (var kvp in KCopyTagList)
            {
                switch (kvp.Key.ToString())
                {
                    case "debug":
                        _debug = kvp.Value;
                        break;
                    case "localDir":
                        _localdir = kvp.Value;
                        break;
                    case "retentionDays":
                        _retentionDays = kvp.Value;
                        break;
                    case "cleanseHours":
                        _cleanseHours = kvp.Value;
                        break;
                    case "azureContainer":
                        _container = kvp.Value;
                        break;
                    case "storage":
                        _storage = kvp.Value;
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
            KManager.Configure(KirokuTagList);

            return true;
        }

        /// <summary>
        /// Private backing fields.
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> _kcopyTagList;
        private static List<KeyValuePair<string, string>> _kirokuTagList;

        // App Settings
        private static string _debug; // TODO: remove debug from all kcopy config packages moving forward.
        private static string _localdir;
        private static string _container;
        private static string _retentionDays;
        private static string _cleanseHours;
        private static string _storage;

        /// <summary>
        /// Readonly properties, backing fields applied with proper conversion.
        /// </summary>

        // Config Packages
        private static List<KeyValuePair<string, string>> KCopyTagList { get { return _kcopyTagList; } }
        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }

        // App Settings
        public static String LocalDirectory { get { return _localdir; } }
        public static String AzureContainer { get { return _container; } }
        public static Double RetentionDays { get { return Convert.ToDouble(_retentionDays); } }
        public static Double CleanseHours { get { return Convert.ToInt32(_cleanseHours); } }
        public static String AzureStorage { get { return _storage; } }

        /// <summary>
        /// Start Kiroku logging.
        /// </summary>
        public static void StartLogging()
        {
            KManager.Open();
        }

        /// <summary>
        /// Stop Kiroku logging.
        /// </summary>
        public static void StopLogging()
        {
            KManager.Close();
        }
    }
}
