namespace KCopy.Core
{
    using System;
    using System.Collections.Generic;
    using Kiroku;

    static class Configuration
    {
        /// <summary>
        /// Parse and set KCopy config package.
        /// </summary>
        public static bool SetKCopyConfig(List<KeyValuePair<string, string>> kcopyConfig)
        {
            foreach (var kvp in kcopyConfig)
            {
                switch (kvp.Key.ToString())
                {
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
        /// 
        /// </summary>
        public static void AddRetentionThreshold(DateTime input)
        {
            _retentionThreshold = input;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void AddCleanseThreshold(DateTime input)
        {
            _cleanseThreshold = input;
        }

        /// <summary>
        /// Readonly properties, backing fields applied with proper conversion.
        /// </summary>
        
        // App Settings
        private static string _localdir;
        private static string _container;
        private static string _storage;
        private static string _retentionDays;
        private static string _cleanseHours;
        private static DateTime _retentionThreshold;
        private static DateTime _cleanseThreshold;

        // App Settings
        public static string LocalDirectory => _localdir;
        public static string AzureContainer => _container;
        public static string AzureStorage => _storage;
        public static string RetentionDays => _retentionDays;
        public static string CleanseHours => _cleanseHours;
        public static DateTime RetentionThreshold => _retentionThreshold;
        public static DateTime CleanseThreshold => _cleanseThreshold;
    }
}
