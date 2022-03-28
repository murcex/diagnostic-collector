namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;

    class SelectTags
    {
        public static void Execute()
        {
            Console.WriteLine($"// List Indexes");

            var listIndexes =
                StorageProvider.SelectTags(
                    Configuration.Collection);

            Console.WriteLine($"Indexes Count: { listIndexes.Count}");

            foreach (var listIndex in listIndexes)
            {
                Console.WriteLine($"Index: {listIndex}");
            }

            Console.WriteLine("");
        }
    }
}
