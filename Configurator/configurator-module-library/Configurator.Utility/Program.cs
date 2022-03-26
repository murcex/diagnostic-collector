using System;
using System.Collections.Generic;
using System.IO;
using Implements.Deserializer;
using Newtonsoft.Json;

namespace Configurator.Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfigLocation(args);

            Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();

            using (Deserializer deserializer = new Deserializer())
            {
                deserializer.Execute(config);

                var configurator = deserializer.GetTag("configurator");

                dictionary.Add("Configurator", LoadDictionary(configurator));

                var remote = deserializer.GetTag("remote");

                dictionary.Add("Remote", LoadDictionary(remote));

                var embedded = deserializer.GetTag("embedded");

                dictionary.Add("Embedded", LoadDictionary(embedded));
            }

            // create output single string
            var jsonString = JsonConvert.SerializeObject(dictionary);

            string output = Path.Combine(Directory.GetCurrentDirectory(), $"OUTPUT.txt");

            using (StreamWriter file = new StreamWriter(output, true))
            {
                file.WriteLine(jsonString);
            }
        }

        public static string GetConfigLocation(string[] args)
        {
            string config = Path.Combine(Directory.GetCurrentDirectory(), $"Config.ini");

            if (args == null || args.Length == 0 || args.Length > 1)
            {
                return config;
            }
            else
            {
                var path = args[0];

                if (File.Exists(path))
                {
                    return path;
                }
            }

            return config;
        }

        public static Dictionary<string, string> LoadDictionary(List<KeyValuePair<string, string>> kvps)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (var kvp in kvps)
            {
                dictionary.Add(kvp.Key, kvp.Value);
            }

            return dictionary;
        }

        public static Dictionary<string, string> CheckRemoteTag(KeyValuePair<string, string> kvps)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();



            return dictionary;
        }
    }
}
