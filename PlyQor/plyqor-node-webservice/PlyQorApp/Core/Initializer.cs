namespace PlyQorApp
{
    using System.Collections.Generic;
    using Implements;
    using Configurator;
    using PlyQor.Engine;

    class Initializer
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

                        var plyqor_cfg = ConvertToDictionary(deserilaizer.GetTag("plyqor"));

                        Dictionary<string, Dictionary<string, string>> cfgDictionary = new Dictionary<string, Dictionary<string, string>>();
                        cfgDictionary.Add("PlyQor", plyqor_cfg);

                        PlyQorManager.Initialize(cfgDictionary);

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
