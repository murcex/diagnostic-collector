namespace KLoad
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Upload first line (Instance) to database.
    /// </summary>
    public static class UploadFirstLine
    {
        public static bool Execute(string line, KLog uploaderLog)
        {
            var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

            cleanLine = cleanLine.Replace("KLOG_VERSION_", "");

            InstanceModel instanceHeader;

            try
            {
                instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);

                var checkAddInstanceStart = DataAccessor.AddInstanceStart(instanceHeader);

                if (!checkAddInstanceStart.Success)
                {
                    uploaderLog.Error($"SQL Expection on [UploadFirstLine].[AddInstanceStart] - Message: {checkAddInstanceStart.Message}");
                    return false;
                }

            }
            catch (Exception ex)
            {
                uploaderLog.Error($"Expection on [UploadFirstLine].[DeserializeInstanceHeader] - Message: {ex.ToString()}");
                return false;
            }

            return true;
        }

        public static void Execute(Guid fileGuid)
        {
            BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).HeaderStatus = false;
            BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).LogStatus = false;
            BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).FooterStatus = false;
        }
    }
}
