namespace Implements.Audit
{
    using System;
    using System.Collections.Generic;
    using Implements.Encryption;

    class EncryptionAudit
    {
        public static void Execute(bool execute)
        {
            if (!execute)
            {
                return;
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
        }
    }
}
