using System;
using System.Collections.Generic;
using System.Text;

namespace Configurator.Storage.Core
{
    class Pylon
    {
        public static Dictionary<string, string> GetConfiguration()
        {
            var pylonConfigString = GetPylonValue();

            var pylonConfigDictionary = ConvertToDictionary();

            var kirokuConfig = ConvertToTag(pylonConfigDictionary, "Kiroku");

            var ConfiguratorConfig = ConvertToTag(pylonConfigDictionary, "Configurator");
        }


        public static string GetPylonValue
        {
            get
            {
                string pylonValue = string.Empty;

                try
                {
                    pylonValue = Environment.GetEnvironmentVariable("PYLON_VALUE", EnvironmentVariableTarget.Process);
                }
                catch
                { }

                return pylonValue;
            }
        }

        public static List<KeyValuePair<string, string>> ConvertToTag(Dictionary<string, string> dictionary)
        {
            List<KeyValuePair<string, string>> kvps = new List<KeyValuePair<string, string>>();

            foreach (var record in dictionary)
            {
                KeyValuePair<string, string> kvp =
                    new KeyValuePair<string, string>(record.Key, record.Value);

                kvps.Add(kvp);
            }

            return kvps;
        }
    }
}
