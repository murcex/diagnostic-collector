namespace Kiroku
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    // Implements Utility Library
    using Implements;

    /// <summary>
    /// CLASS: Starts and Stops Kiroku instance opertaions.
    /// </summary>
    public class KManager
    {
        #region Online

        /// <summary>
        /// Version 2 -- Testing
        /// </summary>
        /// <param name="config"></param>
        public static void Online(List<KeyValuePair<string, string>> config)
        {
            try
            {
                if (config != null)
                {
                    foreach (var kvp in config)
                    {
                        switch (kvp.Key.ToString())
                        {
                            case "write":
                                LogConfiguration.WriteLog = kvp.Value;
                                break;

                            case "applicationid":
                                LogConfiguration.ApplicationID = kvp.Value;
                                break;

                            case "trackid":
                                LogConfiguration.TrackID = kvp.Value;
                                break;

                            case "regionid":
                                LogConfiguration.RegionID = kvp.Value;
                                break;

                            case "clusterid":
                                LogConfiguration.ClusterID = kvp.Value;
                                break;

                            case "deviceid":
                                LogConfiguration.DeviceID = kvp.Value;
                                break;

                            case "verbose":
                                LogConfiguration.WriteVerbose = kvp.Value;
                                break;

                            case "trace":
                                LogConfiguration.Trace = kvp.Value;
                                break;

                            case "info":
                                LogConfiguration.Info = kvp.Value;
                                break;

                            case "warning":
                                LogConfiguration.Warning = kvp.Value;
                                break;

                            case "error":
                                LogConfiguration.Error = kvp.Value;
                                break;

                            case "filepath":
                                LogConfiguration.RootFilePath = kvp.Value;
                                break;

                            default:
                                {
                                    System.Console.WriteLine("Not Hit: {0}", kvp.Value);
                                }
                                break;
                        }
                    }

                    // Set kiroku.dll version
                    LogConfiguration.Version = Utility.GetAssemblyVersion("kiroku");

                    // Set session date
                    LogConfiguration.Datetime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

                    // Set instance Guid
                    LogConfiguration.InstanceID = Guid.NewGuid();

                    // Create file path
                    LogConfiguration.FullFilePath = LogConfiguration.RootFilePath + LogType.WritingToLog + LogConfiguration.InstanceID + ".txt";

                    // Trigger file creation and first write -- as header
                    LogFileWriter.StartInstance(LogType.InstanceStart);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[KManager].[Online] - Exception: {ex.ToString()}");
            }
        }

        #endregion

        #region Offline

        /// <summary>
        /// Signals the end of KLog operations for an instance, closing KLog instance log, renaming the KLOG file for sending operation.
        /// </summary>
        public static void Offline()
        {
            LogFileWriter.StopInstance(LogType.InstanceStop);

            try
            {
                // Rename from KLOG_W_$(guid) to KLOG_S_$(guid) -- this will maket the log available for transmission
                File.Move(LogConfiguration.FullFilePath, (LogConfiguration.RootFilePath + LogType.ReadyToSend + LogConfiguration.InstanceID + ".txt"));
            }
            catch (Exception ex)
            {
                Log.Error($"[KManager].[Offline] - Exception: {ex.ToString()}");
            }
        }

        #endregion
    }
}
