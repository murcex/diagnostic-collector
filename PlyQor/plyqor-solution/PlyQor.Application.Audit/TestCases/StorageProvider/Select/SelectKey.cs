namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Audit.Core;
    using PlyQor.Engine.Components.Storage;
    using System;

    class SelectKey
    {
        public static void Execute()
        {
            Console.WriteLine($"// Select");

            var data =
                StorageProvider.SelectKey(
                    Configuration.Container,
                    Configuration.DocumentName);

            Console.WriteLine($"Check if document is not null (False): {Equals(data, null)}");
            Console.WriteLine($"Select Document: {data.Length}");
            Console.WriteLine($"Documents match (True): {Equals(data.Length, Configuration.DocumentLength)}");

            Console.WriteLine("");
        }
    }
}
