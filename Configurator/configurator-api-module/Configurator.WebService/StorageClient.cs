namespace Configurator.WebService
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.IO;

    class StorageClient
    {
        private static CloudStorageAccount _blobStorageAccount;

        private static CloudBlobClient _blobClient;

        private static CloudBlobContainer _blobContainer;

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
                    //document = Encoding.ASCII.GetBytes("Blob Doesn't exist.");

                    return document;
                }
            }
            catch
            {
                //document = Encoding.ASCII.GetBytes("Get Document Exception.");

                return document;
            }
        }
    }
}
