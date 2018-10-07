using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Specialized;

// Kiroku
using Kiroku;
using System.Configuration;

namespace KLOGLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start instance level logging
            KManager.Online((NameValueCollection)ConfigurationManager.GetSection("Kiroku"));

            BlobClient.Set();

            BlobFileCollector.Execute();

            BlobFileCheck.Execute();

            BlobFileUploader.Execute();

            BlobFileRetention.Execute();

            #region Legacy V1

            /*
            // Prase Connection String => Create Client => Aquire Container => Collect Directories
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(Global.AzureContainer);
            //string blobPrefix = null;
            //bool useFlatBlobListing = false;
            IEnumerable<IListBlobItem> blobCollection = blobContainer.ListBlobs(null, false, BlobListingDetails.None);

            // blobPrefixName = Directory Name of folders inside the Container
            List<string> blobPrefixNames = blobCollection.OfType<CloudBlobDirectory>().Select(b => b.Prefix).ToList();

            // for each dir in the blob container
            foreach (var blobPrefixName in blobPrefixNames)
            {
                IEnumerable<IListBlobItem> prefixblobs = blobContainer.ListBlobs(blobPrefixName, false, BlobListingDetails.None);
                List<string> prefixblobFileNames = prefixblobs.OfType<CloudBlockBlob>().Select(b => b.Name).ToList();

                // for each file in the blob PrefixName/Directory
                foreach (var file in prefixblobFileNames)
                {
                    Console.WriteLine(file);

                    Guid parseGuid = new Guid();
                    DateTimeOffset? checkTime = new DateTimeOffset();
                    List<LogRecordModel> recordModelList = new List<LogRecordModel>();

                    var fileNameWithoutPrefix = file.Replace(blobPrefixName, "");

                    // Check and parse the fill log file name
                    if (fileNameWithoutPrefix.Count() == 47 && fileNameWithoutPrefix.Contains("KLOG_"))
                    {
                        parseGuid = Guid.Parse(fileNameWithoutPrefix.Substring(7, 36));

                        var parseTag = fileNameWithoutPrefix.Substring(1, 6);

                        Console.WriteLine("{0}: {1}", parseGuid, parseTag);
                    }
                    else
                    {
                        Console.WriteLine("Failed: {0}: {1}", fileNameWithoutPrefix.ToString(), fileNameWithoutPrefix.Count());
                        break;
                    }

                    // Check if GUID is currently in database
                    var result = DataAccessor.CheckInstanceId(parseGuid);

                    Console.WriteLine($"Instance Check: {result}");

                    // TODO: clean-up logic to check result
                    bool checkIfInstanceIdAlreadyExist = false;

                    if (result != Guid.Empty)
                    {
                        checkIfInstanceIdAlreadyExist = true;
                    }
                    else
                    {
                        checkIfInstanceIdAlreadyExist = false;
                    }

                    // Check if the instance exist inside the sql database
                    if (!checkIfInstanceIdAlreadyExist)
                    {
                        using (KLog log = new KLog("test"))
                        {
                            var payload = blobContainer.GetBlobReference(file);

                            payload.FetchAttributes();

                            checkTime = payload.Properties.Created;

                            Console.WriteLine($"Blob Created: {checkTime}");
                            log.Info($"Blob Created: {checkTime}");

                            List<string> strContent = new List<string>();

                            using (StreamReader reader = new StreamReader(payload.OpenRead()))
                            {
                                while (!reader.EndOfStream)
                                {
                                    strContent.Add(reader.ReadLine());
                                }
                            }

                            var strContentStart = 1;
                            var strContentCount = strContent.Count();

                            Console.WriteLine("File Count: {0}", strContentCount);
                            log.Info($"File Count: {strContentCount}");

                            // action line in the log file. check first and last line for instance data. break on header fail. 
                            foreach (var line in strContent)
                            {

                                // first line -- expecting the instane "header"
                                if (strContentStart == 1)
                                {
                                    if (line.Contains("#KLOG_INSTANCE_STATUS#"))
                                    {
                                        var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

                                        var instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);

                                        Console.WriteLine($"{instanceHeader.ApplicationID} | {instanceHeader.InstanceID} | {instanceHeader.InstanceStatus}");
                                        log.Info($"{instanceHeader.ApplicationID} | {instanceHeader.InstanceID} | {instanceHeader.InstanceStatus}");

                                        DataAccessor.AddInstanceStart(instanceHeader);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{parseGuid.ToString()} : Header Fail");
                                        log.Info($"{parseGuid.ToString()} : Header Fail");
                                        break;
                                    }

                                    strContentStart++;
                                }

                                // last line -- expecting the instance "footer"
                                else if (strContentStart == strContentCount)
                                {
                                    if (line.Contains("#KLOG_INSTANCE_STATUS#"))
                                    {

                                        var cleanLine = line.Replace("#KLOG_INSTANCE_STATUS#", "");

                                        var instanceHeader = JsonConvert.DeserializeObject<InstanceModel>(cleanLine);

                                        Console.WriteLine($"{instanceHeader.ApplicationID} | {instanceHeader.InstanceID} | {instanceHeader.InstanceStatus}");
                                        log.Info($"{instanceHeader.ApplicationID} | {instanceHeader.InstanceID} | {instanceHeader.InstanceStatus}");

                                        DataAccessor.UpdateInstanceStop(instanceHeader);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Footer Fail");
                                        log.Info("Footer Fail");

                                        var record = JsonConvert.DeserializeObject<LogRecordModel>(line);

                                        var newEndTime = record.EventTime;

                                        InstanceModel newInstanceCloser = new InstanceModel();

                                        newInstanceCloser.InstanceID = parseGuid;
                                        newInstanceCloser.EventTime = newEndTime;

                                        DataAccessor.UpdateInstanceStop(newInstanceCloser);

                                        DataAccessor.UpdateBlockEmptyStop(record, parseGuid);
                                    }
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
                                        Console.WriteLine($"Line Exception: {line}");
                                        log.Info($"Line Exception: {line}");
                                    }

                                    strContentStart++;
                                }
                            }
                        }
                    }

                    // instance id exist -- check for retention
                    else
                    {
                        var payload = blobContainer.GetBlobReference(file);

                        checkTime = payload.Properties.Created;

                        Console.WriteLine($"Blob Created: {checkTime.ToString()}");
                    }

                    Console.WriteLine($"Lines: {recordModelList.Count()}");

                    // TODO: why is this here...?
                    DataAccessor.AddLogs(recordModelList, parseGuid);

                    // retention

                    var check = ((DateTime.UtcNow.AddDays(Global.RetentionDays)) < checkTime) ? "Hold File" : "Mark File for Retention";

                    Console.WriteLine($"Retention Status: {check}");

                    // delete blob if true, but check

                } // End of foreach
            }
            */
            
            #endregion

            // End instance level logging
            KManager.Offline();

            Console.ReadKey();
        }
    }
}
