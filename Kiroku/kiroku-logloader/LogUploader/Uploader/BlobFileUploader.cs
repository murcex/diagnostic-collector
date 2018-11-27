namespace KLOGLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Kiroku
    using Kiroku;

    class BlobFileUploader
    {
        public static void Execute()
        {
            using (KLog uploaderLog = new KLog("ClassBlobFileUploader-MethodExecute"))
            {
                // Load files not currently uploaded
                var existFileCollction = BlobFileCollection.CurrentExistFalseCount();

                try
                {
                    foreach (var blobFile in existFileCollction)
                    {
                        // Set variables
                        var cloudFile = blobFile.CloudFile;
                        var fileGuid = blobFile.FileGuid;
                        var lineCounter = 1;
                        
                        List<LogRecordModel> recordModelList = new List<LogRecordModel>();
                        List<string> lines = new List<string>();

                        var payload = BlobClient.GetPayload(cloudFile);

                        lines = ReadFile.Execute(payload);

                        #region StreamReader
                        //using (StreamReader reader = new StreamReader(payload.OpenRead()))
                        //{
                        //    while (!reader.EndOfStream)
                        //    {
                        //        lines.Add(reader.ReadLine());
                        //    }
                        //}

                        #endregion

                        var lineCountTotal = lines.Count();

                        uploaderLog.Info($"Uploader => Starting Upload - Guid: {fileGuid.ToString()} Line Count: {lineCountTotal} ");

                        // Read each line in the log file. Check first and last line for KLog instance data. Break loop on any unexcpted data or failure. 
                        foreach (var line in lines)
                        {
                            // Check first line -- expecting the KLog instane "header"
                            if (lineCounter == 1)
                            {
                                if (line.Contains("#KLOG_INSTANCE_STATUS#"))
                                {
                                    var checkUploadFirstLine = UploadFirstLine.Execute(line, uploaderLog);

                                    if (!checkUploadFirstLine)
                                    {
                                        uploaderLog.Error($"[BlobFileUploader].[checkUploadFirstLine] - GUID: {fileGuid}");
                                        break;
                                    }

                                    #region UploadFirstLine

                                    //var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

                                    //InstanceModel instanceHeader;

                                    //try
                                    //{
                                    //    instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);

                                    //    var checkAddInstanceStart = DataAccessor.AddInstanceStart(instanceHeader);

                                    //    if (!checkAddInstanceStart.Success)
                                    //    {
                                    //        uploaderLog.Error($"SQL Expection on [AddInstanceStart] - Message: {checkAddInstanceStart.Message}");

                                    //        break;
                                    //    }

                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    uploaderLog.Error($"Expection on [DeserializeInstanceHeader] - Message: {ex.ToString()}");

                                    //    break;
                                    //}

                                    #endregion
                                }
                                else
                                {
                                    UploadFirstLine.Execute(fileGuid);

                                    uploaderLog.Error($"Uploader => Header Check Failed - Guid: {fileGuid.ToString()} Line: {line}");

                                    #region BlobFileCollection

                                    //BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).HeaderStatus = false;
                                    //BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).LogStatus = false;
                                    //BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).FooterStatus = false;

                                    #endregion

                                    break;
                                }

                                lineCounter++;
                            }

                            // Check last line -- expecting the KLog instance "footer" and add all logs.
                            else if (lineCounter == lineCountTotal)
                            {
                                // Post all logs
                                var checkUploadAllLogs = UploadAllLogs.Execute(recordModelList, fileGuid, uploaderLog);

                                if (!checkUploadAllLogs)
                                {
                                    uploaderLog.Error($"[BlobFileUploader].[UploadAllLogs] - GUID: {fileGuid}");
                                    break;
                                }

                                #region UploadAllLogs

                                //// write all logs no matter if footer exists
                                //var checkAddInstanceStop = DataAccessor.AddLogs(recordModelList, fileGuid);

                                //if (!checkAddInstanceStop.Success)
                                //{
                                //    uploaderLog.Error($"SQL Expection on [AddInstanceStop] - Message: {checkAddInstanceStop.Message}");

                                //    break;
                                //}

                                #endregion

                                // Check for a proper KLog instance "footer"
                                if (line.Contains("#KLOG_INSTANCE_STATUS#"))
                                {
                                    var checkUploadLastLine = UploadLastLine.Execute(line, uploaderLog);

                                    if (!checkUploadLastLine)
                                    {
                                        uploaderLog.Error($"[BlobFileUploader].[UploadLastLine] - GUID: {fileGuid}");
                                        //break;
                                    }

                                    #region UploadLastLine

                                    //var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

                                    //InstanceModel instanceHeader = null;
                                    //try
                                    //{
                                    //    instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    uploaderLog.Error($"Expection on [DeserializeInstanceFooter] - Message: {ex.ToString()}");
                                    //}

                                    //var checkUpdateInstanceStop = DataAccessor.UpdateInstanceStop(instanceHeader);

                                    //if (!checkUpdateInstanceStop.Success)
                                    //{
                                    //    uploaderLog.Error($"SQL Expection on [UpdateInstanceStop] - Message: {checkUpdateInstanceStop.Message}");

                                    //    break;
                                    //}

                                    #endregion
                                }
                                else
                                {
                                    var checkUploadInstanceStop = UploadInstanceStop.Execute(line, fileGuid, uploaderLog);

                                    if (!checkUploadInstanceStop)
                                    {
                                        uploaderLog.Error($"[BlobFileUploader].[UploadInstanceStop] - GUID: {fileGuid}");
                                        //break;
                                    }

                                    #region UploadInstanceStop

                                    //uploaderLog.Warning($"Uploader => Footer Check Failed - Guid: {fileGuid.ToString()}");

                                    //BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).FooterStatus = false;

                                    //LogRecordModel record = null;

                                    //try
                                    //{
                                    //    record = JsonConvert.DeserializeObject<LogRecordModel>(line);
                                    //}
                                    //catch (Exception ex)
                                    //{
                                    //    uploaderLog.Error($"Log Record Deserialize Exception: {ex.ToString()}");
                                    //}

                                    //var newEndTime = record.EventTime;

                                    //// TODO: "package" after creatation
                                    //InstanceModel newInstanceCloser = new InstanceModel();

                                    //newInstanceCloser.InstanceID = fileGuid;
                                    //newInstanceCloser.EventTime = newEndTime;

                                    //var checkUpdateInstaceClose = DataAccessor.UpdateInstanceStop(newInstanceCloser);

                                    //if (!checkUpdateInstaceClose.Success)
                                    //{
                                    //    uploaderLog.Error($"SQL Expection on [UpdateInstaceClose] - Message: {checkUpdateInstaceClose.Message}");

                                    //    break;
                                    //}

                                    //var checkUpdateBlockStop = DataAccessor.UpdateBlockEmptyStop(record, fileGuid);

                                    //if (!checkUpdateBlockStop.Success)
                                    //{
                                    //    uploaderLog.Error($"SQL Expection on [UpdateBlockStop] - Message: {checkUpdateBlockStop.Message}");

                                    //    break;
                                    //}

                                    #endregion
                                }

                                uploaderLog.Info($"Log Loaded - Guid: {fileGuid.ToString()}");
                            }

                            // Check line for "normal" log record, add to collection to bulk add later.
                            else
                            {
                                var checkAddLogToCollection = AddLogToCollection.Execute(line, recordModelList, uploaderLog);

                                if (!checkAddLogToCollection)
                                {
                                    uploaderLog.Error($"[BlobFileUploader].[AddLogToCollection] - GUID: {fileGuid}");
                                    break;
                                }

                                #region AddLogToCollection

                                //try
                                //{
                                //    var record = JsonConvert.DeserializeObject<LogRecordModel>(line);

                                //    if (record.LogData.Length > Global.MessageLength)
                                //    {
                                //        var messageCap = Global.MessageLength - 20;
                                //        var cleanLogData = "[ERROR-MAX-" + Global.MessageLength + "]";
                                //        cleanLogData += record.LogData.Substring(1, messageCap);
                                //        record.LogData = cleanLogData;
                                //        record.LogType = "Error";
                                //    }

                                //    if (CheckWriteByType(record.LogType))
                                //    {
                                //        recordModelList.Add(record);
                                //    }
                                //}
                                //catch
                                //{
                                //    uploaderLog.Error($"Uploader => Line Exception: {line}");

                                //    break;
                                //}

                                #endregion

                                lineCounter++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    uploaderLog.Error($"BlobFileUploader Exception: {ex.ToString()}");
                }
            }
        }

        #region CheckWriteByType

        //private static bool CheckWriteByType(string type)
        //{
        //    switch (type.ToLower())
        //    {
        //        case "trace":
        //            return Global.Trace ? true : false;

        //        case "info":
        //            return Global.Info ? true : false;

        //        case "warning":
        //            return Global.Warning ? true : false;

        //        case "error":
        //            return Global.Error ? true : false;

        //        default:
        //            {
        //                return true;
        //            }
        //    }
        //}

        #endregion
    }
}
