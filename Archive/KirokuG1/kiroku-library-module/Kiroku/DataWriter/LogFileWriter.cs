namespace Kiroku
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    // Implements Utility Library
    //using Implements;

    /// <summary>
    /// CLASS: Contains all local I/O write operations.
    /// </summary>
    class LogFileWriter
    {
        private static object _lock = new object();

        #region Add Instance Event

        /// <summary>
        /// Start local instance KLOG file.
        /// </summary>
        /// <param name="logInstance"></param>
        public static void AddInstanceEvent(ILog logInstance)
        {
            lock (_lock)
            {
                try
                {
                    var _logInstanceString = JsonConvert.SerializeObject(logInstance);

                    var filepath = logInstance.FilePath + logInstance.InstanceID + KConstants.s_FileExt;

                    using (StreamWriter file = new StreamWriter(filepath, true))
                    {
                        file.WriteLine(KConstants.s_InstanceStatusTag + _logInstanceString);
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Replace => Log.Error($"[LogFileWriter].[StartInstance] - Exception: {ex.ToString()}");
                }
            }
        }

        #endregion

        #region Add Record Event

        /// <summary>
        /// Write a single log record to the local KLOG file.
        /// </summary>
        /// <param name="logRecord"></param>
        public static void AddRecordEvent(ILog logRecord)
        {
            lock (_lock)
            {
                try
                {
                    var _logInstanceString = JsonConvert.SerializeObject(logRecord);

                    var filepath = logRecord.FilePath + logRecord.InstanceID + KConstants.s_FileExt;

                    using (StreamWriter file = new StreamWriter(filepath, true))
                    {
                        file.WriteLine(_logInstanceString);
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Replace => Log.Error($"[LogFileWriter].[AddRecord] - Exception: {ex.ToString()}");
                }
            }
        }

        #endregion
    }
}