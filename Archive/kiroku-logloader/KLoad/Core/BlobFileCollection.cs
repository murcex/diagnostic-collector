namespace KLoad
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Global Blob File Collection. Contains pre-sorted group types. Example: Available for upload, ready for retention.
    /// </summary>
    public static class BlobFileCollection
    {
        internal static List<BlobFileModel> blobFileCollection = new List<BlobFileModel>();

        /// <summary>
        /// Add Blob File to Collection.
        /// </summary>
        /// <param name="file"></param>
        public static void AddFile(BlobFileModel file)
        {
            blobFileCollection.Add(file);
        }

        /// <summary>
        /// Get all Blob Files in Collection.
        /// </summary>
        /// <returns></returns>
        public static List<BlobFileModel> GetFiles()
        {
            return blobFileCollection;
        }

        /// <summary>
        /// Blob Files which are available for uploading.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BlobFileModel> CurrentExistFalseCount()
        {
            return blobFileCollection.Where(d => 
                d.Exist == false);
        }

        /// <summary>
        /// Blob Files ready for retention / deletion.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BlobFileModel> CurrentRetentionCount()
        {
            return blobFileCollection.Where(d => 
            d.Exist == false ||
            d.HeaderStatus == false ||
            d.FooterStatus == false);
        }
    }
}
