namespace KLOGLoader
{
    using System.Collections.Generic;
    using System.Linq;

    public static class BlobFileCollection
    {
        internal static List<BlobFileModel> blobFileCollection = new List<BlobFileModel>();

        public static void AddFile(BlobFileModel file)
        {
            blobFileCollection.Add(file);
        }

        public static List<BlobFileModel> GetFiles()
        {
            return blobFileCollection;
        }

        public static IEnumerable<BlobFileModel> CurrentExistFalseCount()
        {
            return blobFileCollection.Where(d => 
            d.Exist == false);
        }

        public static IEnumerable<BlobFileModel> CurrentRetentionCount()
        {
            return blobFileCollection.Where(d => 
            d.Exist == false ||
            d.HeaderStatus == false ||
            d.FooterStatus == false);
        }

        public static void UpdateBlobFileStatus(string type, bool property)
        {

        }
    }
}
