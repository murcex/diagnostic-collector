namespace Audit
{
    using System;
    using System.Collections.Generic;

    // Implements
    using Implements;

    class Program
    {
        static void Main(string[] args)
        {
            ///
            /// Logging
            ///

            Log.Initialize();
            
            ///
            /// Deserializer Example
            ///

            Dictionary<string, List<KeyValuePair<string, string>>> test_collection;
            List<KeyValuePair<string, string>> test_tag;
            string test_value1,test_value2;
            List<string> test_values;

            try
            {
                using (Deserializer deserializer = new Deserializer())
                {
                    deserializer.Execute(@"C:\Temp\MyConfig\MyConfigFile.txt", true, true);

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

            try
            {
                ///
                /// Encryption.Encrypt();
                ///

                var key = "yourstringpasswordhere";
                var iv = "16charlongivonly";
                List<string> encrList;

                List<string> myLine = new List<string>();
                myLine.Add("Test Line 1");
                myLine.Add("Test Line 2");

                using (Encryption encr_1 = new Encryption(key, iv))
                {

                    encrList = encr_1.Encrypt(myLine);

                    Console.WriteLine("");
                    Console.WriteLine("-- Encrypt Test --");
                    Console.WriteLine("");

                    foreach (string line in encrList)
                    {
                        Console.WriteLine($"Contents: {line}");
                    }
                }

                ///
                /// Encryption.Decrypt();
                ///
                
                using (Encryption encr_2 = new Encryption(key, iv))
                {

                    var myNewList = encr_2.Decrypt(encrList);

                    Console.WriteLine("");
                    Console.WriteLine("-- Decrypt Test --");
                    Console.WriteLine("");

                    foreach (string line in myNewList)
                    {
                        Console.WriteLine($"Contents: {line}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Encryption Exception: {e.ToString()}");
            }

            Console.ReadKey();
        }
    }
}
