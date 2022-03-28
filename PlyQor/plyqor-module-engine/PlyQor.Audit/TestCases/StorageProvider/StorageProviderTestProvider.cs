namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;

    class StorageProviderTestProvider
    {
        public static void Execute()
        {
            Console.WriteLine("");

            // Insert

            InsertKey.Execute();

            InsertTag.Execute();

            // Selete

            SelectKey.Execute();

            // List

            SelectTags.Execute();

            SelectKeyList.Execute();

            SelectTagsByKey.Execute();

            // Count

            SelectTagCount.Execute();

            // Update

            UpdateKey.Execute();

            UpdateData.Execute();

            UpdateTagByKey.Execute();

            UpdateTag.Execute();

            // Delete

            DeleteKeyAndTags.Execute();

            DeleteTagByKey.Execute();

            DeleteTag.Execute();
        }
    }
}
