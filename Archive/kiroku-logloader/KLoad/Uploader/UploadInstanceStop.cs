namespace KLoad
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Upload last line, try log type -- not Instance -- then back fill current Instance with a stop time.
    /// </summary>
    public static class UploadInstanceStop
    {
        public static bool Execute(string line, Guid fileGuid, KLog uploaderLog)
        {
            uploaderLog.Warning($"Uploader => Footer Check Failed - Guid: {fileGuid.ToString()}");

            BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).FooterStatus = false;

            LogRecordModel record = null;

            try
            {
                record = JsonConvert.DeserializeObject<LogRecordModel>(line);
            }
            catch (Exception ex)
            {
                uploaderLog.Error($" [UploadInstanceStop] Log Record Deserialize Exception: {ex.ToString()}");
                return false;
            }

            var newEndTime = record.EventTime;

            InstanceModel newInstanceCloser = new InstanceModel();
            newInstanceCloser.InstanceID = fileGuid;
            newInstanceCloser.EventTime = newEndTime;

            var checkUpdateInstaceClose = DataAccessor.UpdateInstanceStop(newInstanceCloser);

            if (!checkUpdateInstaceClose.Success)
            {
                uploaderLog.Error($"SQL Expection on [UploadInstanceStop].[UpdateInstaceClose] - Message: {checkUpdateInstaceClose.Message}");
                return false;
            }

            var checkUpdateBlockStop = DataAccessor.UpdateBlockEmptyStop(record, fileGuid);

            if (!checkUpdateBlockStop.Success)
            {
                uploaderLog.Error($"SQL Expection on [UploadInstanceStop].[UpdateBlockStop] - Message: {checkUpdateBlockStop.Message}");
                return false;
            }

            return true;
        }
    }
}
