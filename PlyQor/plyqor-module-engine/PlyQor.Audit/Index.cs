namespace PlyQor.Audit
{
    using System;
    using PlyQor.Audit.Core;
    using PlyQor.Audit.TestCases.StorageProvider;
    using PlyQor.Audit.TestCases.PlyManager;
    using PlyQor.Audit.TestCases.PlyClient;
    using PlyQor.Engine;

    class Index
    {
        static void Main(string[] args)
        {
            var testStorageProvider = true;

            var testPlyManager = true;

            var testPlyClient = true;

            Console.WriteLine("");

            Initializer.Execute();

            if (testStorageProvider)
            {
                StorageProviderTestProvider.Execute();
            }

            if (testPlyManager)
            {
                PlyManagerTestProvider.Execute();
            }

            if (testPlyClient)
            {
                PlyClientTestProvider.Execute();
            }

            Console.WriteLine("");

            Console.WriteLine(" \n\r// DataRetention");
            PlyQorManager.Retention();

            Console.WriteLine("");

            Console.ReadKey();
        }
    }
}
