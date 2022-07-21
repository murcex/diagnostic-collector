namespace KCopy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Kiroku;

    static class SendLogs
    {
        /// <summary>
        /// Send "KLOG_S_$(guid).txt" files to Azure Storage Blob. On success, local file is renamed to "KLOG_A_$(guid).txt" to set as archive.
        /// </summary>
        /// <param name="sendFiles"></param>
        /// <param name="containerName"></param>
        public static void Execute()
        {
            using (KLog logSend = new KLog("ClassSendLogs-MethodExecute"))
            {
                try
                {
                    if (Capsule.SendFileCount() > 0)
                    {
                        logSend.Info($" File Count => {Capsule.SendFileCount().ToString()}");

                        foreach (var sendFile in Capsule.SendFiles)
                        {
                            // Parse connection string
                            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureStorage);

                            // Create the blob client
                            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                            // Retrieve container reference -- create if it's not availble
                            CloudBlobContainer container = blobClient.GetContainerReference(Configuration.AzureContainer);
                            container.CreateIfNotExistsAsync().Wait();

                            // Build KLOG _R_ead file name to use on the blob write
                            var blobfileName = @"\KLOG_R_" + sendFile.FileGuid.ToString() + ".txt";

                            // Retrieve blob reference -- example: containerName\$(localDir)\KLOG_R_$(guid).txt
                            CloudBlockBlob blockBlob = container.GetBlockBlobReference(sendFile.DirName + blobfileName);

                            // Check if the file already exist before trying to send
                            if (!blockBlob.ExistsAsync().GetAwaiter().GetResult())
                            {
                                // Create or overwrite the "myblob" blob with contents from a local file
                                using (var fileStream = System.IO.File.OpenRead(sendFile.FullPath))
                                {
                                    blockBlob.UploadFromStreamAsync(fileStream).GetAwaiter().GetResult();
                                    logSend.Info($"Send Status => Sending: {blockBlob.Name}");
                                }

                                // Check and then Rename local file after file has been sent to Azure Storage Blob
                                if ((container.GetBlockBlobReference(sendFile.DirName + blobfileName)).ExistsAsync().GetAwaiter().GetResult())
                                {
                                    logSend.Info("Send Status => |- Transmission Confirmed.");

                                    var renamefileName = sendFile.Path + @"\KLOG_A_" + sendFile.FileGuid.ToString() + ".txt";

                                    logSend.Info($"Send Status => |- Renaming: {renamefileName}");

                                    File.Move(sendFile.FullPath, renamefileName);
                                }
                            }

                            // Files already exist in Azure Storage Blob -- rename local file for archiving
                            else
                            {
                                logSend.Info($"Send Status => Exist. Skip Sending File: {sendFile.FileName}");

                                var renamefileName = sendFile.Path + @"\KLOG_A_" + sendFile.FileGuid.ToString() + ".txt";

                                logSend.Info($"Send Status => |- Renaming: {renamefileName}");

                                File.Move(sendFile.FullPath, renamefileName);
                            }
                        }
                    }
                    else
                    {
                        logSend.Info("No files were sent.");
                    }
                }
                catch (Exception ex)
                {
                    logSend.Error($"{ex.ToString()}");
                }
            }
        }

        #region Retention

        /// <summary>
        /// Delete local "KLOG_A_$(guid).txt" files that exceed the provided retention period (in days).
        /// </summary>
        /// <param name="retentionFiles"></param>
        public static void Retention(IEnumerable<FileModel> retentionFiles)
        {
            using (KLog logRetention = new KLog("ClassProcessor-MethodRetention"))
            {
                try
                {
                    foreach (var retentionFile in retentionFiles)
                    {
                        // TODO: clean-up check + checkBool
                        var check = ((DateTime.UtcNow.AddDays(Configuration.RetentionDays)) < retentionFile.FileDate) ? "Hold" : "Delete";

                        var checkBool = ((DateTime.UtcNow.AddDays(Configuration.RetentionDays)) < retentionFile.FileDate);

                        logRetention.Info($"Retention File Operation => Time: {retentionFile.FileDate.ToString()}, Result: {check.ToString()}, File: {retentionFile.FileName}");

                        if (!checkBool)
                        {
                            File.Delete(retentionFile.FullPath);

                            logRetention.Info($"Retention File Operation => |- Delete File: {retentionFile.FileName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logRetention.Error($"{ex.ToString()}");
                }
            }
        }

        #endregion
    }
}
