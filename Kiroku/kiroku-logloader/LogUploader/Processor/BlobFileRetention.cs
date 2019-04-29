namespace KLOGLoader
{
    using System;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Delete Blob File from Azure Storage Blob.
    /// </summary>
    class BlobFileRetention
    {
        public static void Execute()
        {
            using (KLog retentionLog = new KLog("BlobFileRetention-MethodExecute"))
            {
                try
                {
                    var retentionFileCollction = BlobFileCollection.CurrentRetentionCount();

                    foreach (var file in retentionFileCollction)
                    {
                        // for each file, confim check one more
                        retentionLog.Info($"Retention => File Name: {file.CloudFile}");

                        var cloudFile = file.CloudFile;

                        BlobClient.DeleteBlobFile(cloudFile);

                        retentionLog.Info($"File Deleted => File Name: {file.CloudFile}");
                    }

                    var checkRetention = DataAccessor.Retention(Global.RetentionDays);

                    if (!checkRetention.Success)
                    {
                        retentionLog.Error($"SQL Expection on [BlobFileRetention].[Retention] - Message: {checkRetention.Message}");
                    }
                }
                catch (Exception ex)
                {
                    retentionLog.Error($"BlobFileRetention Exception: {ex.ToString()}");
                }
            }
        }
    }
}
