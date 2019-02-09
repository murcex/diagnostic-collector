using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiroku
{
    /// <summary>
    /// Log record injection layer. Evaluate Log record for (1) Write and (2) Verbose.
    /// </summary>
    static class LogRecordInjector
    {
        #region Log Funnel

        /// <summary>
        /// The log injection funnel is a layer between the Log Type "switch" class and the Logging action classes.
        /// The entry is evaluated for both a (1) log write action and (2) verbose console feedback action.
        /// </summary>
        /// <param name="blockID"> Log block (GUID) ID</param>
        /// <param name="blockName">Log block name</param>
        /// <param name="logType">Log type</param>
        /// <param name="logData">Log data payload</param>
        public static void Execute(Guid blockID, string blockName, string logType, string logData)
        {
            //using (LogRecord logBase = new LogRecord())
            //{
            //    logBase.BlockID = blockID;
            //    logBase.LogType = logType;
            //    logBase.BlockName = blockName;
            //    logBase.LogData = logData;

            //    if (LogConfiguration.WriteLog == "1")
            //    {
            //        LogFileWriter.AddRecord(logBase);
            //    }

            //    if (LogConfiguration.WriteVerbose == "1")
            //    {
            //        LogVerboseWriter.Write(logBase);
            //    }
            //}
        }

        #endregion
    }
}
