namespace KirokuG2.Service.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Configurator;
    using Implements;

    public class Setup
    {
        private static bool _initialized = false;

        public static void Execute()
        {
            if (!_initialized)
            {
                var file = IsAzureFunction();

                var cfg = CfgManager.GetCfg();

                if (CfgManager.CheckCfg(cfg, out string erroMsg))
                {
                    using (Deserializer deseralizer = new Deserializer())
                    {
                        deseralizer.Execute(file);

                        var kiroku_cfg = ConvertToDictionary(deseralizer.GetTag("kiroku-service"));

                        _initialized = Configuration.Load(kiroku_cfg);
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
