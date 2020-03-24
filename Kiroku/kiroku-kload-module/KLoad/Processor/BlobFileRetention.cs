namespace KLoad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Metadata;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Delete Blob File from Azure Storage Blob.
    /// </summary>
    class BlobFileRetention
    {
        public static void Execute()
        {
            // Blob retention
            using (KLog blobRetention = new KLog("BlobFileRetention-BlobRetention"))
            {
                try
                {
                    IEnumerable<BlobFileModel> retentionFileCollection = BlobFileCollection.CurrentRetentionCount();

                    blobRetention.Info($"Retention Count: {retentionFileCollection.Count()}");

                    foreach (BlobFileModel blobFile in retentionFileCollection)
                    {
                        // for each file, confim check one more
                        blobRetention.Info($"Retention => File Name: {blobFile.CloudFile}");

                        string cloudFile = blobFile.CloudFile;

                        try
                        {
                            BlobClient.DeleteBlobFile(cloudFile);

                            blobRetention.Info($"File Deleted => File Name: {blobFile.CloudFile}");
                        }
                        catch (Exception ex)
                        {
                            blobRetention.Error($"Delete Failure: {blobFile.CloudFile} Exception: {ex}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    blobRetention.Error($"BlobFileRetention Exception: {ex.ToString()}");
                }
            }

            // SQL Retention
            using (KLog sqlRetention = new KLog("BlobFileRetention-SQLRetention"))
            {
                try
                {
                    SQLResponseModel checkRetention = DataAccessor.Retention(Configuration.RetentionDays);

                    if (!checkRetention.Success)
                    {
                        sqlRetention.Error($"SQL Expection on [BlobFileRetention].[Retention] - Message: {checkRetention.Message}");
                    }
                }
                catch (Exception ex)
                {
                    sqlRetention.Error($"BlobFileRetention Exception: {ex.ToString()}");
                }
            }
        }
    }
}
