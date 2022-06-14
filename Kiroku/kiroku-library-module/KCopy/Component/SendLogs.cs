namespace KCopy.Component
{
    using System;
    using Kiroku;
    using KCopy.Storage;
    using KCopy.Model;

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
                    var fileCount = Capsule.SendFileCount();

                    logSend.Metric("SendFileCount", fileCount);

                    if (fileCount > 0)
                    {
                        foreach (var sendFile in Capsule.SendFiles)
                        {
                            if (!RemoteStorage.LogExist(sendFile))
                            {
                                var document = LocalStorage.ReadLog(sendFile);

                                if (RemoteStorage.SendLog(sendFile, document))
                                {
                                    if (RemoteStorage.LogExist(sendFile))
                                    {
                                        LocalStorage.MarkToArchiveLog(sendFile);
                                    }
                                    else
                                    {
                                        logSend.Error($"Send Status => SendLog-LogExist: Log doesn't exist. {sendFile.FileName}");
                                    }
                                }
                                else
                                {
                                    logSend.Error($"Send Status => SendLog: Writing log to remote storage failed: {sendFile.FileName}");
                                }
                            }

                            // if the file already exist in remote storage, then renamed locally for archiving
                            else
                            {
                                logSend.Error($"Send Status => Exist. Skip Sending File: {sendFile.FileName}");

                                LocalStorage.MarkToArchiveLog(sendFile);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logSend.Error($"{ex}");
                }
            }
        }
    }
}
