namespace PlyQor.Audit.TestCases.StorageProvider
{
    using System;
    using System.Linq;
    using PlyQor.Engine.Components.Storage;
    using PlyQor.Audit.Core;

    class UpdateTagByKey
    {
        public static void Execute()
        {
            Console.WriteLine($"// Update Id Index");

            var indexes = StorageProvider.SelectTags(Configuration.Container);

            string targetIndex = null;
            string checkForStage = "Stage";
            string newIndex = "ACTIVE";

            foreach (var index in indexes)
            {
                if (index.Contains(checkForStage.ToUpper()))
                {
                    targetIndex = index;

                    break;
                }
            }

            var testUpdateIdList = StorageProvider.SelectKeyList(Configuration.Container, targetIndex, 1);

            var targetId = testUpdateIdList.FirstOrDefault();

            StorageProvider.UpdateKeyTag(Configuration.Container, targetId, targetIndex, newIndex);

            // list all tags for key
            var indexes2 = StorageProvider.SelectKeyTags(Configuration.Container, targetId);

            // check if new and old tag exist
            var checkIndex = string.Empty;
            bool NoHit = false;
            foreach (var index in indexes2)
            {
                if (index.Contains(newIndex))
                {
                    checkIndex = index;
                }

                if (Equals(index, targetIndex))
                {
                    NoHit = true;
                }
            }

            Console.WriteLine($"Update Index {targetIndex} to {newIndex}");
            Console.WriteLine($"Tag exists (True): {Equals(newIndex, checkIndex)}");
            Console.WriteLine($"Old tag doesn't exists (True): {Equals(NoHit, false)}");
            Console.WriteLine($"");
        }
    }
}
