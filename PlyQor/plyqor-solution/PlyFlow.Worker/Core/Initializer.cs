namespace Javelin.Worker
{
    using Configurator;
    using Implements;
    using KirokuG2;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class Initializer
    {
        private static bool _initialized = false;

        public static void Execute()
        {
            if (!_initialized)
            {
                var file = IsAzureFunction();

                var cfg = CfgManager.GetCfg();

                if (CfgManager.CheckCfg(cfg, out string errorMsg))
                {
                    using (Deserializer deserilaizer = new Deserializer())
                    {
                        deserilaizer.Execute(file);

                        var worker_cfg = ConvertToDictionary(deserilaizer.GetTag("worker"));

                        Dictionary<string, Dictionary<string, string>> cfgDictionary = new Dictionary<string, Dictionary<string, string>>();

                        cfgDictionary.Add("Worker", worker_cfg);

                        Configuration.Load(cfgDictionary);

                        KManager.Configure(true);

                        _initialized = true;
                    }
                }
            }
        }

        private static string IsAzureFunction()
        {
            var check = Environment.GetEnvironmentVariable("FUNCTIONS_EXTENSION_VERSION", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(check))
            {
                return Directory.GetCurrentDirectory() + @"\Config.ini";
            }
            else
            {
                return @"D:\home\data\app\cfg\Config.ini";
            }
        }

        private static Dictionary<string, string> ConvertToDictionary(List<KeyValuePair<string, string>> config)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var kv in config)
            {
                result[kv.Key] = kv.Value;
            }

            return result;
        }
    }
}
