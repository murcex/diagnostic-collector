namespace KirokuG2.Processor.Core
{
    using System.Collections.Generic;
    using PlyQor.Client;

    public class Configuration
    {
        // -- public --

        public static string Database => _database;

        public static PlyClient Storage => _storage;

        // -- private --

        private static string _database;

        private static PlyClient _storage;

        // -- preload --

        private static string _storageUrl;

        private static string _storageContainer;

        private static string _storageToken;

        public static bool Load(Dictionary<string, string> configuration)
        {
            foreach (var kvp in configuration)
            {
                Sort(kvp.Key, kvp.Value);
            }

            PostLoad();

            return true;
        }

        private static string Sort(string key, string value) => key.ToUpper() switch
        {
            "DATABASE" => _database = value,
            "STORAGE_URL" => _storageUrl = value,
            "STORAGE_CONTAINER" => _storageContainer = value,
            "STORAGE_TOKEN" => _storageToken = value,
            _ => null,
        };

        private static bool PostLoad()
        {
            // plyqor client
            _storage = new PlyClient(_storageUrl, _storageContainer, _storageToken);

            return true;
        }
    }
}
