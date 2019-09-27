namespace KLOGCopy
{
    using Kiroku;
    using System;
    using System.IO;

    class DeleteLogs
    {
        /// <summary>
        /// Delete local "KLOG_A_$(guid).txt" files that exceed the provided retention period (in days).
        /// </summary>
        /// <param name="retentionFiles"></param>
        public static void Execute()
        {
            using (KLog logRetention = new KLog("ClassRetention-MethodExecute"))
            {
                try
                {
                    if (Capsule.DeleteFileCount() > 0)
                    {
                        foreach (var retentionFile in Capsule.DeleteFiles)
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
                    else
                    {
                        logRetention.Info("No files were marked for retention.");
                    }
                }
                catch (Exception ex)
                {
                    logRetention.Error($"{ex.ToString()}");
                }
            }
        }
    }
}
