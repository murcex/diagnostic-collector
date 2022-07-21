namespace ExampleModule
{
    using System;
    using System.Collections.Generic;
    using Kiroku;

    static class Configuration
    {
        /// <summary>
        /// Set the application and logging configs.
        /// </summary>
        /// <param name="appCfg"></param>
        /// <param name="kirokuCfg"></param>
        public static bool SetConfigs(List<KeyValuePair<string, string>> appCfg, List<KeyValuePair<string, string>> kirokuCfg)
        {
            _appTagList = appCfg;

            _kirokuTagList = kirokuCfg;

            if (SetAppConfig())
            {
                return SetKirokuConfig();
            }

            return false;
        }

        /// <summary>
        /// Parse and set the application config package.
        /// </summary>
        private static bool SetAppConfig()
        {
            foreach (var kvp in AppTagList)
            {
                switch (kvp.Key.ToString())
                {
                    // TODO: *** add custom application variables ***

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

        /// <summary>
        /// Kiroku methods.
        /// </summary>

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

        /// <summary>
        /// Set kiroku config.
        /// </summary>
        /// <returns></returns>
        private static bool SetKirokuConfig()
        {
            KManager.Configure(KirokuTagList);

            return true;
        }

        /// <summary>
        /// Private backing fields.
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> _appTagList;
        private static List<KeyValuePair<string, string>> _kirokuTagList;

        // App Settings
        // TODO: *** add custom application variables ***
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
        private static List<KeyValuePair<string, string>> AppTagList { get { return _appTagList; } }
        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }

        // App Settings
        // TODO: *** add custom application variables ***
        public static String LocalDirectory { get { return _localdir; } }
        public static String AzureContainer { get { return _container; } }
        public static Double RetentionDays { get { return Convert.ToDouble(_retentionDays); } }
        public static Double CleanseHours { get { return Convert.ToInt32(_cleanseHours); } }
        public static String AzureStorage { get { return _storage; } }
    }
}