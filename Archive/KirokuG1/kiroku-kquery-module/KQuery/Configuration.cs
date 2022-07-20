namespace KQuery
{
    using System.Collections.Generic;
    using Kiroku;

    class Configuration
    {
        /// <summary>
        /// Set KQuery Configs.
        /// </summary>
        /// <param name="kqueryConfig">KQuery Config</param>
        /// <param name="kirokuConfig">Kiroku Config</param>
        public static bool SetConfigs(List<KeyValuePair<string, string>> kqueryConfig, List<KeyValuePair<string, string>> kirokuConfig)
        {
            KQueryTagList = kqueryConfig;

            KirokuTagList = kirokuConfig;

            if (SetKQueryConfig())
            {
                return SetKirokuConfig();
            }

            return false;
        }

        /// <summary>
        /// Sort KQuery Config packaage and parse into properties.
        /// </summary>
        private static bool SetKQueryConfig()
        {
            foreach (var kvp in KQueryTagList)
            {
                switch (kvp.Key.ToString())
                {
                    case "storage":
                        StorageConnectionString = kvp.Value;
                        break;

                    default:
                        { }
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Set Kiroku Config.
        /// </summary>
        private static bool SetKirokuConfig()
        {
            KManager.Configure(KirokuTagList, dynamic: true);

            return true;
        }

        /// <summary>
        /// KQuery Config package.
        /// </summary>
        private static List<KeyValuePair<string, string>> KQueryTagList { get; set; }
        
        /// <summary>
        /// Kiroku Config package.
        /// </summary>
        public static List<KeyValuePair<string, string>> KirokuTagList { get; set; }
        
        /// <summary>
        /// Storage Blob connection string.
        /// </summary>
        public static string StorageConnectionString { get; private set; }
    }
}
