namespace Implements.Audit
{
    using System;

    class Index
    {
        // Audit switches
        private static bool Log = true;

        private static bool Deserializer = true;

        private static bool Encryption = true;

        static void Main(string[] args)
        {
            LogAudit.Execute(Log);
            Break();

            DeserializerAudit.Execute(Deserializer);
            Break();

            EncryptionAudit.Execute(Encryption);
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