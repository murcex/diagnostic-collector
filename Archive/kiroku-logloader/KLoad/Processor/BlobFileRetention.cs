namespace KLoad
{
    using System;
    using System.Collections.Generic;

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

                    foreach (BlobFileModel blobFile in retentionFileCollection)
                    {
                        // for each file, confim check one more
                        blobRetention.Info($"Retention => File Name: {blobFile.CloudFile}");

                        string cloudFile = blobFile.CloudFile;

                        BlobClient.DeleteBlobFile(cloudFile);

                        blobRetention.Info($"File Deleted => File Name: {blobFile.CloudFile}");
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
                    SQLResponseModel checkRetention = DataAccessor.Retention(Global.RetentionDays);

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
