namespace Implements.Substrate.Blob
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class BlobClient
    {
        private static ConcurrentDictionary<string, CloudBlobContainer> profiles = new ConcurrentDictionary<string, CloudBlobContainer>();

        private string error_1 = "";

        /// <summary>
        /// Initialize the blob storage client with account and container details.
        /// </summary>
        public static async Task<bool> Initialize(
            string profileName, 
            string storageAccount, 
            string storageContainer, 
            bool verifyContainer = false)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                var _account = storageAccount;

                var _blobContainer = CloudStorageAccount.Parse(storageAccount)
                    .CreateCloudBlobClient()
                    .GetContainerReference(storageContainer.ToLower());
                
                if (verifyContainer)
                {
                    if (await CreateContainer(profileName))
                    {
                        if (await VerifyContainer(profileName))
                        {
                            profiles[profileName] = _blobContainer;

                            return profiles.TryGetValue(profileName, out CloudBlobContainer container);
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
                    profiles[profileName] = _blobContainer;

                    return profiles.TryGetValue(profileName, out CloudBlobContainer container);
                }
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
        public static async Task<bool> InsertBlob(
            string profileName, 
            string file, 
            byte[] document)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (string.IsNullOrEmpty(file))
                {
                    return false;
                }

                if (document == null)
                {
                    return false;
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

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
        public static async Task<byte[]> SelectBlob(
            string profileName, 
            string fileName)
        {
            byte[] document = null;

            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return document;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    return document;
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return document;
                }

                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileName);

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
        /// Update an existing document on the default blob storage container.
        /// </summary>
        public static async Task<bool> UpdateBlob(
            string profileName, 
            string fileName, 
            byte[] document)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    return false;
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileName);

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
                    if (await DeleteBlob(profileName, fileName))
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
        public static async Task<bool> DeleteBlob(
            string profileName, 
            string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (string.IsNullOrEmpty(fileName))
                {
                    return false;
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

                CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileName);

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
        /// Single document modifer within the same container.
        /// ---

        /// <summary>
        /// Rename an existing document on the default blob storage container.
        /// </summary>
        public static async Task<bool> RenameBlobInContainer(
            string profileName, 
            string currentFileName, 
            string newFileName)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (string.IsNullOrEmpty(currentFileName) || string.IsNullOrEmpty(newFileName))
                {
                    return false;
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

                var currentDocument = await SelectBlob(profileName, currentFileName);

                if (currentDocument != null)
                {
                    var addWithNewFileName = await InsertBlob(profileName, newFileName, currentDocument);

                    if (addWithNewFileName)
                    {
                        return await DeleteBlob(profileName, currentFileName);
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

        /// <summary>
        /// Copy an existing document to a location in the default blob storage container, with an option to delete the source file after copying.
        /// </summary>
        public static async Task<bool> CopyBlobInContainer(
            string profileName, 
            string sourceFileName, 
            string destinationFileName, 
            bool deleteSource = false)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (string.IsNullOrEmpty(sourceFileName) || string.IsNullOrEmpty(destinationFileName))
                {
                    return false;
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

                var sourceDocument = await SelectBlob(profileName, sourceFileName);

                if (sourceDocument != null)
                {
                    var addWithNewFileName = await InsertBlob(profileName, destinationFileName, sourceDocument);

                    if (addWithNewFileName)
                    {
                        if (deleteSource)
                        {
                            return await DeleteBlob(profileName, sourceFileName);
                        }
                        else
                        {
                            return true;
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
        /// List container operations.
        /// ---

        #region List container documents

        // list all documents in container
        public static List<string> SelectBlobList(
            string profileName, 
            string blobPrefix = null)
        {
            List<string> files = new List<string>();

            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return files;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return files;
                }

                if (!string.IsNullOrEmpty(blobPrefix))
                {
                    if (blobPrefix.Last() != '/')
                    {
                        blobPrefix = blobPrefix + "/";
                    }
                }

                var token = new BlobContinuationToken();

                var segments = _blobContainer.ListBlobsSegmentedAsync(blobPrefix, token).GetAwaiter().GetResult();

                files = segments.Results.OfType<CloudBlockBlob>().Select(file => file.Name).ToList();

                files = files.Select(file => file.Replace('/', '\\')).ToList();

                return files;
            }
            catch
            {
                return files;
            }
        }

        public static List<string> SelectPrefixList(
            string profileName, 
            string blobPrefix = null)
        {
            List<string> prefixs = new List<string>();

            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return prefixs;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return prefixs;
                }

                if (!string.IsNullOrEmpty(blobPrefix))
                {
                    if (blobPrefix.Last() != '/')
                    {
                        blobPrefix = blobPrefix + "/";
                    }
                }

                var token = new BlobContinuationToken();

                var segments = _blobContainer.ListBlobsSegmentedAsync(blobPrefix, token).GetAwaiter().GetResult();

                prefixs = segments.Results.OfType<CloudBlobDirectory>().Select(file => file.Prefix).ToList();

                prefixs = prefixs.Select(file => file.Replace('/', '\\')).ToList();

                return prefixs;
            }
            catch
            {
                return prefixs;
            }
        }

        #endregion

        /// ---
        /// Container utility operations.
        /// ---

        /// <summary>
        /// Check if the default container exists.
        /// </summary>
        public static async Task<bool> VerifyContainer(string profileName)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

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
        public static async Task<bool> CreateContainer(string profileName)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

                return await _blobContainer.CreateIfNotExistsAsync().ConfigureAwait(false);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete the default container.
        /// </summary>
        public static async Task<bool> DeleteContainer(string profileName)
        {
            try
            {
                if (string.IsNullOrEmpty(profileName))
                {
                    return false;
                }
                else
                {
                    profileName = profileName.ToUpper();
                }

                if (!profiles.TryGetValue(profileName, out CloudBlobContainer _blobContainer))
                {
                    return false;
                }

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
            catch
            {
                return false;
            }
        }

        /// ---
        /// Conversation utilities.
        /// ---

        /// <summary>
        /// Convert byte array to ASCII string.
        /// </summary>
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
        public static string ByteToString(byte[] input)
        {
            try
            {
                return Encoding.UTF8.GetString(input, 0, input.Length);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}