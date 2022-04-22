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

            SelectTags.Execute();

            SelectKeyList.Execute();

            SelectTagsByKey.Execute();

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

            // TODO: Retention?
        }
    }
}
