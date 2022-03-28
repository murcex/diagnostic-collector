namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;

    class SelectKeyList
    {
        public static void Execute()
        {
            Console.WriteLine($"// List Keys By Index");

            var Ids =
                StorageProvider.SelectKeyList(
                    Configuration.Collection,
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
