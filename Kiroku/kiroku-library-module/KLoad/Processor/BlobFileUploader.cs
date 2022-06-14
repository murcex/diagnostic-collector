namespace KLoad
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Kiroku
    using Kiroku;

    /// <summary>
    /// Update Blob File contents to database.
    /// </summary>
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
                        try
                        {
                            // Set variables
                            var cloudFile = blobFile.CloudFile;
                            var fileGuid = blobFile.FileGuid;
                            var lineCounter = 1;

                            List<LogRecordModel> recordModelList = new List<LogRecordModel>();
                            List<string> lines = new List<string>();

                            var document = BlobClient.GetDocument(cloudFile);

                            lines = ReadFile.Execute(document);

                            var lineCountTotal = lines.Count();

                            uploaderLog.Info($"Uploader => Starting Upload - Guid: {fileGuid.ToString()} Line Count: {lineCountTotal} ");

                            //
                            // Read each line in the log file. Check first and last line for KLog instance data. Break loop on any unexcpted data or failure.
                            //
                            foreach (var line in lines)
                            {
                                //
                                // Check first line -- expecting the KLog instane "header"
                                //
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
                                    }
                                    else
                                    {
                                        UploadFirstLine.MarkFailure(fileGuid);

                                        uploaderLog.Error($"Uploader => Header Check Failed - Guid: {fileGuid.ToString()} Line: {line}");

                                        break;
                                    }

                                    lineCounter++;
                                }

                                //
                                // Check last line -- expecting the KLog instance "footer" and add all logs.
                                //
                                else if (lineCounter == lineCountTotal)
                                {
                                    // Post all logs
                                    var checkUploadAllLogs = UploadAllLogs.Execute(recordModelList, fileGuid, uploaderLog);

                                    if (!checkUploadAllLogs)
                                    {
                                        uploaderLog.Error($"[BlobFileUploader].[UploadAllLogs] - GUID: {fileGuid}");
                                        break;
                                    }

                                    // Check for a proper KLog instance "footer"
                                    if (line.Contains("#KLOG_INSTANCE_STATUS#"))
                                    {
                                        var checkUploadLastLine = UploadLastLine.Execute(line, uploaderLog);

                                        if (!checkUploadLastLine)
                                        {
                                            uploaderLog.Error($"[BlobFileUploader].[UploadLastLine] - GUID: {fileGuid}");
                                        }
                                    }
                                    else
                                    {
                                        var checkUploadInstanceStop = UploadInstanceStop.Execute(line, fileGuid, uploaderLog);

                                        if (!checkUploadInstanceStop)
                                        {
                                            uploaderLog.Error($"[BlobFileUploader].[UploadInstanceStop] - GUID: {fileGuid}");
                                        }
                                    }

                                    uploaderLog.Info($"Log Loaded - Guid: {fileGuid.ToString()}");
                                }

                                //
                                // Check line for "normal" log record, add to collection to bulk add later.
                                //
                                else
                                {
                                    var checkAddLogToCollection = AddLogToCollection.Execute(line, recordModelList, uploaderLog);

                                    if (!checkAddLogToCollection)
                                    {
                                        uploaderLog.Error($"[BlobFileUploader].[AddLogToCollection] - GUID: {fileGuid}");
                                        break;
                                    }

                                    lineCounter++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            uploaderLog.Error($"Blob Upload Failure - Exception: {ex}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    uploaderLog.Error($"BlobFileUploader Exception: {ex}");
                }
            }
        }
    }
}
