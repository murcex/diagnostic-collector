namespace Kiroku
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// DATA MODEL: Contain data for a single log event.
    /// </summary>
    public class KLog : IDisposable
    {
        #region Variables 

        bool dispose = false;
        private Guid blockID { get; set; }
        private string blockName { get; set; }
        private bool resultStatus { get; set; }

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
            resultStatus = false;
            LogInjector(blockID, blockName, LogType.Start, LogType.StartTag);
        }

        // Silent Stop
        private void Stop()
        {
            if (!resultStatus)
            {
                Success("Block Success, Default Disposal");
            }

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

        public void Error(Exception ex, string logData)
        {
            if (LogConfiguration.Error == "1")
            {
                // new logic to pull all inner exceptions in layer, tagging each layer, appending to sting.
                var ex2 = ex.ToString();

                // pass both the inner exception collection and any additiona information passed in on the method down the injector.
                var logPayload = "Exception Stack:" + ex + " Additonal information: " + logData;

                LogInjector(blockID, blockName, LogType.Error, logData);
            }
        }

        // Metric
        public void Metric(string metricName, object metricValue)
        {
            if (LogConfiguration.Metric == "1")
            {
                string logData = "No Metric Type Match";

                if (metricValue.GetType() == typeof(int))
                {
                    logData = MetricBuilder(metricName, "int", (int)metricValue);
                }

                if (metricValue.GetType() == typeof(double))
                {
                    logData = MetricBuilder(metricName, "double", (double)metricValue);
                }

                if (metricValue.GetType() == typeof(bool))
                {
                    logData = MetricBuilder(metricName, "bool", (bool)metricValue);
                }

                LogInjector(blockID, blockName, LogType.Metric, logData);
            }
        }

        // Success Result
        public void Success(string logData = "Block Success")
        {
            resultStatus = true;
            LogInjector(blockID, blockName, LogType.Result, logData);
        }

        // Failure Result
        public void Failure(string logData = "Block Failure")
        {
            resultStatus = true;
            LogInjector(blockID, blockName, LogType.Result, logData);
        }

        #endregion

        #region Metric Builder

        /// <summary>
        /// Convert a Metric parameters into a single safe JSON string for logging.
        /// </summary>
        /// <param name="metricName"></param>
        /// <param name="metricType"></param>
        /// <param name="metricValue"></param>
        /// <returns></returns>
        private static string MetricBuilder(string metricName, string metricType, object metricValue)
        {
            Dictionary<string, string> metricRecord = new Dictionary<string, string>();
            metricRecord.Add("Metric Name", metricName);
            metricRecord.Add("Metric Type", metricType);
            metricRecord.Add("Metric Value", metricValue.ToString());

            var jsonString = JsonConvert.SerializeObject(metricRecord);

            return jsonString.Replace("\"", "#");
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
