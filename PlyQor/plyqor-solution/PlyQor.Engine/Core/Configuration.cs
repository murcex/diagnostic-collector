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

        private static int _defaultRetentionCapacity = 100;

        private static int _defaultRetentionCycle = 250;

        private static int _defaultTraceRetention = 1;

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
        /// Set sql storage connection string
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
        /// Load and set system and user container configurations
        /// </summary>
        public static bool LoadContainers(Dictionary<string, Dictionary<string, string>> containers)
        {
            // extract system container settings
            // TODO: replace strings with prop's
            if (containers.TryGetValue("SYSTEM", out var system_container))
            {
                if (system_container == null || system_container.Count < 1)
                {
                    // create default configuration values
                    system_container = new Dictionary<string, string>()
                    {
                        {"Trace", "1"},
                        {"Capacity", "10"},
                        {"Cycle", "10"}
                    };
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
                // create default configuration values
                system_container = new Dictionary<string, string>()
                    {
                        {"Trace", "1"},
                        {"Capacity", "10"},
                        {"Cycle", "10"}
                    };

                LoadSystemContainerConfigurations(system_container);
            }

            LoadUserContainerConfigurations(containers);

            return true;
        }

        /// <summary>
        /// Load and set system container configurations
        /// </summary>
        private static bool LoadSystemContainerConfigurations(Dictionary<string, string> system_container)
        {
            if (system_container.TryGetValue("Trace", out var system_trace_string))
            {
                int.TryParse(system_trace_string, out var system_trace);

                if (system_trace == 0)
                {
                    system_trace = _defaultTraceRetention;
                }

                if (system_trace < 0)
                {
                    system_trace *= -1;
                }

                _systemTraceRetention = system_trace;
            }
            else
            {
                _systemTraceRetention = _defaultTraceRetention;
            }

            if (system_container.TryGetValue("Capacity", out var system_retention_capacity_string))
            {
                int.TryParse(system_retention_capacity_string, out var system_retention_capacity);

                _retentionCapacity = system_retention_capacity;
            }
            else
            {
                _retentionCapacity = _defaultRetentionCapacity;
            }

            if (system_container.TryGetValue("Cycle", out var system_retention_cooldown_string))
            {
                int.TryParse(system_retention_cooldown_string, out var system_retention_cooldown);

                _retentionCycle = system_retention_cooldown;
            }
            else
            {
                _retentionCycle = _defaultRetentionCycle;
            }

            return true;
        }

        /// <summary>
        /// Load and set user container configurations
        /// </summary>
        private static bool LoadUserContainerConfigurations(Dictionary<string, Dictionary<string, string>> containers)
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

            if (_containers == null)
            {
                _containers = new List<string>();
            }

            // prase container configuration for tokens, data and trace retention policy
            foreach (var container in containers)
            {
                // access tokens
                if (container.Value.TryGetValue(InitializerValues.TokensConfigKey, out string tokensJson))
                {
                    var tokens = JsonConvert.DeserializeObject<List<string>>(tokensJson);

                    _tokens.Add(container.Key.ToUpper(), tokens);
                }

                // data retention policy
                int days = 0;
                if (container.Value.TryGetValue(InitializerValues.RetentionConfigKey, out string s_days))
                {
                    if (int.TryParse(s_days, out days))
                    {
                        if (days < 0)
                        {
                            days *= -1;
                        }
                    }
                }

                _dataRetention.Add(container.Key.ToUpper(), days);

                // trace retention policy
                days = 0;
                if (container.Value.TryGetValue("Trace", out s_days))
                {
                    if (int.TryParse(s_days, out days))
                    {
                        if (days < 0)
                        {
                            days *= -1;
                        }
                    }
                }

                days = days == 0 ? _systemTraceRetention : days;

                _traceRetention.Add(container.Key.ToUpper(), days);

                _containers.Add(container.Key.ToUpper());
            }

            return true;
        }
    }
}
