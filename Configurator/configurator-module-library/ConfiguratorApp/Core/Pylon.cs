using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.Core
{
    class Pylon
    {
        public static Dictionary<string, Dictionary<string, string>> Execute()
        {
            var pylonConfigString = GetPylonValue();

            var pylonConfigDictionary = ConvertToDictionary(pylonConfigString);

            return pylonConfigDictionary;
        }

        /// <summary>
        /// Get Pylon config string from environment variable
        /// </summary>
        private static string GetPylonValue()
        {
            string pylonValue = string.Empty;

            try
            {
                pylonValue = Environment.GetEnvironmentVariable("PYLON", EnvironmentVariableTarget.Process);
            }
            catch
            { }

            return pylonValue;
        }

        /// <summary>
        /// Convert Pylon config string to dictionary
        /// </summary>
        private static Dictionary<string, Dictionary<string, string>> ConvertToDictionary(string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(input);
            }
            catch (Exception ex)
            {
                return new Dictionary<string, Dictionary<string, string>>();
            }
        }

        /// <summary>
        /// Convert dictionary to legacy kvp list
        /// </summary>
        public static List<KeyValuePair<string, string>> ConvertToTag(
            Dictionary<string, Dictionary<string, string>> dictionary,
            string index)
        {
            List<KeyValuePair<string, string>> kvps = new List<KeyValuePair<string, string>>();

            if (dictionary.TryGetValue("", out Dictionary<string, string> subset))
            {
                foreach (var record in subset)
                {
                    KeyValuePair<string, string> kvp =
                        new KeyValuePair<string, string>(record.Key, record.Value);

                    kvps.Add(kvp);
                }
            }

            return kvps;
        }
    }
}
