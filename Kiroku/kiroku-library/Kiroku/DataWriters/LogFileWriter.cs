namespace Kiroku
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    // Implements Utility Library
    using Implements;

    /// <summary>
    /// CLASS: Contains all local I/O write operations.
    /// </summary>
    class LogFileWriter
    {
        #region Start Instance (Static)

        /// <summary>
        /// Create a new local KLOG file with the instance header as the first line.
        /// </summary>
        /// <param name="instanceStatus"></param>
        public static void StartInstance(string instanceStatus)
        {
            try
            {
                using (LogInstance logInstance = new LogInstance(instanceStatus))
                {
                    var _logInstancestring = JsonConvert.SerializeObject(logInstance);

                    using (StreamWriter file = new StreamWriter(LogConfiguration.FullFilePath, true))
                    {
                        file.WriteLine(LogType.InstanceStatusTag + _logInstancestring);
                    }

                    if (LogConfiguration.WriteVerbose == "1")
                    {
                        LogVerboseWriter.Write(logInstance);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[LogFileWriter].[StartInstance] - Exception: {ex.ToString()}");
            }
        }

        #endregion

        #region Start Instance (Dynamic)

        /// <summary>
        /// Create a new local KLOG file with the instance header as the first line with instance Id.
        /// </summary>
        /// <param name="instanceStatus"></param>
        public static void StartInstanceWithId(string instanceStatus, Guid instance)
        {
            try
            {
                using (LogInstance logInstance = new LogInstance(instanceStatus, instance))
                {
                    var _logInstancestring = JsonConvert.SerializeObject(logInstance);

                    using (StreamWriter file = new StreamWriter(LogConfiguration.FullFilePath + instance.ToString() + ".txt", true))
                    {
                        file.WriteLine(LogType.InstanceStatusTag + _logInstancestring);
                    }

                    if (LogConfiguration.WriteVerbose == "1")
                    {
                        LogVerboseWriter.Write(logInstance);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[LogFileWriter].[StartInstanceWithId] - Exception: {ex.ToString()}");
            }
        }

        #endregion

        #region Stop Instance (Static)

        /// <summary>
        /// Add the closing instance footer to the local LOG file as the last line.
        /// </summary>
        /// <param name="instanceStatus"></param>
        public static void StopInstance(string instanceStatus)
        {
            try
            {
                using (LogInstance logInstance = new LogInstance(instanceStatus))
                {
                    var _logInstancestring = JsonConvert.SerializeObject(logInstance);

                    using (StreamWriter file = File.AppendText(LogConfiguration.FullFilePath))
                    {
                        file.WriteLine(LogType.InstanceStatusTag + _logInstancestring);
                    }

                    if (LogConfiguration.WriteVerbose == "1")
                    {
                        LogVerboseWriter.Write(logInstance);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[LogFileWriter].[StopInstance] - Exception: {ex.ToString()}");
            }
        }

        #endregion 

        #region Stop Instance (Dynamic)

        /// <summary>
        /// Add the closing instance footer to the local LOG file as the last line by instance Id.
        /// </summary>
        /// <param name="instanceStatus"></param>
        public static void StopInstanceWithId(string instanceStatus, Guid instance)
        {
            try
            {
                using (LogInstance logInstance = new LogInstance(instanceStatus, instance))
                {
                    var _logInstancestring = JsonConvert.SerializeObject(logInstance);

                    using (StreamWriter file = File.AppendText(LogConfiguration.FullFilePath + instance.ToString() + ".txt"))
                    {
                        file.WriteLine(LogType.InstanceStatusTag + _logInstancestring);
                    }

                    if (LogConfiguration.WriteVerbose == "1")
                    {
                        LogVerboseWriter.Write(logInstance);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[LogFileWriter].[StopInstanceWithId] - Exception: {ex.ToString()}");
            }
        }

        #endregion

        #region Add Record (Static)

        /// <summary>
        /// Write a single log record to the local KLOG file.
        /// </summary>
        /// <param name="logRecordPayload"></param>
        public static void AddRecord(LogRecord logRecordPayload)
        {
            try
            {
                var _logRecordEntry = JsonConvert.SerializeObject(logRecordPayload);

                using (StreamWriter file = File.AppendText(LogConfiguration.FullFilePath))
                {
                    file.WriteLine(_logRecordEntry);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[LogFileWriter].[AddRecord] - Exception: {ex.ToString()}");
            }
        }

        #endregion

        #region Add Record (Dynamic)

        /// <summary>
        /// Write a single log record to the local KLOG file with instance Id.
        /// </summary>
        /// <param name="logRecordPayload"></param>
        public static void AddRecord(LogRecord logRecordPayload, Guid instance)
        {
            try
            {
                var _logRecordEntry = JsonConvert.SerializeObject(logRecordPayload);

                using (StreamWriter file = File.AppendText(LogConfiguration.FullFilePath + instance.ToString() + ".txt"))
                {
                    file.WriteLine(_logRecordEntry);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[LogFileWriter].[AddRecord] - Exception: {ex.ToString()}");
            }
        }

        #endregion
    }
}
