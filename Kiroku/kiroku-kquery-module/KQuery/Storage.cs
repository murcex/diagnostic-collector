namespace KQuery
{
    using System.IO;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    class Storage
    {
        public static byte[] GetLog(string key)
        {
            byte[] document = null;

            try
            {
                // Parse connection string
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.StorageConnectionString);

                // Create the blob client
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve container reference -- create if it's not availble
                CloudBlobContainer container = blobClient.GetContainerReference("kiroku-archive");
                container.CreateIfNotExistsAsync().GetAwaiter();

                // Build KLOG _R_ead file name to use on the blob write
                var blobfileName = "KLOG_R_" + key + ".txt";

                // Retrieve blob reference -- example: containerName\$(localDir)\KLOG_R_$(guid).txt
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobfileName);

                // Check and then Rename local file after file has been sent to Azure Storage Blob
                Stream stream = new MemoryStream();

                //var klog = blockBlob.DownloadTextAsync().GetAwaiter();

                blockBlob.DownloadToStreamAsync(stream).Wait();

                byte[] buffer = new byte[16 * 1024];

                stream.Position = 0; // Add this line to set the input stream position to 0

                using (MemoryStream ms = new MemoryStream())
                {
                    int read;

                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    document = ms.ToArray();

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
