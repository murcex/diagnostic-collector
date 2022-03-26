namespace Implements.Audit
{
    using System;

    class Index
    {
        // Audit switches
        private static bool Log = false;

        private static bool Deserializer = false;

        private static bool Encryption = false;

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