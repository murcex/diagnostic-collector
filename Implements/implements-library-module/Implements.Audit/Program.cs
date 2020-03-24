namespace Audit
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    // Implements
    using Implements;
    using Implements.Utility;
    using Implements.Substrate;

    class Program
    {
        static void Main(string[] args)
        {

            var curImplVer = Conversion.GetAssemblyVersion("Implements");

            ///
            /// Logging
            ///

            var cfg = Log.GenerateConfig();
            cfg.LogName = "ExampleLog";

            Log.Initialize();

            Log.Info($"Current Implement.dll Version: {curImplVer}");
            Log.Info("");

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

            Console.WriteLine($"Enter Storage Blob Audit.");
            Console.ReadKey();

            var account = ""; // <= add storage key here
            var containerOne = "TestOne"; // <= add default container here (should not exist)
            var containerTwo = "TestTwo"; // <= add second container here (static)

            Console.WriteLine($"Init: {BlobStorageAssessor.Initialize(account, containerOne).GetAwaiter().GetResult()} (Assert: True)");

            Console.WriteLine($"Manual Check: Container is Created. \r\n");
            Console.ReadKey();

            var doc1v1 = BlobStorageAssessor.StringToByte("Document V1.");
            var doc1v2 = BlobStorageAssessor.StringToByte("Document V2.");
            var doc1v1Name = "Doc1.txt";
            var doc1v2Name = "Doc2.txt";

            var doc3Data = BlobStorageAssessor.StringToByte("Document 3.");
            var doc3 = "Doc3.txt";
            Console.WriteLine($"Add Doc3: {BlobStorageAssessor.AddDocument(doc3, doc3Data).GetAwaiter().GetResult()}");
            var doc4Data = BlobStorageAssessor.StringToByte("Document 4.");
            var doc4 = "Test\\Doc4.txt";
            Console.WriteLine($"Add Doc4: {BlobStorageAssessor.AddDocument(doc4, doc4Data).GetAwaiter().GetResult()}");

            Console.WriteLine($"Add Doc1: {BlobStorageAssessor.AddDocument(doc1v1Name, doc1v1).GetAwaiter().GetResult()}");
            var doc1Output = BlobStorageAssessor.GetDocument(doc1v1Name).GetAwaiter().GetResult();
            var doc1String = BlobStorageAssessor.ByteToString(doc1Output);
            Console.WriteLine($"Get Doc1: {doc1String}");
            Console.WriteLine($"Manual Check: Doc1 V1 Added. \r\n");
            Console.ReadKey();

            Console.WriteLine($"Update Doc1: {BlobStorageAssessor.UpdateDocument(doc1v1Name, doc1v2).GetAwaiter().GetResult()}");
            var doc2Output = BlobStorageAssessor.GetDocument(doc1v1Name).GetAwaiter().GetResult();
            var doc2String = BlobStorageAssessor.ByteToString(doc2Output);
            Console.WriteLine($"Get Doc1: {doc2String}");
            Console.WriteLine($"Manual Check: Doc1 Updated V2. \r\n");
            Console.ReadKey();

            Console.WriteLine($"Move Doc: {BlobStorageAssessor.MoveDocumentInContainer(doc1v1Name, doc1v2Name).GetAwaiter().GetResult()}");
            Console.WriteLine($"Manual Check: Move Doc1. \r\n");
            Console.ReadKey();

            Console.WriteLine($"Move Doc From Container: {BlobStorageAssessor.MoveDocumentFromContainer(containerTwo, doc1v2Name).GetAwaiter().GetResult()}");
            Console.WriteLine($"Manual Check: Move Doc2 From Container. \r\n");
            Console.ReadKey();

            //var docList = BlobStorageAssessor.GetDocumentList();
            //Console.WriteLine($"Doc List All.");
            //foreach (var doc in docList)
            //{
            //    Console.WriteLine(doc);
            //}

            //var docList2 = BlobStorageAssessor.GetDocumentList("Test\\");
            //Console.WriteLine($"Doc List Test dir.");
            //foreach (var doc in docList2)
            //{
            //    Console.WriteLine(doc);
            //}

            Console.WriteLine($"Delete Doc1: {BlobStorageAssessor.DeleteDocument(doc1v2Name).GetAwaiter().GetResult()}");
            Console.WriteLine($"Manual Check: Doc1 Deleted. \r\n");
            Console.ReadKey();

            Console.WriteLine($"Delete Container: {BlobStorageAssessor.DeleteContainer().GetAwaiter().GetResult()}");
            Console.WriteLine($"Manual Check: Container Deleted. \r\n");
            Console.ReadKey();
        }
    }
}