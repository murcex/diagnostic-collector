namespace KLOGLoader
{
    using System;
    using System.Collections.Generic;

    // Kiroku
    using Kiroku;

    public static class UploadAllLogs
    {
        public static bool Execute(List<LogRecordModel> recordModelList, Guid fileGuid, KLog uploaderLog)
        {
            // write all logs no matter if footer exists
            var checkAddInstanceStop = DataAccessor.AddLogs(recordModelList, fileGuid);

            if (!checkAddInstanceStop.Success)
            {
                uploaderLog.Error($"SQL Expection on [UploadAllLogs].[AddInstanceStop] - Message: {checkAddInstanceStop.Message}");
                return false;
            }

            return true;
        }
    }
}
