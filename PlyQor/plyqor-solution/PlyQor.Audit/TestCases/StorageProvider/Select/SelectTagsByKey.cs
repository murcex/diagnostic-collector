namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Audit.Core;
    using PlyQor.Engine.Components.Storage;
    using System;

    class SelectTagsByKey
    {
        public static void Execute()
        {
            Console.WriteLine($"// List Indexes by Id");

            var selectIndexe =
                StorageProvider.SelectKeyTags(
                    Configuration.Container,
                    Configuration.DocumentName);

            foreach (var index in selectIndexe)
            {
                Console.WriteLine($"Id: {Configuration.DocumentName} Index: {index}");
            }

            Console.WriteLine("");
        }
    }
}
