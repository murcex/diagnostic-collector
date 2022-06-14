namespace KCopy.Component
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using Kiroku;
    using KCopy.Storage;
    using KCopy.Model;

    static class CollectLogs
    {
        /// <summary>
        /// Collect and tag all local "KLOG_$(tag)_$(guid).txt" files from provided filepath.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>List of FileModel type</returns>
        public static List<FileModel> Execute(string filePath)
        {
            List<FileModel> logCollection = new List<FileModel>();

            using (KLog log = new KLog("ClassCollectLogs-MethodExecute"))
            {
                try
                {
                    // Search for all folders in the provide root filepath
                    // If you're using KLOG for more than one service locally, divide them by folders at this root folder
                    var directories = LocalStorage.GetDirectories(filePath);

                    foreach (var directory in directories)
                    {
                        DirectoryInfo directoryInfo = LocalStorage.GetDirectoryInfo(directory);
                        log.Trace($"DirectoryInfo.FullDirectory: {directory}");
                        log.Trace($"DirectoryInfo.DirectoryName: {directoryInfo.Name}");

                        // Scan each folder for KLOG's (KLOG_*.txt)
                        foreach (var file in directoryInfo.GetFiles("*.txt"))
                        {
                            if (file.Name.Count() == 47 && file.Name.Contains("KLOG_"))
                            {
                                FileModel logModel = new FileModel(directoryInfo, file);

                                log.Trace(logModel.TracetoString());

                                logCollection.Add(logModel);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"{ex}");
                }
            }

            return logCollection;
        }
    }
}
