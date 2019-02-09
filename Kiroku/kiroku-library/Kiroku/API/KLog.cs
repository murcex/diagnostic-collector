namespace Kiroku
{
    using System;

    /// <summary>
    /// DATA MODEL: Contain data for a single log event.
    /// </summary>
    public class KLog : IDisposable
    {
        #region Variables 

        bool dispose = false;
        private Guid blockID { get; set; }
        private string blockName { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Log constructor, boot strapping block id and block name
        /// </summary>
        /// <param name="_eventAction">The friendly block name</param>
        public KLog(string _eventAction)
        {
            blockID = Guid.NewGuid();
            blockName = _eventAction;

            Start();
        }

        #endregion

        #region Start/Stop Log Block
        
        /// <summary>
        /// Log Token/Tag for Start/Stop. Used for parsing, tracking and time.
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            LogInjector(blockID, blockName, LogType.Start, LogType.StartTag);
        }

        // Silent Stop
        private void Stop()
        {
            LogInjector(blockID, blockName, LogType.Stop, LogType.StopTag);
        }

        #endregion

        #region Log Event Types

        /// <summary>
        /// Log Types; Trace, Info, Warning, Error. 
        /// A check is preformed to determine if the Type is enabled for injection.
        /// Injection contains additional checks for (1) write to file and/or (2) console verbose.
        /// </summary>
        // Information Types
        // Trace
        public void Trace(string logData)
        {
            if (LogConfiguration.Trace == "1")
            {
               LogInjector(blockID, blockName, LogType.Trace, logData);
            }
        }

        // Info
        public void Info(string logData)
        {
            if (LogConfiguration.Info == "1")
            {
                LogInjector(blockID, blockName, LogType.Info, logData);
            }
        }

        // Alert Types
        // Warning
        public void Warning(string logData)
        {
            if (LogConfiguration.Warning == "1")
            {
                LogInjector(blockID, blockName, LogType.Warning, logData);
            }
        }

        // Error
        public void Error(string logData)
        {
            if (LogConfiguration.Error == "1")
            {
                LogInjector(blockID, blockName, LogType.Error, logData);
            }
        }

        #endregion

        #region Log Injector

        /// <summary>
        /// The log injection funnel is a layer between the Log Type "switch" class and the Logging action classes.
        /// The entry is evaluated for both a (1) log write action and (2) verbose console feedback action.
        /// </summary>
        /// <param name="blockID"> Log block (GUID) ID</param>
        /// <param name="blockName">Log block name</param>
        /// <param name="logType">Log type</param>
        /// <param name="logData">Log data payload</param>
        private static void LogInjector(Guid blockID, string blockName, string logType, string logData)
        {
            using (LogRecord logBase = new LogRecord())
            {
                logBase.BlockID = blockID;
                logBase.LogType = logType;
                logBase.BlockName = blockName;
                logBase.LogData = logData;

                if (LogConfiguration.WriteLog == "1")
                {
                    LogFileWriter.AddRecord(logBase);
                }

                if (LogConfiguration.WriteVerbose == "1")
                {
                    LogVerboseWriter.Write(logBase);
                }
            }
        }

        #endregion

        #region Disposable

        /// <summary>
        /// Disposable Logic
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    //dispose managed resources
                    Stop();
                }
            }
            //dispose unmanaged resources
            dispose = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
