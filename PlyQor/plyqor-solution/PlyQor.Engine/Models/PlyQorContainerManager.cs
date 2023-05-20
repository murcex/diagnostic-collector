using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlyQor.Engine.Models
{
    public class PlyQorContainerManager
    {
        private static Dictionary<string, Dictionary<string, string>> _containers;

        private static Dictionary<string, List<string>> _containerTokens;

        private static Dictionary<string, PlyQorRetentionConfiguration> _containerRetentionConfigurations;

        public PlyQorContainerManager(Dictionary<string, Dictionary<string, string>> containers)
        {
            _containers = containers;

            _containerTokens = new Dictionary<string, List<string>>();

            _containerRetentionConfigurations = new Dictionary<string, PlyQorRetentionConfiguration>();

            ExtractTokens();

            //ExtraceRetentionConfigurations();
        }

        public static List<string> GetContainers()
        {
            return _containers.Keys.ToList();
        }

        public static bool CheckToken(string container, string token)
        {
            if (_containerTokens.TryGetValue(container, out List<string> tokens))
            {
                if (tokens.Contains(token))
                {
                    return true;
                }
            }

            return false;
        }

        public static int GetDataRetentionValue()
        {
            return GetContainerValue("retention");
        }

        public static int GetDataRetentionSize()
        {
            return GetContainerValue("retention_count");
        }

        public static int GetDataRetentionCooldown()
        {
            return GetContainerValue("retention_cooldown");
        }

        public static int GetTraceRetentionValue()
        {
            return GetContainerValue("trace");
        }

        private static int GetContainerValue(string key)
        {
            if (_containers != null && _containers.Count > 0)
            {
                foreach (var container in _containers.Keys)
                {
                    if (_containers.TryGetValue(container, out Dictionary<string, string> configuration))
                    {
                        if (configuration.TryGetValue(key, out string value))
                        {
                            return Int32.TryParse(value, out int result) ? result : 0;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }

            return 0;
        }

        private static void ExtractTokens()
        {
            if (_containers != null && _containers.Count > 0)
            {
                foreach (var container in _containers.Keys)
                {
                    if (_containers.TryGetValue(container, out Dictionary<string, string> configuration))
                    {
                        if (configuration.TryGetValue("Tokens", out string tokensJson))
                        {
                            var tokens = JsonConvert.DeserializeObject<List<string>>(tokensJson);

                            _containerTokens.Add(container, tokens);
                        }
                    }
                }
            }
        }

        // TODO: leagcy?
        //private static int ExtractInt(string key)
        //{
        //    if (_containers != null && _containers.Count > 0)
        //    {
        //        foreach (var container in _containers.Keys)
        //        {
        //            if (_containers.TryGetValue(container, out Dictionary<string, string> configuration))
        //            {
        //                if (configuration.TryGetValue(key, out string tokensJson))
        //                {
        //                    Int32.TryParse(tokensJson, out int value);

        //                    return value;
        //                }
        //            }
        //        }
        //    }
        //}

        private static void ExtraceRetentionConfigurations()
        {
            if (_containers != null && _containers.Count > 0)
            {
                foreach (var container in _containers.Keys)
                {
                    int days = 0;
                    int size = 0;
                    int cooldown = 0;
                    int trace = 0;

                    if (_containers.TryGetValue(container, out Dictionary<string, string> configuration))
                    {

                        if (configuration.TryGetValue("Retention", out string data))
                        {
                            var tokens = JsonConvert.DeserializeObject<List<string>>(data);

                            _containerTokens.Add(container, tokens);
                        }

                        if (configuration.TryGetValue("Size", out data))
                        {
                            var tokens = JsonConvert.DeserializeObject<List<string>>(data);

                            _containerTokens.Add(container, tokens);
                        }

                        if (configuration.TryGetValue("Cooldown", out string tokensJson))
                        {
                            var tokens = JsonConvert.DeserializeObject<List<string>>(data);

                            _containerTokens.Add(container, tokens);
                        }

                        //if (configuration.TryGetValue("Trace", out string tokensJson))
                        //{
                        //    var tokens = JsonConvert.DeserializeObject<List<string>>(data);

                        //    _containerTokens.Add(container, tokens);
                        //}

                        //PlyQorRetentionConfiguration plyQorRetentionConfiguration = new PlyQorRetentionConfiguration();
                    }
                }
            }
        }
    }
}
