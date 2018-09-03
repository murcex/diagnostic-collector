using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// Kiroku Logging Library
using Kiroku;


namespace KLOGCopy
{
    public static class FileCollector
    {
        /// <summary>
        /// Collect and tag all local "KLOG_$(tag)_$(guid).txt" files from provided filepath.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>List of FileModel type</returns>
        public static List<FileModel> Execute(string filePath)
        {
            List<FileModel> fileMetadata = new List<FileModel>();

            using (KLog log = new KLog("ClassFileCollector-MethodExecute"))
            {
                try
                {
                    // Search for all folders in the provide root filePath -- if you're using KLOG for more than one service locally, divide them by folders here
                    var directories = Directory.GetDirectories(filePath);

                    foreach (var d in directories)
                    {
                        // Logging
                        DirectoryInfo dInfo = new DirectoryInfo(d);
                        log.Info($"DirectoryInfo => Full Directory: {d}");
                        log.Info($"DirectoryInfo => Directory Name: {dInfo.Name}");

                        // Scan each folder for KLOG's
                        foreach (var file in dInfo.GetFiles("*.txt"))
                        {
                            FileModel fileModel = new FileModel();

                            // if KLOG
                            if (file.Name.Count() == 47 && file.Name.Contains("KLOG_"))
                            {
                                // Load metadata
                                fileModel.FullPath = file.FullName;
                                fileModel.Path = file.DirectoryName;
                                fileModel.FileName = file.Name;
                                fileModel.TagCode = -1;
                                fileModel.FileDate = file.LastWriteTimeUtc;
                                fileModel.DirName = dInfo.Name;

                                // Parse file name down to GUID and Tag
                                fileModel.FileGuid = Guid.Parse(file.Name.Substring(7, 36));

                                fileModel.Tag = file.Name.Substring(1, 6);

                                if (file.Name.Contains("KLOG_S")) { fileModel.TagCode = 1; }

                                if (file.Name.Contains("KLOG_W")) { fileModel.TagCode = 2; }

                                if (file.Name.Contains("KLOG_A")) { fileModel.TagCode = 3; }

                                // Logging
                                log.Info($"GetFileDetails => FullPath: {fileModel.FullPath}");
                                log.Info($"GetFileDetails => |- Tag: {fileModel.Tag}, Tag Code: {fileModel.TagCode}, File Date: {fileModel.FileDate}");

                                fileMetadata.Add(fileModel);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error($"{ex.ToString()}");
                }
            }

            return fileMetadata;
        }
    }
}
