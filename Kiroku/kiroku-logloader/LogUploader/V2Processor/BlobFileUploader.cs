using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

// Kiroku
using Kiroku;

namespace KLOGLoader
{
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
                        var cloudFile = blobFile.CloudFile;
                        var fileGuid = blobFile.FileGuid;

                        var payload = BlobClient.GetPayload(cloudFile);

                        List<LogRecordModel> recordModelList = new List<LogRecordModel>();
                        List<string> lines = new List<string>();

                        using (StreamReader reader = new StreamReader(payload.OpenRead()))
                        {
                            while (!reader.EndOfStream)
                            {
                                lines.Add(reader.ReadLine());
                            }
                        }

                        var lineCounter = 1;
                        var lineCountTotal = lines.Count();

                        uploaderLog.Info($"Uploader => Starting Upload - Guid: {fileGuid.ToString()} Line Count: {lineCountTotal} ");

                        // action line in the log file. check first and last line for instance data. break on header fail. 
                        foreach (var line in lines)
                        {
                            // first line -- expecting the instane "header"
                            if (lineCounter == 1)
                            {
                                if (line.Contains("#KLOG_INSTANCE_STATUS#"))
                                {
                                    var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

                                    InstanceModel instanceHeader;

                                    try
                                    {
                                        instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);

                                        var checkAddInstanceStart = DataAccessor.AddInstanceStart(instanceHeader);

                                        if (!checkAddInstanceStart.Success)
                                        {
                                            uploaderLog.Error($"SQL Expection on [AddInstanceStart] - Message: {checkAddInstanceStart.Message}");

                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        uploaderLog.Error($"Expection on [DeserializeInstanceHeader] - Message: {ex.ToString()}");

                                        break;
                                    }
                                }
                                else
                                {
                                    uploaderLog.Error($"Uploader => Header Check Failed - Guid: {fileGuid.ToString()} Line: {line}");

                                    BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).HeaderStatus = false;
                                    BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).LogStatus = false;
                                    BlobFileCollection.GetFiles().First(d => d.FileGuid == fileGuid).FooterStatus = false;

                                    break;
                                }

                                lineCounter++;
                            }

                            // last line -- expecting the instance "footer"
                            else if (lineCounter == lineCountTotal)
                            {
                                // write all logs no matter if footer exists
                                var checkAddInstanceStop = DataAccessor.AddLogs(recordModelList, fileGuid);

                                if (!checkAddInstanceStop.Success)
                                {
                                    uploaderLog.Error($"SQL Expection on [AddInstanceStop] - Message: {checkAddInstanceStop.Message}");

                                    break;
                                }

                                // check if there is a proper footer
                                if (line.Contains("#KLOG_INSTANCE_STATUS#"))
                                {
                                    var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

                                    InstanceModel instanceHeader = null;
                                    try
                                    {
                                        instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);
                                    }
                                    catch (Exception ex)
                                    {
                                        uploaderLog.Error($"Expection on [DeserializeInstanceFooter] - Message: {ex.ToString()}");
                                    }

                                    var checkUpdateInstanceStop = DataAccessor.UpdateInstanceStop(instanceHeader);

                                    if (!checkUpdateInstanceStop.Success)
                                    {
                                        uploaderLog.Error($"SQL Expection on [UpdateInstanceStop] - Message: {checkUpdateInstanceStop.Message}");

                                        break;
                                    }
                                }
                                else
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
                                        uploaderLog.Error($"Log Record Deserialize Exception: {ex.ToString()}");
                                    }

                                    var newEndTime = record.EventTime;

                                    // TODO: "package" after creatation
                                    InstanceModel newInstanceCloser = new InstanceModel();

                                    newInstanceCloser.InstanceID = fileGuid;
                                    newInstanceCloser.EventTime = newEndTime;

                                    var checkUpdateInstaceClose = DataAccessor.UpdateInstanceStop(newInstanceCloser);

                                    if (!checkUpdateInstaceClose.Success)
                                    {
                                        uploaderLog.Error($"SQL Expection on [UpdateInstaceClose] - Message: {checkUpdateInstaceClose.Message}");

                                        break;
                                    }

                                    var checkUpdateBlockStop = DataAccessor.UpdateBlockEmptyStop(record, fileGuid);

                                    if (!checkUpdateBlockStop.Success)
                                    {
                                        uploaderLog.Error($"SQL Expection on [UpdateBlockStop] - Message: {checkUpdateBlockStop.Message}");

                                        break;
                                    }
                                }

                                uploaderLog.Info($"Log Loaded - Guid: {fileGuid.ToString()}");
                            }

                            // "normal" log record
                            else
                            {
                                try
                                {
                                    var record = JsonConvert.DeserializeObject<LogRecordModel>(line);

                                    recordModelList.Add(record);
                                }
                                catch
                                {
                                    uploaderLog.Error($"Uploader => Line Exception: {line}");

                                    break;
                                }

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
    }
}
