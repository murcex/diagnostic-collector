using Implements;
using PlyQor.Engine;
using System.Collections.Generic;
using System.IO;

namespace PlyQor.Audit.Core
{
    class Initializer
    {
        public static void Execute()
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Config.ini";

                deserilaizer.Execute(_file);

                var plyqor_cfg = ConvertToDictionary(deserilaizer.GetTag("plyengine_audit"));

                var plyclient_cfg = ConvertToDictionary(deserilaizer.GetTag("plyclient_audit"));

                Dictionary<string,Dictionary<string, string>> cfg = new Dictionary<string,Dictionary<string,string>>();
                cfg.Add("PlyQor", plyqor_cfg);

                //PlyQorManager.Initialize();

                Configuration.Load(plyclient_cfg);
            }
        }

        private static Dictionary<string, string> ConvertToDictionary(List<KeyValuePair<string, string>> config)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            
            foreach(var kv in config)
            {
                result[kv.Key] = kv.Value;
            }

            return result;
        }
    }
}
