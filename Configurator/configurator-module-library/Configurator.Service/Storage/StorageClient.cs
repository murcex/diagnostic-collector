namespace Configurator.Service
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.IO;

    class StorageClient
    {
        /// <summary>
        /// Blob storage account.
        /// </summary>
        private static CloudStorageAccount _blobStorageAccount;

        /// <summary>
        /// Blob storage client.
        /// </summary>
        private static CloudBlobClient _blobClient;

        /// <summary>
        /// Blob storage container.
        /// </summary>
        private static CloudBlobContainer _blobContainer;

        /// <summary>
        /// Initilize storage blob client.
        /// </summary>
        /// <param name="storageAccount"></param>
        /// <param name="storageContainer"></param>
        /// <returns></returns>
        public static bool Initialize(string storageAccount, string storageContainer)
        {
            try
            {
                _blobStorageAccount = CloudStorageAccount.Parse(storageAccount);
                _blobClient = _blobStorageAccount.CreateCloudBlobClient();
                _blobContainer = _blobClient.GetContainerReference(storageContainer);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get a document from storage blob by file name.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] GetDocument(string file)
        {
            byte[] document = null;

            try
            {
                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);
                var task = blockBlob.ExistsAsync();
                task.Wait();

                if (task.Result)
                {
                    Stream stream = new MemoryStream();

                    blockBlob.DownloadToStreamAsync(stream).Wait();

                    byte[] buffer = new byte[16 * 1024];

                    stream.Position = 0;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        return ms.ToArray();
                    }
                }
                else
                {
                    return document;
                }
            }
            catch
            {
                return document;
            }
        }
    }
}