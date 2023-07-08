namespace PlyQor.Audit.TestCases.StorageProvider
{
    using PlyQor.Audit.Core;
    using PlyQor.Audit.Ultilties;
    using PlyQor.Engine.Components.Storage;
    using System;

    class InsertTag
    {
        public static void Execute()
        {
            Console.WriteLine("// Insert Additional Index");

            var addIndexId = GetTestIndex.Execute();

            StorageProvider.InsertTag(Configuration.Container, addIndexId, "AUDIT");

            var selectIndexes = StorageProvider.SelectKeyTags(Configuration.Container, addIndexId);

            bool hitTracker = false;

            foreach (var index in selectIndexes)
            {
                if (index.Contains("AUDIT"))
                {
                    hitTracker = true;
                }
            }

            Console.WriteLine($"Adding Audit Index on {addIndexId}");
            Console.WriteLine($"Confirm Audit Index exists (True): {Equals(true, hitTracker)}");

            Console.WriteLine($"");
        }
    }
}
