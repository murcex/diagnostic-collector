using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Kiroku
{
    /// <summary>
    /// CLASS: Contains all local I/O write operations.
    /// </summary>
    class LogFileWriter
    {
        #region Start Instance

        /// <summary>
        /// Create a new local KLOG file with the instance header as the first line.
        /// </summary>
        /// <param name="instanceStatus"></param>
        public static void StartInstance(string instanceStatus)
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

        #endregion

        #region Stop Instance

        /// <summary>
        /// Add the closing instance footer to the local LOG file as the last line.
        /// </summary>
        /// <param name="instanceStatus"></param>
        public static void StopInstance(string instanceStatus)
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

        #endregion

        #region Add Record

        /// <summary>
        /// Write a single log record to the local KLOG file.
        /// </summary>
        /// <param name="logRecordPayload"></param>
        public static void AddRecord(LogRecord logRecordPayload)
        {
            var _logRecordEntry = JsonConvert.SerializeObject(logRecordPayload);

            using (StreamWriter file = File.AppendText(LogConfiguration.FullFilePath))
            {
                file.WriteLine(_logRecordEntry);
            }
        }

        #endregion
    }
}
