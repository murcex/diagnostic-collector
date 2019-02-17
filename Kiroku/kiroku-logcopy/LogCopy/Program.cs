namespace KLOGCopy
{
    using System.Collections.Generic;
    using System.Linq;

    // Kiroku Logging Library
    using Kiroku;

    class Program
    {
        static void Main(string[] args)
        {
            // Start instance level logging
            Global.StartLogging();

            // Log global properties
            using (KLog logConfig = new KLog("ClassProgram-LogicConfig"))
            {
                logConfig.Info($"Config Local Directory: {Global.LocalDirectory}");
                logConfig.Info($"Config Azure Container: {Global.AzureContainer}");
                logConfig.Info($"Config Retention: {Global.RetentionDays}");
                logConfig.Info($"Config Cleanse: {Global.CleanseHours}");
            }

            // Initialize filter groups
            IEnumerable<FileModel> 
                sendFiles = null, 
                cleanupFiles = null, 
                retentionFiles = null;

            // Extract all files names
            var fileMetaData = FileCollector.Execute(Global.LocalDirectory);

            // Filter and count tags
            using (KLog logSort = new KLog("ClassProgram-LogicSort"))
            {
                if (fileMetaData != null)
                {
                    sendFiles = fileMetaData.Where(x => x.TagCode == 1);
                    logSort.Info($"Send File Count: {sendFiles.Count().ToString()}");

                    cleanupFiles = fileMetaData.Where(x => x.TagCode == 2);
                    logSort.Info($"Cleanse File Count: {cleanupFiles.Count().ToString()}");

                    retentionFiles = fileMetaData.Where(x => x.TagCode == 3 || (x.TagCode == 4));
                    logSort.Info($"Retention File Count: {retentionFiles.Count().ToString()}");
                }
                else
                {
                    logSort.Info("No files where found during collection to sort.");
                }
            }

            // Process each IEnum filter group heir appropriate action method
            using (KLog logProcess = new KLog("ClassProgram-LogicProcess"))
            {
                if (retentionFiles != null)
                {
                    Processor.Retention(retentionFiles);
                }
                else
                {
                    logProcess.Info("No files were marked for retention.");
                }

                if (sendFiles != null)
                {
                    Processor.Send(sendFiles, Global.AzureContainer);
                }
                else
                {
                    logProcess.Info("No files were sent.");
                }

                if (cleanupFiles != null)
                {
                    Processor.Cleanse(cleanupFiles);
                }
                else
                {
                    logProcess.Info("No files required clean-up.");
                }
            }

            // End instance level logging
            Global.StopLogging();

            Global.CheckDebug();
        }
    }
}
