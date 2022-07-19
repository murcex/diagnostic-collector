namespace KirokuG2.Processor.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Configurator;
    using Implements;
    using KirokuG2.Loader;

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

                        var kiroku_cfg = ConvertToDictionary(deseralizer.GetTag("kiroku-processor"));

                        Configuration.Load(kiroku_cfg);

                        _initialized = true;
                    }

                    SetupAppliances();
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

        private static bool SetupAppliances()
        {
            var kload = KLoaderManager.Configuration(Configuration.Storage, Configuration.Database);

            var kiroku = KManager.Configure(true);

            return (kload && kiroku);
        }
    }
}
