namespace PlyQor.Audit.Core
{
    using System;
    using System.Collections.Generic;

    class Configuration
    {
        // replace with generated data
        public static string Document { get; set; }

        public static string DocumentName { get; } = Guid.NewGuid().ToString();

        public static int DocumentLength { get; set; } = 0;

        public static string Collection { get; set; }

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

        public static void Load(Dictionary<string, string> cfg)
        {
            foreach (var kv in cfg)
            {
                _ = kv.Key switch
                {
                    "client_url" => ClientUrl = kv.Value,
                    "client_container" => ClientContainer = kv.Value,
                    "client_token" => ClientToken = kv.Value,
                    "local_container" => Collection = kv.Value,
                    "local_token" => Token = kv.Value,
                    "document" => Document = kv.Value,
                    
                    _ => null
                };
            }
        }
    }
}
