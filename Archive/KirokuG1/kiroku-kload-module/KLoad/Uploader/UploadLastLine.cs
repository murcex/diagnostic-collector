namespace KLoad
{
    using System;
    using Newtonsoft.Json;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Upload last line (Instance) to database.
    /// </summary>
    public static class UploadLastLine
    {
        public static bool Execute(string line, KLog uploaderLog)
        {
            var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

            InstanceModel instanceHeader = null;
            try
            {
                instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);
            }
            catch (Exception ex)
            {
                uploaderLog.Error($"Expection on [UploadLastLine].[DeserializeInstanceFooter] - Message: {ex.ToString()}");
                return false;
            }

            var checkUpdateInstanceStop = DataAccessor.UpdateInstanceStop(instanceHeader);

            if (!checkUpdateInstanceStop.Success)
            {
                uploaderLog.Error($"SQL Expection on [UploadLastLine].[UpdateInstanceStop] - Message: {checkUpdateInstanceStop.Message}");
                return false;
            }

            return true;
        }
    }
}
