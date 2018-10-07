using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace KLOGLoader
{
    public static class BlobClient
    {
        //private static GlobalBlobClient _instance;
        static CloudStorageAccount _blobStorageAccount { get; set; }

        static CloudBlobClient _blobClient { get; set; }

        public static CloudBlobContainer BlobContainer { get; set; }

        public static void Set()
        {
            _blobStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Global.AzureContainer);
        }

        public static CloudBlob GetPayload(string file)
        {
            _blobStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Global.AzureContainer);

            return BlobContainer.GetBlobReference(file);
        }

        public static string DeleteBlobFile(string file)
        {
            _blobStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            _blobClient = _blobStorageAccount.CreateCloudBlobClient();
            BlobContainer = _blobClient.GetContainerReference(Global.AzureContainer);

            var _blobFile = BlobContainer.GetBlobReference(file);

            _blobFile.Delete();

            return "Success";
        }
    }
}
