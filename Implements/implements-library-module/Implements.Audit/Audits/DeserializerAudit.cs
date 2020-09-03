namespace Implements.Audit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Implements.Deserializer;

    class DeserializerAudit
    {
        public static void Execute(bool execute)
        {
            if (!execute)
            {
                Console.WriteLine($"Audit: {nameof(DeserializerAudit)} has not been marked for execution, skipping audit.");
                Console.WriteLine($"*** PRESS ANY KEY TO CONTINUE *** \r\n");
                Console.ReadKey();

                return;
            }

            ///
            /// Deserializer Example
            ///

            Dictionary<string, List<KeyValuePair<string, string>>> test_collection;
            List<KeyValuePair<string, string>> test_tag;
            string test_value1, test_value2;
            List<string> test_values;

            try
            {
                using (Deserializer deserializer = new Deserializer())
                {
                    var configPath = Directory.GetCurrentDirectory() + @"\MyConfigFile.ini";
                    deserializer.Execute(configPath, true, true);

                    test_collection = deserializer.GetCollection();
                    test_tag = deserializer.GetTag("app_first");
                    test_value1 = deserializer.GetValue("app_first", "appname");
                    test_value2 = deserializer.GetValue("app_second", "type");
                    test_values = deserializer.GetValues("apps_index", "app");
                }

                Console.WriteLine("");
                Console.WriteLine("--- Program.cs Check ---");
                Console.WriteLine("");
                Console.WriteLine("-> Collection Check:");

                foreach (var item in test_collection)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Tag: {item.Key}");

                    foreach (var pair in item.Value)
                    {
                        Console.WriteLine($"Key: {pair.Key} = Value: {pair.Value}");
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("-> Tag Check:");
                Console.WriteLine("");

                foreach (var kvp in test_tag)
                {
                    Console.WriteLine($"Key: {kvp.Key} = Value: {kvp.Value}");
                }

                Console.WriteLine("");
                Console.WriteLine("-> Value Check:");
                Console.WriteLine("");
                Console.WriteLine($"Value 1: {test_value1}");
                Console.WriteLine($"Value 2: {test_value2}");
                Console.WriteLine("");
                Console.WriteLine("-> Values Check:");
                Console.WriteLine("");
                var counter = 1;
                foreach (var item in test_values)
                {
                    Console.WriteLine($"Value {counter}: {item}");
                    counter++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Deserializer Exception: {e.ToString()}");
            }
        }
    }
}
