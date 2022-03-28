namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;

    class SelectKey
    {
        public static void Execute()
        {
            Console.WriteLine($"// Select");

            var data =
                StorageProvider.SelectKey(
                    Configuration.Collection,
                    Configuration.DocumentName);

            Console.WriteLine($"Check if document is not null (False): {Equals(data, null)}");
            Console.WriteLine($"Select Document: {data.Length}");
            Console.WriteLine($"Documents match (True): {Equals(data.Length, Configuration.DocumentLength)}");

            Console.WriteLine("");
        }
    }
}
