namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using System.Linq;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;

    class DeleteTagByKey
    {
        public static void Execute()
        {
            Console.WriteLine("// Delete Index Id");

            var indexes = StorageProvider.SelectTags(Configuration.Collection);

            string targetIndex = null;
            string checkForStage = Configuration.Tag_Stage;

            foreach (var index in indexes)
            {
                if (index.Contains(checkForStage.ToUpper()))
                {
                    targetIndex = index;

                    break;
                }
            }

            var testUpdateIdList = StorageProvider.SelectKeyList(Configuration.Collection, targetIndex, 1);

            var testDeleteId = testUpdateIdList.FirstOrDefault();

            StorageProvider.DeleteTagByKey(
                Configuration.Collection,
                testDeleteId,
                targetIndex);

            var checkSelectIndexes = 
                StorageProvider.SelectTagsByKey(
                    Configuration.Collection, 
                    testDeleteId);

            bool NoHit = false;
            foreach (var index in checkSelectIndexes)
            {
                if (index.Contains(targetIndex))
                {
                    NoHit = true;
                }

                Console.WriteLine($"Index: {index}");
            }

            Console.WriteLine($"Check Delete Id Index (True): {Equals(NoHit, false)}");
            Console.WriteLine($"Removing Index {targetIndex} on Storage Id {testDeleteId}");
            Console.WriteLine($"");
        }
    }
}
