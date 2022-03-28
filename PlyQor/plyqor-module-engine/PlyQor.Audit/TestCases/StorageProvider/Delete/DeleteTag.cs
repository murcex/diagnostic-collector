namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;
    class DeleteTag
    {
        public static void Execute()
        {
            Console.WriteLine($"// Delete Index Set");
            var indexes = StorageProvider.SelectTags(Configuration.Collection);

            string targetIndex = null;
            string checkForStage = "Stage";

            foreach (var index in indexes)
            {
                if (index.Contains(checkForStage.ToUpper()))
                {
                    targetIndex = index;

                    break;
                }
            }

            StorageProvider.DeleteTag(Configuration.Collection, targetIndex);

            var indexes2 = StorageProvider.SelectTags(Configuration.Collection);

            bool NoHit = true;

            foreach (var index in indexes2)
            {
                if (index.Contains(targetIndex))
                {
                    NoHit = false;
                    break;
                }
            }

            Console.WriteLine($"Removing Index: {targetIndex}");
            Console.WriteLine($"Index removed (True): {Equals(true, NoHit)}");
            Console.WriteLine($"");
        }
    }
}
