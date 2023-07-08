namespace PlyQor.Audit.Core
{
    using System;
    using System.Collections.Generic;

    class Configuration
    {

        private static string _storageProviderTest;

        private static string _plyManagerTest;

        private static string _plyClientTest;

        private static string _baselineTest;

        private static string _documentTest;

        private static string _nanoCount;

        private static string _microCount;

        private static string _smallCount;

        private static string _mediumCount;

        private static string _largeCount;

        // replace with generated data
        public static string Document { get; set; }

        public static string DocumentName { get; } = Guid.NewGuid().ToString();

        public static int DocumentLength { get; set; } = 0;

        public static string Container { get; set; }

        // client values
        public static string ClientContainer { get; set; }

        public static string ClientToken { get; set; }

        public static string ClientUrl { get; set; }

        // replace with cfg value
        public static string Token { get; set; }

        public static string Tag_Upload { get; } = "Upload";

        public static string Tag_Stage { get; } = "Stage";

        public static string Key_1 { get; } = Guid.NewGuid().ToString();

        public static string Key_2 { get; } = Guid.NewGuid().ToString();

        public static string Data_1 { get; } = Guid.NewGuid().ToString();

        public static string Data_2 { get; } = Guid.NewGuid().ToString();

        public static List<string> DeleteTestKeys { get; set; }

        public static bool StorageProviderTest
        {
            get
            {
                return ConvertToBool(_storageProviderTest);
            }
        }

        public static bool PlyManagerTest
        {
            get
            {
                return ConvertToBool(_plyManagerTest);
            }
        }

        public static bool PlyClientTest
        {
            get
            {
                return ConvertToBool(_plyClientTest);
            }
        }

        public static bool BaselineTest
        {
            get
            {
                return ConvertToBool(_baselineTest);
            }
        }

        public static bool DocumentTest
        {
            get
            {
                return ConvertToBool(_documentTest);
            }
        }

        public static int NanoCount
        {
            get
            {
                return ConvertToInt(_nanoCount);
            }
        }

        public static int MicroCount
        {
            get
            {
                return ConvertToInt(_microCount);
            }
        }

        public static int SmallCount
        {
            get
            {
                return ConvertToInt(_smallCount);
            }
        }

        public static int MediumCount
        {
            get
            {
                return ConvertToInt(_mediumCount);
            }
        }

        public static int LargeCount
        {
            get
            {
                return ConvertToInt(_largeCount);
            }
        }

        public static void Load(Dictionary<string, Dictionary<string, string>> cfg_package)
        {
            if (cfg_package.TryGetValue("plyclient", out var cfg))
            {
                foreach (var kv in cfg)
                {
                    _ = kv.Key switch
                    {
                        "storage_provider" => _storageProviderTest = kv.Value,
                        "plymanager" => _plyManagerTest = kv.Value,
                        "plyclient" => _plyClientTest = kv.Value,

                        "client_url" => ClientUrl = kv.Value,
                        "client_container" => ClientContainer = kv.Value,
                        "client_token" => ClientToken = kv.Value,
                        "local_container" => Container = kv.Value,
                        "local_token" => Token = kv.Value,
                        "document" => Document = kv.Value,

                        "baseline" => _baselineTest = kv.Value,
                        "documents" => _documentTest = kv.Value,
                        "nano" => _nanoCount = kv.Value,
                        "micro" => _microCount = kv.Value,
                        "small" => _smallCount = kv.Value,
                        "medium" => _mediumCount = kv.Value,
                        "large" => _largeCount = kv.Value,

                        _ => null
                    };
                }
            }
            else
            {
                throw new Exception("cfg package doesn't contain plyclient cfg");
            }
        }

        private static int ConvertToInt(string input)
        {
            return Convert.ToInt32(input);
        }

        private static bool ConvertToBool(string input)
        {
            return bool.Parse(input);
        }
    }
}
