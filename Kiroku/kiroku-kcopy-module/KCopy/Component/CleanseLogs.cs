namespace KCopy.Component
{
    using System;
    using Kiroku;
    using KCopy.Core;
    using KCopy.Storage;
    using KCopy.Model;

    class CleanseLogs
    {
        /// <summary>
        /// Identify local "KLOG_W_$(guid).txt" files (orpahned) that exceed the configured retention period (in hours). 
        /// The files are renamed (re-queued) to "KLOG_S_$(guid).txt" for sending.
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
                            if (Configuration.CleanseThreshold < cleanseFile.FileDate)
                            {
                                LocalStorage.MarkToSendLog(cleanseFile);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logCleanse.Error($"{ex}");
                }
            }
        }
    }
}
