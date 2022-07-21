namespace PlyQor.Engine.Core
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Engine.Resources;

    class Initializer
    {
        public static bool Execute()
        {
            SetApplication();

            SetAppliances();

            return true;
        }

        private static bool SetApplication()
        {
            var plyqor_configuration = Environment.GetEnvironmentVariable("PLYQOR_CFG", EnvironmentVariableTarget.Process);

            Configuration.Load(plyqor_configuration);

            var containers = GetContainerConfigurations();

            Configuration.LoadContainers(containers);
        }

        private static bool SetAppliances()
        {
            return true;
        }

        //private static Dictionary<string, List<string>> GetContainerTokens()
        //{
        //    Dictionary<string, List<string>> containerTokens = new Dictionary<string, List<string>>();
        //    Dictionary<string, int> retentionPolicy = new Dictionary<string, int>();

        //    var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

        //    var containers = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

        //    if (containers != null && containers.Count > 0)
        //    {
        //        foreach (var container in containers.Keys)
        //        {
        //            if (containers.TryGetValue(container, out Dictionary<string, string> configuration))
        //            {
        //                if (configuration.TryGetValue(InitializerValues.TokensConfigKey, out string tokensJson))
        //                {
        //                    var tokens = JsonConvert.DeserializeObject<List<string>>(tokensJson);

        //                    containerTokens.Add(container, tokens);
        //                }

        //                if (configuration.TryGetValue(InitializerValues.RetentionConfigKey, out string s_days))
        //                {
        //                    if (int.TryParse(s_days, out int days))
        //                    {
        //                        if (days != 0)
        //                        {
        //                            if (days > 0)
        //                            {
        //                                days *= -1;
        //                            }

        //                            retentionPolicy.Add(container, days);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //
        //                    }

        //                }
        //            }
        //        }
        //    }

        //    Configuration.SetRetentionPolicy(retentionPolicy);

        //    return containerTokens;
        //}

        public static Dictionary<string, Dictionary<string, string>> GetContainerConfigurations()
        {
            var json = StorageProvider.SelectKey(InitializerValues.SystemContainer, InitializerValues.ContainersValue);

            var containers = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

            return containers;
        }
    }
}
