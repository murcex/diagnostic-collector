namespace KLOGLoader
{
    using System;
    using System.Collections.Generic;

    // Kiroku
    using Kiroku;
    using Newtonsoft.Json;

    /// <summary>
    /// Sort log type into Metric, Result or Log, for be written to database.
    /// </summary>
    public static class UploadAllLogs
    {
        public static bool Execute(List<LogRecordModel> recordModelList, Guid fileGuid, KLog uploaderLog)
        {
            Dictionary<Guid, LogRecordModel> blockCache = new Dictionary<Guid, LogRecordModel>();

            foreach (var record in recordModelList)
            {
                if (record.LogData.Contains("KLOG_BLOCK"))
                {
                    if (record.LogData.Contains("KLOG_BLOCK_START"))
                    {
                        blockCache.Add(record.BlockID, record);
                    }
                    else if (record.LogData.Contains("KLOG_BLOCK_STOP"))
                    {
                        var startBlock = blockCache[record.BlockID];

                        if (startBlock != null)
                        {
                            var checkAddBlockResponse = DataAccessor.AddBlock(record, startBlock, instanceId: fileGuid);

                            if (!checkAddBlockResponse.Success)
                            {
                                uploaderLog.Error($"SQL Expection on [UploadAllLogs].[AddBlock] - Message: {checkAddBlockResponse.Message}");
                                return false;
                            }
                        }
                        else
                        {
                            uploaderLog.Error($"Starting Block not found, is missing from Block pair.");
                        }

                        blockCache.Remove(record.BlockID);
                    }
                    else
                    {
                        uploaderLog.Error($"Malformed Block");
                    }
                }
                else
                {
                    if (record.LogType == "Metric")
                    {
                        var jsonString = record.LogData.Replace("#", "\"");
                        var metric = JsonConvert.DeserializeObject<MetricRecordModel>(jsonString);

                        var checkMetricResponse = DataAccessor.AddMetric(record, metric, instanceId: fileGuid);

                        if (!checkMetricResponse.Success)
                        {
                            uploaderLog.Error($"SQL Expection on [UploadAllLogs].[AddMetric] - Message: {checkMetricResponse.Message}");
                            return false;
                        }
                    }
                    else if (record.LogType == "Result")
                    {
                        var jsonString = record.LogData.Replace("#", "\"");
                        var result = JsonConvert.DeserializeObject<ResultRecordModel>(jsonString);

                        var checkResultResponse = DataAccessor.AddResult(record, result, instanceId: fileGuid);

                        if (!checkResultResponse.Success)
                        {
                            uploaderLog.Error($"SQL Expection on [UploadAllLogs].[AddResult] - Message: {checkResultResponse.Message}");
                            return false;
                        }
                    }
                    else
                    {
                        if (record.LogData.Length > Global.MessageLength)
                        {
                            var messageCap = Global.MessageLength - 20;
                            var cleanLogData = "[ERROR-MAX-" + Global.MessageLength + "]";
                            cleanLogData += record.LogData.Substring(1, messageCap);
                            record.LogData = cleanLogData;
                        }

                        var checkAddInstanceStop = DataAccessor.AddLog(record, instanceId: fileGuid);

                        if (!checkAddInstanceStop.Success)
                        {
                            uploaderLog.Error($"SQL Expection on [UploadAllLogs].[AddLog] - Message: {checkAddInstanceStop.Message}");
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
