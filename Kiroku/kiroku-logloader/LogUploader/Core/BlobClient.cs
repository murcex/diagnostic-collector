namespace KLOGLoader
{
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public static class BlobClient
    {
        public static CloudBlobContainer BlobContainer { get; set; }

        public static void Set()
        {
            //_blobStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var _blobStorageAccount = CloudStorageAccount.Parse(Global.AzureStorage);
            var _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Global.AzureContainer);
        }

        public static CloudBlob GetPayload(string file)
        {
            //var _blobStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var _blobStorageAccount = CloudStorageAccount.Parse(Global.AzureStorage);
            var _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Global.AzureContainer);

            return BlobContainer.GetBlobReference(file);
        }

        public static string DeleteBlobFile(string file)
        {
            //var _blobStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var _blobStorageAccount = CloudStorageAccount.Parse(Global.AzureStorage);
            var _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Global.AzureContainer);

            var _blobFile = BlobContainer.GetBlobReference(file);

            _blobFile.Delete();

            return "Success";
        }
    }
}
