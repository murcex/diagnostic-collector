namespace KQuery
{
    using System.Collections.Generic;
    using Kiroku;

    class Configuration
    {
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

        private static bool SetKirokuConfig()
        {
            KManager.Configure(KirokuTagList, dynamic: true);

            return true;
        }

        private static List<KeyValuePair<string, string>> KQueryTagList { get; set; }
        public static List<KeyValuePair<string, string>> KirokuTagList { get; set; }
        public static string StorageConnectionString { get; private set; }
    }
}
