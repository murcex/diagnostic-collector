namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;

    class SelectTagsByKey
    {
        public static void Execute()
        {
            Console.WriteLine($"// List Indexes by Id");

            var selectIndexe =
                StorageProvider.SelectTagsByKey(
                    Configuration.Collection,
                    Configuration.DocumentName);

            foreach (var index in selectIndexe)
            {
                Console.WriteLine($"Id: {Configuration.DocumentName} Index: {index}");
            }

            Console.WriteLine("");
        }
    }
}
