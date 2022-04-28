namespace Javelin.Worker
{
    using System.Collections.Generic;
    using Configurator;
    using Implements;

    internal class Initializer
    {
        private static bool _initialized = false;

        public static void Execute()
        {
            if (!_initialized)
            {
                var cfg = CfgManager.GetCfg();

                if (CfgManager.CheckCfg(cfg, out string errorMsg))
                {
                    using (Deserializer deserilaizer = new Deserializer())
                    {
                        var _file = @"D:\home\data\app\cfg\Config.ini";

                        deserilaizer.Execute(_file);

                        var worker_cfg = ConvertToDictionary(deserilaizer.GetTag("worker"));

                        Dictionary<string, Dictionary<string, string>> cfgDictionary = new Dictionary<string, Dictionary<string, string>>();
                        cfgDictionary.Add("Worker", worker_cfg);

                        Configuration.Load(cfgDictionary);

                        _initialized = true;
                    }
                }
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
