namespace KLoad
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    /// Global Azure Storage Blob Client.
    /// </summary>
    public static class BlobClient
    {
        /// <summary>
        /// Azure Storage Blob Container.
        /// </summary>
        public static CloudBlobContainer BlobContainer { get; set; }

        /// <summary>
        /// Set Azure Storage Blob Client.
        /// </summary>
        public static void Set()
        {
            var _blobStorageAccount = CloudStorageAccount.Parse(Configuration.AzureStorage);
            var _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Configuration.AzureContainer);
        }

        /// <summary>
        /// Get Blob File From Azure Stroge Blob.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static CloudBlob GetDocument(string file)
        {
            var _blobStorageAccount = CloudStorageAccount.Parse(Configuration.AzureStorage);
            var _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Configuration.AzureContainer);

            return BlobContainer.GetBlobReference(file);
        }

        /// <summary>
        /// Delete Blob File from Azure Storage Blob.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string DeleteBlobFile(string file)
        {
            var _blobStorageAccount = CloudStorageAccount.Parse(Configuration.AzureStorage);
            var _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Configuration.AzureContainer);

            var _blobFile = BlobContainer.GetBlobReference(file);

            _blobFile.DeleteAsync().GetAwaiter().GetResult();

            return "Success";
        }
    }
}
