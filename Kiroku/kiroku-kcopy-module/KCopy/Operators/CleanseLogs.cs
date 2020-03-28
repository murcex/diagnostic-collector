namespace KCopy
{
    using System;
    using System.IO;
    using Kiroku;

    class CleanseLogs
    {
        /// <summary>
        /// Identify local "KLOG_W_$(guid).txt" files (orpahned) that exceed the configured retention period (in hours). The files are renamed (re-queued) to "KLOG_S_$(guid).txt" for sending.
        /// </summary>
        /// <param name="cleanupFiles"></param>
        public static void Execute()
        {
            using (KLog logCleanse = new KLog("ClassCleanseLogs-MethodExecute"))
            {
                try
                {
                    if (Capsule.CleanUpFileCount() > 0)
                    {
                        foreach (var cleanseFile in Capsule.CleanUpFiles)
                        {
                            // TODO: clean-up check + checkBool
                            var check = ((DateTime.UtcNow.AddHours(Configuration.CleanseHours)) < cleanseFile.FileDate) ? "Hold" : "Rename";

                            var checkBool = ((DateTime.UtcNow.AddHours(Configuration.CleanseHours)) < cleanseFile.FileDate);

                            logCleanse.Info($"Cleanse File Operation => Time: {cleanseFile.FileDate.ToString()}, Result: {check.ToString()}, File: {cleanseFile.FileName}");

                            if (!checkBool)
                            {
                                var renamefileName = cleanseFile.Path + @"\KLOG_S_" + cleanseFile.FileGuid.ToString() + ".txt";

                                File.Move(cleanseFile.FullPath, renamefileName);

                                logCleanse.Info($"Cleanse File Operation => |- Rename File: {renamefileName}");
                            }
                        }
                    }
                    else
                    {
                        logCleanse.Info("No files required clean-up.");
                    }
                }
                catch (Exception ex)
                {
                    logCleanse.Error($"{ex.ToString()}");
                }
            }
        }
    }
}
