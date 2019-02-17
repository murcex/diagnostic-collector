namespace KLOGCopy
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    // Kiroku
    using Kiroku;

    // Implements Utility Library
    using Implements;


    public static class Global
    {
        static Global()
        {
            GetConfigs();
            SetConfig();
        }

        public static void GetConfigs()
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Core\Config.ini";

                deserilaizer.Execute(_file);

                _kcopyTagList = deserilaizer.GetTag("kcopy");

                _kirokuTagList = deserilaizer.GetTag("kiroku");
            }
        }

        private static void SetConfig()
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
                        {
                            Log.Error($"Not Hit: {kvp.Key}");
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Private backing fields
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> _kcopyTagList;
        private static List<KeyValuePair<string, string>> _kirokuTagList;

        //
        private static string _debug;
        private static string _localdir;
        private static string _container;
        private static string _retentionDays;
        private static string _cleanseHours;
        private static string _storage;

        /// <summary>
        /// Public readonly properties, backing fields applied with proper conversion
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> KCopyTagList { get { return _kcopyTagList; } }
        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }

        //
        public static String Debug { get { return _debug; } }
        public static String LocalDirectory { get { return _localdir; } }
        public static String AzureContainer { get { return _container; } }
        public static Double RetentionDays { get { return Convert.ToDouble(_retentionDays); } }
        public static Double CleanseHours { get { return Convert.ToInt32(_cleanseHours); } }
        public static String AzureStorage { get { return _storage; } }

        public static void StartLogging()
        {
            KManager.Online(KirokuTagList);
        }

        public static void StopLogging()
        {
            KManager.Offline();
        }

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
