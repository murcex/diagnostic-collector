namespace KCopy.Component
{
    using System;
    using Kiroku;
    using KCopy.Core;
    using KCopy.Storage;
    using KCopy.Model;

    class DeleteLogs
    {
        /// <summary>
        /// Delete local "KLOG_A_$(guid).txt" files that exceed the configured retention period (in days).
        /// </summary>
        /// <param name="retentionFiles"></param>
        public static void Execute()
        {
            using (KLog logRetention = new KLog("ClassDeleteLogs-MethodExecute"))
            {
                try
                {
                    if (Capsule.DeleteFileCount() > 0)
                    {
                        logRetention.Trace($"Retention Threshold: {Configuration.RetentionThreshold}");

                        foreach (var retentionFile in Capsule.DeleteFiles)
                        {
                            if (Configuration.RetentionThreshold > retentionFile.FileDate)
                            {
                                LocalStorage.DeleteLog(retentionFile.FullPath);

                                logRetention.Trace($"Deleting: {retentionFile.FileName}");
                            }
                            else
                            {
                                logRetention.Trace($"Holding: {retentionFile.FileName}");
                            }
                        }
                    }
                    else
                    {
                        logRetention.Info("No files were marked for retention.");
                    }
                }
                catch (Exception ex)
                {
                    logRetention.Error($"{ex}");
                }
            }
        }
    }
}
