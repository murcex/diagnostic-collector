namespace PlyQor.Audit.Ultilties
{
    using PlyQor.Engine.Components.Storage;
    using System.Linq;
    using PlyQor.Audit.Core;

    class Ultilty
    {
        public static string GetTestIndex()
        {
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

            return testUpdateIdList.FirstOrDefault();
        }
    }
}
