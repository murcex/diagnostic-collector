namespace Implements.Substrate
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobStorageAssessor
    {
        /// <summary>
        /// Storage account details; name and key.
        /// </summary>
        private static string _account;

        /// <summary>
        /// Container client, by way of storage account client.
        /// </summary>
        private static CloudBlobContainer _blobContainer;

        /// <summary>
        /// Initialize the blob storage client with account and container details.
        /// </summary>
        /// <param name="storageAccount"></param>
        /// <param name="storageContainer"></param>
        /// <returns></returns>
        public static async Task<bool> Initialize(string storageAccount, string storageContainer)
        {
            try
            {
                _account = storageAccount;

                _blobContainer = CloudStorageAccount.Parse(storageAccount)
                    .CreateCloudBlobClient()
                    .GetContainerReference(storageContainer.ToLower());

                await CreateContainer();
                return await CheckContainer();
            }
            catch
            {
                return false;
            }
        }

        /// ---
        /// Single document CRUD commands.
        /// ---

        /// <summary>
        /// Add a document to the default blob storage container.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static async Task<bool> AddDocument(string file, byte[] document)
        {
            try
            {
                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);

                if (!await blockBlob.ExistsAsync())
                {
                    await blockBlob.UploadFromByteArrayAsync(document, 0, document.Length);

                    if (await blockBlob.ExistsAsync())
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get a document from the default blob storage container.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetDocument(string file)
        {
            byte[] document = null;

            try
            {
                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);

                if (await blockBlob.ExistsAsync())
                {
                    Stream stream = new MemoryStream();

                    await blockBlob.DownloadToStreamAsync(stream);

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

        /// <summary>
        /// Update a document on the default blob storage container.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateDocument(string file, byte[] document)
        {
            CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);

            try
            {
                if (!await blockBlob.ExistsAsync())
                {
                    await blockBlob.UploadFromByteArrayAsync(document, 0, document.Length);

                    if (await blockBlob.ExistsAsync())
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    if (await DeleteDocument(file))
                    {
                        await blockBlob.UploadFromByteArrayAsync(document, 0, document.Length);

                        if (await blockBlob.ExistsAsync())
                        {
                            return true;
                        }

                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a document from the default blob storage container.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteDocument(string file)
        {
            try
            {
                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);

                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();

                    if (!await blockBlob.ExistsAsync())
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// ---
        /// Single document moves, between and within the same container.
        /// ---

        /// <summary>
        /// Move a document from default container to another target container.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<bool> MoveDocumentFromContainer(string container, string file)
        {
            try
            {
                var sourceBlobContainer = _blobContainer;
                var targetBlobContainer = CloudStorageAccount.Parse(_account)
                    .CreateCloudBlobClient()
                    .GetContainerReference(container.ToLower());

                // Get document from source
                var sourceDocument = await GetDocument(file);

                // Add document to new container
                var uploadStatus = false;

                try
                {
                    CloudBlockBlob targetBlockBlob = targetBlobContainer.GetBlockBlobReference(file);

                    if (!await targetBlockBlob.ExistsAsync())
                    {
                        await targetBlockBlob.UploadFromByteArrayAsync(sourceDocument, 0, sourceDocument.Length);

                        if (await targetBlockBlob.ExistsAsync())
                        {
                            uploadStatus = true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

                // Delete document from source once add is finished
                if (uploadStatus)
                {
                    var deleOp = await DeleteDocument(file);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Move a document within the default container to another path.
        /// </summary>
        /// <param name="currentFile"></param>
        /// <param name="newFile"></param>
        /// <returns></returns>
        public static async Task<bool> MoveDocumentInContainer(string currentFile, string newFile)
        {
            try
            {
                bool addOp = false;

                var getDoc = await GetDocument(currentFile);

                if (getDoc != null)
                {
                    addOp = await AddDocument(newFile, getDoc);

                    if (addOp)
                    {
                        var delOp = await DeleteDocument(currentFile);

                        if (delOp)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// ---
        /// Container utility operations.
        /// ---

        /// <summary>
        /// Check if the default container exists.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CheckContainer()
        {
            try
            {
                return await _blobContainer.ExistsAsync();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Create the default container.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CreateContainer()
        {
            try
            {
                return await _blobContainer.CreateIfNotExistsAsync();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete the default container.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> DeleteContainer()
        {
            if (await _blobContainer.DeleteIfExistsAsync())
            {
                if (!await _blobContainer.ExistsAsync())
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        /// ---
        /// List container document operations.
        /// ---

        #region List container documents

        // list all documents in container
        //public static List<string> GetDocumentList(string blobPrefix = null)
        //{
        //    IEnumerable<IListBlobItem> blobList = null;
        //    List<string> docList = null;

        //    try
        //    {
        //        blobList = _blobContainer.ListBlobs(blobPrefix, false, BlobListingDetails.None);

        //        foreach (var doc in blobList)
        //        {
        //            docList.Add(((CloudBlockBlob)doc).Name);
        //        }

        //        return docList;
        //    }
        //    catch
        //    {
        //        return docList;
        //    }
        //}

        #endregion

        /// ---
        /// Conversation utilities.
        /// ---

        /// <summary>
        /// Convert byte array to ASCII string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] StringToByte(string input)
        {
            try
            {
                return Encoding.ASCII.GetBytes(input);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Covert string to UTF8 byte array.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ByteToString(byte[] input)
        {
            try
            {
                return Encoding.UTF8.GetString(input, 0, input.Length);
            }
            catch
            {
                return null;
            }
        }
    }
}