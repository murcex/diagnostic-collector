namespace PlyQor.Engine.Core
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PlyQor.Engine.Resources;
    using PlyQor.Resources;

    class Configuration
    {
        private static string _database;

        private static Dictionary<string, Dictionary<string, string>> _containers;

        private static Dictionary<string, List<string>> _tokens;

        private static Dictionary<string, int> _retention;

        private static string _retentionCapacity;

        private static string _retentionCycle;

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

        public static Dictionary<string, Dictionary<string, string>> Containers => _containers;

        public static Dictionary<string, List<string>> Tokens => _tokens;

        public static Dictionary<string, int> RetentionPolicy => _retention;

        public static string RetentionCapacity => _retentionCapacity;

        public static string RetentionCycle => _retentionCycle;

        public static string TraceRetention => _traceRetention;

        public static List<string> Operations => _operations;

        public static bool Load(string configuration)
        {
            if (string.IsNullOrEmpty(configuration))
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _database = configuration;

            return true;
        }

        public static bool LoadContainers(Dictionary<string, Dictionary<string, string>> containers)
        {
            // extract system container
            if (containers.TryGetValue("SYSTEM", out var system_container))
            {
                if (system_container == null || system_container.Count < 1)
                {
                    throw new Exception($"SYSTEM Container is Null or Empty");
                }

                if (system_container.TryGetValue("TRACE", out var system_trace))
                {
                    _traceRetention = system_trace;
                }
                else
                {
                    _traceRetention = "1";
                }

                if (system_container.TryGetValue("CAPACITY", out var system_retention_capacity))
                {
                    _retentionCapacity = system_retention_capacity;
                }
                else
                {
                    _retentionCapacity = "0";
                }

                if (system_container.TryGetValue("CYCLE", out var system_retention_cooldown))
                {
                    _retentionCycle = system_retention_cooldown;
                }
                else
                {
                    _retentionCycle = "0";
                }

                containers.Remove("SYSTEM");
            }
            else
            {
                throw new Exception($"SYSTEM Container configuration is missing");
            }

            if (_tokens == null)
            {
                _tokens = new Dictionary<string, List<string>>();
            }

            foreach (var container in containers)
            {
                if (container.Value.TryGetValue(InitializerValues.TokensConfigKey, out string tokensJson))
                {
                    var tokens = JsonConvert.DeserializeObject<List<string>>(tokensJson);

                    _tokens.Add(container.Key.ToUpper(), tokens);
                }

                // extract retention policy

                // if "retention" else set as 0
                if (container.Value.TryGetValue(InitializerValues.RetentionConfigKey, out string s_days))
                {
                    if (int.TryParse(s_days, out int days))
                    {
                        if (days != 0)
                        {
                            if (days > 0)
                            {
                                days *= -1;
                            }

                            retentionPolicy.Add(container, days);
                        }
                    }
                    else
                    {
                        //
                    }

                }

                // if "trace" else use system default
            }

            return true;
        }
    }
}
