using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Substrate.Blob
{
    class CRUD
    {

        /// <summary>
        /// Add a document to the default blob storage container.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<bool> AddDocument(CloudBlobContainer container, string file, byte[] document)
        {
            try
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file);

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
        public async Task<byte[]> GetDocument(CloudBlobContainer container, string file)
        {
            byte[] document = null;

            try
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file);

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
        public async Task<bool> UpdateDocument(CloudBlobContainer container, string file, byte[] document)
        {
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(file);

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
                    if (await DeleteDocument(container, file))
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
        public async Task<bool> DeleteDocument(CloudBlobContainer container, string file)
        {
            try
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file);

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
    }
}
