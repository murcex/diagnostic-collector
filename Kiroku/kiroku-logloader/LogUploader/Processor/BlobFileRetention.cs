

namespace KLOGLoader
{
    using System;

    // Kiroku
    using Kiroku;

    class BlobFileRetention
    {
        public static void Execute()
        {
            using (KLog retentionLog = new KLog("ClassBlobFileRetention-MethodExecute"))
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
                }
                catch (Exception ex)
                {
                    retentionLog.Error($"BlobFileRetention Exception: {ex.ToString()}");
                    // TODO: Tracker
                }
            }
        }
    }
}
