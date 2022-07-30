namespace PlyQor.Engine.Core
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PlyQor.Engine.Resources;
    using PlyQor.Resources;

    class Configuration
    {
        // --- private ---

        private static string _database;

        private static List<string> _containers;

        private static Dictionary<string, List<string>> _tokens;

        private static Dictionary<string, int> _dataRetention;

        private static Dictionary<string, int> _traceRetention;

        private static int _retentionCapacity;

        private static int _retentionCycle;

        private static int _systemTraceRetention;

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

        // --- public ---

        public static string DatabaseConnection => _database;

        public static List<string> Containers => _containers;

        public static Dictionary<string, List<string>> Tokens => _tokens;

        public static Dictionary<string, int> DataRetentionPolicy => _dataRetention;

        public static Dictionary<string, int> TraceRetentionPolicy => _traceRetention;

        public static int RetentionCapacity => _retentionCapacity;

        public static int RetentionCycle => _retentionCycle;

        public static int SystemTraceRetention => _systemTraceRetention;

        public static List<string> Operations => _operations;

        /// <summary>
        /// 
        /// </summary>
        public static bool Load(string configuration)
        {
            if (string.IsNullOrEmpty(configuration))
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _database = configuration;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool LoadContainers(Dictionary<string, Dictionary<string, string>> containers)
        {
            // extract system container settings
            if (containers.TryGetValue("SYSTEM", out var system_container))
            {
                if (system_container == null || system_container.Count < 1)
                {
                    throw new Exception($"SYSTEM Container is Null or Empty");
                }

                if (LoadSystemContainerConfigurations(system_container))
                {
                    containers.Remove("SYSTEM");
                }
                else
                {
                    throw new Exception($"SYSTEM Container failed to load properly");
                }
            }
            else
            {
                throw new Exception($"SYSTEM Container configuration is missing");
            }

            LoadContainerConfigurations(containers);

            return true;
        }

        private static bool LoadSystemContainerConfigurations(Dictionary<string, string> system_container)
        {
            if (system_container == null || system_container.Count < 1)
            {
                throw new Exception($"SYSTEM Container is Null or Empty");
            }

            if (system_container.TryGetValue("TRACE", out var system_trace_string))
            {
                int.TryParse(system_trace_string, out var system_trace);

                if (system_trace == 0)
                {
                    system_trace = 1;
                }

                _systemTraceRetention = system_trace;
            }
            else
            {
                _systemTraceRetention = 1;
            }

            if (system_container.TryGetValue("CAPACITY", out var system_retention_capacity_string))
            {
                int.TryParse(system_retention_capacity_string, out var system_retention_capacity);

                _retentionCapacity = system_retention_capacity;
            }
            else
            {
                _retentionCapacity = 0;
            }

            if (system_container.TryGetValue("CYCLE", out var system_retention_cooldown_string))
            {
                int.TryParse(system_retention_cooldown_string, out var system_retention_cooldown);

                _retentionCycle = system_retention_cooldown;
            }
            else
            {
                _retentionCycle = 0;
            }

            return true;
        }

        private static bool LoadContainerConfigurations(Dictionary<string, Dictionary<string, string>> containers)
        {
            // set-up tokens, data retention and trace retention collections
            if (_tokens == null)
            {
                _tokens = new Dictionary<string, List<string>>();
            }

            if (_dataRetention == null)
            {
                _dataRetention = new Dictionary<string, int>();
            }

            if (_traceRetention == null)
            {
                _traceRetention = new Dictionary<string, int>();
            }

            // prase container configuration for tokens, data retention and trace retention
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

                            _dataRetention.Add(container.Key.ToUpper(), days);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        days = 0;
                    }

                    _dataRetention.Add(container.Key.ToUpper(), days);
                }

                // if "trace" else use system default
                if (container.Value.TryGetValue("TRACE", out s_days))
                {
                    if (int.TryParse(s_days, out int days))
                    {
                        if (days != 0)
                        {
                            if (days > 0)
                            {
                                days *= -1;
                            }

                            _traceRetention.Add(container.Key.ToUpper(), days);
                        }
                    }
                    else
                    {
                        //
                    }
                }
            }

            return true;
        }
    }
}
