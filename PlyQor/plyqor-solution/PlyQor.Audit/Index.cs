namespace PlyQor.Audit
{
    using PlyQor.Audit.Core;
    using PlyQor.Audit.TestCases.PlyClient;
    using PlyQor.Audit.TestCases.PlyManager;
    using PlyQor.Audit.TestCases.StorageProvider;
    using System;

    class Index
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting PlyQor Audit\r\n");

            try
            {
                Initializer.Execute();

                Console.WriteLine("-- Audit Settings -- ");
                Console.WriteLine($"StorageProviderTest = {Configuration.StorageProviderTest}");
                Console.WriteLine($"PlyManagerTest = {Configuration.PlyManagerTest}");
                Console.WriteLine($"PlyClientTest = {Configuration.PlyClientTest}");

                Console.WriteLine($"NanoCount = {Configuration.NanoCount}");
                Console.WriteLine($"MicroCount = {Configuration.MicroCount}");
                Console.WriteLine($"SmallCount = {Configuration.SmallCount}");
                Console.WriteLine($"MediumCount = {Configuration.MediumCount}");
                Console.WriteLine($"LargeCount = {Configuration.LargeCount}");

                Console.ReadKey();

                if (Configuration.StorageProviderTest)
                {
                    StorageProviderTestProvider.Execute();
                }

                if (Configuration.PlyManagerTest)
                {
                    PlyManagerTestProvider.Execute();
                }

                if (Configuration.PlyClientTest)
                {
                    PlyClientTestProvider.Execute();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();
        }
    }
}
