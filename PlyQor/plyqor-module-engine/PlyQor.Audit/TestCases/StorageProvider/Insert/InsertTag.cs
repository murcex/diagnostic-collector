namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;
    using PlyQor.Audit.Ultilties;

    class InsertTag
    {
        public static void Execute()
        {
            Console.WriteLine("// Insert Additional Index");

            var addIndexId = GetTestIndex.Execute();

            StorageProvider.InsertTag(Configuration.Container, addIndexId, "AUDIT");

            //TODO: check -> select tag insert
            var selectIndexe = StorageProvider.SelectKeyTags(Configuration.Container, addIndexId);

            bool Hit = false;

            foreach (var index in selectIndexe)
            {
                if (index.Contains("AUDIT"))
                {
                    Hit = true;
                }
            }

            Console.WriteLine($"Adding Audit Index on {addIndexId}");
            Console.WriteLine($"Confirm Audit Index exists (True): {Equals(true, Hit)}");

            Console.WriteLine($"");
        }
    }
}
