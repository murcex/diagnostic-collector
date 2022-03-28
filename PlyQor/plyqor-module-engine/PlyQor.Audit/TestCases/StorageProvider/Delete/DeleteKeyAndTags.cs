namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Engine.Components.Storage;
    using System;
    using PlyQor.Audit.Core;
    using PlyQor.Audit.Ultilties;

    class DeleteKeyAndTags
    {
        public static void Execute()
        {
            Console.WriteLine($"// Delete Key and Tags");
            
            var deleteId = Ultilty.GetTestIndex();

            StorageProvider.DeleteKey(Configuration.Collection, deleteId);
            StorageProvider.DeleteTagsByKey(Configuration.Collection, deleteId); // DeleteTagsOnKey

            var checkDeleteKey = StorageProvider.SelectKey(Configuration.Collection, deleteId);
            var checkDeleteIndex = StorageProvider.SelectTagsByKey(Configuration.Collection, deleteId);

            Console.WriteLine($"Check Key is null (True): {string.IsNullOrEmpty(checkDeleteKey)}");
            Console.WriteLine($"Check Tags are empty (True): {Equals(checkDeleteIndex.Count, 0)}");
            Console.WriteLine($"Deleted {deleteId} from Storage and Tag");
            Console.WriteLine($"");
        }
    }
}
