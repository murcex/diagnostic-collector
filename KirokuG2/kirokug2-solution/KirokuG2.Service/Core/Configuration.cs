namespace KirokuG2.Service.Core
{
    using PlyQor.Client;
    using System.Collections.Generic;
    using System.Linq;

    public class Configuration
    {
        // -- public --

        public static List<string> CollectorTokens => _collectorTokens;

        public static List<string> QueryTokens => _queryTokens;

        public static PlyClient Storage => _storage;

        // -- private --

        private static List<string> _queryTokens;

        private static List<string> _collectorTokens;

        private static PlyClient _storage;

        // -- preload --

        private static string _rawCollectorTokens;

        private static string _rawQueryTokens;

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
            "STORAGE_URL" => _storageUrl = value,
            "STORAGE_CONTAINER" => _storageContainer = value,
            "STORAGE_TOKEN" => _storageToken = value,
            "COLLECTOR_TOKENS" => _rawCollectorTokens = value,
            "QUERY_TOKENS" => _rawQueryTokens = value,
            _ => null,
        };

        private static bool PostLoad()
        {
            // collector tokens
            _collectorTokens = _rawCollectorTokens.Split(',').ToList();

            // query tokens
            _queryTokens = _rawQueryTokens.Split(',').ToList();

            // plyqor client
            _storage = new PlyClient(_storageUrl, _storageContainer, _storageToken);

            return true;
        }
    }
}
