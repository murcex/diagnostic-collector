using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiroku
{
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

        #region Start/Stop
        
        /// <summary>
        /// Log Token/Tag for Start/Stop. Used for parsing, tracking and time.
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            LogRecordInjector.Execute(blockID, blockName, LogType.Start, LogType.StartTag);
        }

        // Silent Stop
        public void Stop()
        {
            LogRecordInjector.Execute(blockID, blockName, LogType.Stop, LogType.StopTag);
        }

        #endregion

        #region Types

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
                LogRecordInjector.Execute(blockID, blockName, LogType.Trace, logData);
            }
        }

        // Info
        public void Info(string logData)
        {
            if (LogConfiguration.Info == "1")
            {
                LogRecordInjector.Execute(blockID, blockName, LogType.Info, logData);
            }
        }

        // Alert Types
        // Warning
        public void Warning(string logData)
        {
            if (LogConfiguration.Warning == "1")
            {
                LogRecordInjector.Execute(blockID, blockName, LogType.Warning, logData);
            }
        }

        // Error
        public void Error(string logData)
        {
            if (LogConfiguration.Error == "1")
            {
                LogRecordInjector.Execute(blockID, blockName, LogType.Error, logData);
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
