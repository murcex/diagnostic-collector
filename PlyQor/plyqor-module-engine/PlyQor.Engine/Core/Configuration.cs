namespace PlyQor.Engine.Core
{
    using System.Collections.Generic;
    using PlyQor.Resources;

    class Configuration
    {
        private static string _database;

        private static Dictionary<string, Dictionary<string,string>> _containers;

        

        private static Dictionary<string, List<string>> _tokens;

        private static Dictionary<string, int> _retention;

        private static string _traceRetention;

        private static List<string> _operations =
            new List<string>()
            {
                QueryOperation.InsertKey,
                QueryOperation.InsertTag,
                QueryOperation.SelectKey,
                QueryOperation.SelectTags,
                QueryOperation.SelectTagCount,
                QueryOperation.SelectKeyList,
                QueryOperation.SelectKeyTags,
                QueryOperation.UpdateKey,
                QueryOperation.UpdateData,
                QueryOperation.UpdateKeyTag,
                QueryOperation.UpdateTag,
                QueryOperation.DeleteKey,
                QueryOperation.DeleteTag,
                QueryOperation.DeleteKeyTags,
                QueryOperation.DeleteKeyTag
            };

        public static string DatabaseConnection => _database;

        public static Dictionary<string, Dictionary<string,string>> Containers => _containers;

        public static Dictionary<string, List<string>> Tokens => _tokens;

        public static Dictionary<string, int> RetentionPolicy => _retention;

        public static string TraceRetention => _traceRetention;

        public static List<string> Operations => _operations;

        public static bool Load(Dictionary<string, string> configuration)
        {
            foreach (var item in configuration)
            {
                switch (item.Key.ToUpper())
                {
                    // TODO: (switch) move literal string to const
                    case "DATABASE":
                        _database = item.Value;
                        break;
                    //case "ADMIN":
                    //    break;
                    //case "KEY":
                    //    break;
                    //case "TRACE_RETENTION":
                    //    _traceRetention = item.Value;
                    //break;

                    default:
                        break;
                }
            }

            return true;
        }

        public static bool SetSystemConfiguration()
        {
            // default trace - fail safe: 1 day
            // default retention size - fail safe: *
            // default retention cooldown - fail safe: 0ms

        }

        public static bool SetContainerTokens(Dictionary<string, List<string>> containerTokens)
        {
            _tokens = containerTokens;

            return true;
        }

        public static bool SetContainerTokensV2(Dictionary<string, Dictionary<string,string>> containers)
        {
            return true;
        }

        public static bool SetRetentionPolicy(Dictionary<string, int> retentionPolicy)
        {
            _retention = retentionPolicy;

            return true;
        }

        public static bool SetRetentionPolicyV2(Dictionary<string, Dictionary<string, string>> containers)
        {
            return true;
        }
    }
}
