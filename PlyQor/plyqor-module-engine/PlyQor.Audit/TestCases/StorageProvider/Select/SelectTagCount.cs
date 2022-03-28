namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Engine.Components.Storage;
    using System;
    using PlyQor.Audit.Core;

    class SelectTagCount
    {
        public static void Execute()
        {
            Console.WriteLine($"// Count Ids by Index");

            var count =
                StorageProvider.SelectTagCount(
                    Configuration.Collection, 
                    Configuration.Tag_Upload);

            Console.WriteLine($"UPLOAD Count: {count}");
            Console.WriteLine("");
        }
    }
}
