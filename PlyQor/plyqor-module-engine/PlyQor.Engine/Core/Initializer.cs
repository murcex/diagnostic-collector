namespace PlyQor.Engine.Core
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Engine.Resources;

    class Initializer
    {
        public static bool Execute(Dictionary<string, Dictionary<string, string>> configuration)
        {
            SetApplication(configuration);

            SetAppliances(configuration);

            return true;
        }

        private static bool SetApplication(Dictionary<string, Dictionary<string, string>> configuration)
        {
            if (configuration.TryGetValue(InitializerValues.PlyQorConfigKey, out Dictionary<string, string> plyqorConfiguration))
            {
                if (Configuration.Load(plyqorConfiguration))
                {
                    var containerTokens = GetContainerTokens();

                    return Configuration.SetContainerTokens(containerTokens);
                }
            }

            // TODO: hold for pylon replacement
            throw new Exception("SetApplication Failure");
        }

        private static bool SetAppliances(Dictionary<string, Dictionary<string, string>> configuration)
        {
            return true;
        }

        private static Dictionary<string, List<string>> GetContainerTokens()
        {
            Dictionary<string, List<string>> containerTokens = new Dictionary<string, List<string>>();
            Dictionary<string, int> retentionPolicy = new Dictionary<string, int>();

            var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

            var containers = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

            foreach (var container in containers.Keys)
            {
                if (containers.TryGetValue(container, out Dictionary<string, string> configuration))
                {
                    if (configuration.TryGetValue(InitializerValues.TokensConfigKey, out string tokensJson))
                    {
                        var tokens = JsonConvert.DeserializeObject<List<string>>(tokensJson);

                        containerTokens.Add(container, tokens);
                    }

                    if (configuration.TryGetValue(InitializerValues.RetentionConfigKey, out string s_days))
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
                }
            }

            Configuration.SetRetentionPolicy(retentionPolicy);

            return containerTokens;
        }
    }
}
