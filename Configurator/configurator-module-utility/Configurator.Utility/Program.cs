namespace Configurator.Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Implements.Deserializer;
    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfigLocation(args);

            Dictionary<string, Dictionary<string, string>> dictionary = 
                new Dictionary<string, Dictionary<string, string>>();

            using (Deserializer deserializer = new Deserializer())
            {
                deserializer.Execute(config);

                List<KeyValuePair<string, string>> configurator = 
                    deserializer.GetTag(Configuration.Configurator);

                if (configurator == null || configurator.Count < 1)
                {
                    Console.WriteLine("[configurator] is missing.");

                    Environment.Exit(0);
                }

                dictionary.Add(Configuration.Configurator.ToUpper(), LoadDictionary(configurator));

                if (CheckConfiguratorType(dictionary, out string type))
                {
                    if (type.ToUpper() == Configuration.External.ToUpper())
                    {
                        List<KeyValuePair<string, string>> externalType = 
                            deserializer.GetTag(Configuration.External);

                        dictionary.Add(Configuration.External.ToUpper(), LoadDictionary(externalType));
                    }

                    if (type.ToUpper() == Configuration.Internal.ToUpper())
                    {
                        List<KeyValuePair<string, string>> internalType = 
                            deserializer.GetTag(Configuration.Internal);

                        dictionary.Add(Configuration.Internal.ToUpper(), LoadDictionary(internalType));

                        var links = GetLinkValues(dictionary);

                        if (links.Length > 0 && links != null)
                        {
                            foreach (var link in links)
                            {
                                var tag = deserializer.GetTag(link.ToLower());

                                dictionary.Add(link.ToUpper(), LoadDictionary(tag));
                            }
                        }
                    }
                }
            }

            var dictionaryJson = JsonConvert.SerializeObject(dictionary);

            string output = Path.Combine(Directory.GetCurrentDirectory(), $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}-OUTPUT.txt");

            using (StreamWriter file = new StreamWriter(output, true))
            {
                file.WriteLine(dictionaryJson);
            }

            Environment.Exit(0);
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

            if (kvps != null && kvps.Count > 0)
            {

                foreach (var kvp in kvps)
                {
                    dictionary.Add(kvp.Key.ToUpper(), kvp.Value);
                }
            }

            return dictionary;
        }

        public static bool CheckConfiguratorType(
            Dictionary<string, Dictionary<string, string>> dictionary, 
            out string typeValue)
        {
            if (dictionary.TryGetValue(Configuration.Configurator.ToUpper(), 
                out Dictionary<string, string> configurator))
            {
                if (configurator.TryGetValue(Configuration.Type.ToUpper(), out typeValue))
                {
                    if (string.IsNullOrEmpty(typeValue))
                    {
                        return false;
                    }

                    return true;
                }

                typeValue = "ERR_2";
                return false;
            }
            else
            {
                typeValue = "ERR_1";
                return false;
            }
        }

        public static string[] GetLinkValues(Dictionary<string,Dictionary<string,string>> dictionary)
        {
            string[] links = null;

            if (dictionary.TryGetValue(Configuration.Internal.ToUpper(), 
                out Dictionary<string,string> internalDictionary))
            {
                if (internalDictionary.TryGetValue(Configuration.Link.ToUpper(), 
                    out string link_ouput))
                {
                    links = link_ouput.Split(",");
                }
            }

            return links;
        }
    }
}
