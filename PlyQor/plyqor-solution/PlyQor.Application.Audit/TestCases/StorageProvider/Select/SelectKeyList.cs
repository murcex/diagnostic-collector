namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Audit.Core;
    using PlyQor.Engine.Components.Storage;
    using System;

    class SelectKeyList
    {
        public static void Execute()
        {
            Console.WriteLine($"// List Keys By Index");

            var Ids =
                StorageProvider.SelectKeyList(
                    Configuration.Container,
                    Configuration.Tag_Upload,
                    5);

            foreach (var id in Ids)
            {
                Console.WriteLine($"UPLOAD Id: {id}");
            }

            Console.WriteLine("");
        }
    }
}
