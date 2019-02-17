namespace KLOGCopy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.IO;

    // Kiroku Logging Library
    using Kiroku;

    public static class Processor
    {
        #region Sort (offline)

        /// <summary>
        /// Currently offline -- sort and filter a List of FileModel type into groups to process from (send, retention, ect).
        /// </summary>
        /// <param name="fileCollection"></param>
        [Obsolete]
        public static void Sort(List<FileModel> fileCollection)
        {
            using (KLog logSort = new KLog("ClassProcessor-MethodSort"))
            {
                var sendFiles = fileCollection.Where(x => x.TagCode == 1);
                logSort.Info($"Send File Count: {sendFiles.Count().ToString()}");

                var cleanupFiles = fileCollection.Where(x => x.TagCode == 2);
                logSort.Info($"Cleanse File Count: {cleanupFiles.Count().ToString()}");

                var retentionFiles = fileCollection.Where(x => x.TagCode == 3 || (x.TagCode == 4));
                logSort.Info($"Retention File Count: {retentionFiles.Count().ToString()}");
            }
        }

        #endregion

        #region Send

        /// <summary>
        /// Send "KLOG_S_$(guid).txt" files to Azure Storage Blob. On success, local file is renamed to "KLOG_A_$(guid).txt" to set as archive.
        /// </summary>
        /// <param name="sendFiles"></param>
        /// <param name="containerName"></param>
        public static void Send(IEnumerable<FileModel> sendFiles, string containerName)
        {
            using (KLog logSend = new KLog("ClassProcessor-MethodSend"))
            {
                try
                {
                    logSend.Info($" File Count => {sendFiles.Count().ToString()}");

                    foreach (var sendFile in sendFiles)
                    {
                        // Parse connection string
                        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Global.AzureStorage);

                        // Create the blob client
                        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                        // Retrieve container reference -- create if it's not availble
                        CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                        container.CreateIfNotExistsAsync().Wait();

                        // Build KLOG _R_ead file name to use on the blob write
                        var blobfileName = @"\KLOG_R_" + sendFile.FileGuid.ToString() + ".txt";

                        // Retrieve blob reference -- example: containerName\$(localDir)\KLOG_R_$(guid).txt
                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(sendFile.DirName + blobfileName);

                        // Check if the file already exist before trying to send
                        if (!blockBlob.Exists())
                        {
                            // Create or overwrite the "myblob" blob with contents from a local file
                            using (var fileStream = System.IO.File.OpenRead(sendFile.FullPath))
                            {
                                blockBlob.UploadFromStream(fileStream);
                                logSend.Info($"Send Status => Sending: {blockBlob.Name}");
                            }

                            // Check and then Rename local file after file has been sent to Azure Storage Blob
                            if ((container.GetBlockBlobReference(sendFile.DirName + blobfileName)).Exists())
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
                catch (Exception ex)
                {
                    logSend.Error($"{ex.ToString()}");
                }
            }
        }

        #endregion

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
                        var check = ((DateTime.UtcNow.AddDays(Global.RetentionDays)) < retentionFile.FileDate) ? "Hold" : "Delete";

                        var checkBool = ((DateTime.UtcNow.AddDays(Global.RetentionDays)) < retentionFile.FileDate);

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

        #region Cleanse

        /// <summary>
        /// Identify local "KLOG_W_$(guid).txt" files (orpahned) that exceed the provided retention period (in hours). The files are renamed (re-queued) to "KLOG_S_$(guid).txt" for sending.
        /// </summary>
        /// <param name="cleanupFiles"></param>
        public static void Cleanse(IEnumerable<FileModel> cleanupFiles)
        {
            using (KLog logCleanse = new KLog("ClassProcessor-MethodCleanse"))
            {
                try
                {
                    foreach (var cleanseFile in cleanupFiles)
                    {
                        // TODO: clean-up check + checkBool
                        var check = ((DateTime.UtcNow.AddHours(Global.CleanseHours)) < cleanseFile.FileDate) ? "Hold" : "Rename";

                        var checkBool = ((DateTime.UtcNow.AddHours(Global.CleanseHours)) < cleanseFile.FileDate);

                        logCleanse.Info($"Cleanse File Operation => Time: {cleanseFile.FileDate.ToString()}, Result: {check.ToString()}, File: {cleanseFile.FileName}");

                        if (!checkBool)
                        {
                            var renamefileName = cleanseFile.Path + @"\KLOG_S_" + cleanseFile.FileGuid.ToString() + ".txt";

                            File.Move(cleanseFile.FullPath, renamefileName);

                            logCleanse.Info($"Cleanse File Operation => |- Rename File: {renamefileName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logCleanse.Error($"{ex.ToString()}");
                }
            }
        }

        #endregion
    }
}
