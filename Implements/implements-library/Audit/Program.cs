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

            Config cfg = new Config();

            try
            {
                using (Deserializer deserializer = new Deserializer())
                {
                    cfg.Collection = deserializer.Execute(@"C:\Temp\MyConfig\MyConfigFile.txt", true, true);

                    Console.WriteLine("");
                    Console.WriteLine("--- Program.cs Check ---");

                    foreach (var item in cfg.Collection)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"Tag: {item.Key}");

                        foreach (var pair in item.Value)
                        {
                            Console.WriteLine($"PartA: {pair.A} = PartB: {pair.B}");
                        }
                    }
                }

                var test1 = cfg.GetValue("app_first", "appname");
                var test2 = cfg.GetValue("app_second", "type");
                var test3 = cfg.GetValues("apps_index", "app");

                Console.WriteLine("");
                Console.WriteLine("Test Value Select:");
                Console.WriteLine($"Test Select: {test1}");
                Console.WriteLine($"Test Select: {test2}");
                Console.WriteLine("");
                Console.WriteLine("Test Values Select:");
                foreach (var item in test3)
                {
                    Console.WriteLine($"Test Select: {item}");
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
