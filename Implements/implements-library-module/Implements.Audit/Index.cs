using Implements.Substrate.Blob;
using System;
using System.Linq.Expressions;

namespace Implements.Audit
{
    class Index
    {
        // Audit switches
        private static bool Log = true;

        private static bool Deserializer = true;

        private static bool Encryption = true;

        private static bool BlobClient = false;

        static void Main(string[] args)
        {
            LogAudit.Execute(Log);
            Break();

            DeserializerAudit.Execute(Deserializer);
            Break();

            EncryptionAudit.Execute(Encryption);
            Break();

            BlobClientAudit.Execute(BlobClient);
            Break();
        }

        private static void Break()
        {
            Console.WriteLine($"");
            Console.WriteLine($"--- Audit Completed ---");
            Console.ReadKey();
            Console.Clear();
        }
    }
}