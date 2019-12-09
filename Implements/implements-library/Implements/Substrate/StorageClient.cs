namespace Implements.Substrate
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Text;

    public class StorageClient
    {
        private static CloudBlobContainer _blobContainer;

        private static string _account;

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

        /// Single Document CRUD operations

        // insert
        public static async Task<bool> AddDocument(string file, byte[] document)
        {
            try
            {
                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);

                if (!await blockBlob.ExistsAsync())
                {
                    blockBlob.UploadFromByteArray(document, 0, document.Length);

                    if (await blockBlob.ExistsAsync())
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    //document = Encoding.ASCII.GetBytes("Blob Doesn't exist.");

                    return false;
                }
            }
            catch
            {
                //document = Encoding.ASCII.GetBytes("Get Document Exception.");

                return false;
            }
        }

        // select
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

        // update
        public static async Task<bool> UpdateDocument(string file, byte[] document)
        {
            CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);

            try
            {
                if (!await blockBlob.ExistsAsync())
                {
                    blockBlob.UploadFromByteArray(document, 0, document.Length);

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
                        blockBlob.UploadFromByteArray(document, 0, document.Length);

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

        // delete
        public static async Task<bool> DeleteDocument(string file)
        {
            try
            {
                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(file);

                if (await blockBlob.ExistsAsync())
                {
                    blockBlob.Delete();

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

        /// Single Document Moves

        // move in container
        public static async Task<bool> MoveDocumentFromContainer(string container, string file)
        {
            try
            {
                var sourceBlobContainer = _blobContainer;
                var targetBlobContainer = CloudStorageAccount.Parse(_account)
                    .CreateCloudBlobClient()
                    .GetContainerReference(container.ToLower());

                // get doc from source
                var sourceDocument = await GetDocument(file);

                // insert doc to new container
                var uploadStatus = false;

                try
                {
                    CloudBlockBlob targetBlockBlob = targetBlobContainer.GetBlockBlobReference(file);

                    if (!await targetBlockBlob.ExistsAsync())
                    {
                        targetBlockBlob.UploadFromByteArray(sourceDocument, 0, sourceDocument.Length);

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

                // delete from source once insert is finished
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

        // move from container
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

        /// Container Operations

        // check if container exist
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

        // create container
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

        // delete container
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

        /// List

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

        // utl
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
