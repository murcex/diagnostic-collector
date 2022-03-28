using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Implements;

namespace PlyQor.Admin
{
    internal class AdminManager
    {

        public static void Execute()
        {
            Dictionary<string, Dictionary<string, string>> containers = new Dictionary<string, Dictionary<string, string>>();

            using (Deserializer deserilaizer = new Deserializer())
            {
                var _file = Directory.GetCurrentDirectory() + @"\Config.ini";

                deserilaizer.Execute(_file);

                var containers_cfg = ConvertToDictionary(deserilaizer.GetTag("containers"));

                if (containers_cfg.TryGetValue("containers", out string containers_value))
                {
                    if (!string.IsNullOrEmpty(containers_value))
                    {
                        var containers_list = containers_value.Split(',');
                        foreach (var container in containers_list)
                        {
                            var asdf = ConvertToDictionary(deserilaizer.GetTag(container));
                        }
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
