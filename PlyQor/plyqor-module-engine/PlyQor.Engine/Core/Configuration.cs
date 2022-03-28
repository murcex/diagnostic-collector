namespace PlyQor.Engine.Core
{
    using System;
    using System.Collections.Generic;
    using PlyQor.Resources;

    class Configuration
    {
        private static string _database;

        private static string _version = "0.0.0.1";

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
                QueryOperation.SelectTagsByKey,
                QueryOperation.UpdateKey,
                QueryOperation.UpdateData,
                QueryOperation.UpdateTagByKey,
                QueryOperation.UpdateTag,
                QueryOperation.DeleteKey,
                QueryOperation.DeleteTag,
                QueryOperation.DeleteTagsByKey,
                QueryOperation.DeleteTagByKey
            };

        // -----

        public static string DatabaseConnection => _database;

        public static string Version => _version;

        public static Dictionary<string,List<string>> Tokens => _tokens;

        public static Dictionary<string, int> RetentionPolicy => _retention;

        public static string TraceRetention => _traceRetention;

        public static List<string> Operations => _operations;

        public static bool Load(Dictionary<string, string> configuration)
        {
            foreach(var item in configuration)
            {
                switch (item.Key.ToUpper())
                {
                    case "DATABASE":
                        Console.WriteLine($"Loading Database: {item.Value}");
                        _database = item.Value;
                        break;
                    case "ADMIN":
                        Console.WriteLine($"Loading Admin: {item.Value}");
                        break;
                    case "KEY":
                        Console.WriteLine($"Loading Key: {item.Value}");
                        break;
                    case "TRACE_RETENTION":
                        Console.WriteLine($"Loading Trace Retention: {item.Value}");
                        _traceRetention = item.Value;
                        break;

                    default:
                        Console.WriteLine("No Hit!");
                        break;
                }
            }

            return true;
        }

        public static bool SetContainerTokens(Dictionary<string,List<string>> containerTokens)
        {
            _tokens = containerTokens;

            return true;
        }

        public static bool SetRetentionPolicy(Dictionary<string, int> retentionPolicy)
        {
            _retention = retentionPolicy;

            return true;
        }
    }

}
