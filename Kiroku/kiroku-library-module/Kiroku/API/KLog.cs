namespace Kiroku
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;

    /// <summary>
    /// DATA MODEL: Contain data for a single log event.
    /// </summary>
    public class KLog : IDisposable
    {
        #region Variables 

        private bool dispose = false;
        private bool entryNode = false;
        private Guid blockID;
        private string blockName;
        private bool resultStatus;
        private AppConfiguration config;
        private Guid instanceId;

        #endregion

        #region Constructors

        /// <summary>
        /// Boot strapping block id and block name -- create instance if dynamically logging.
        /// </summary>
        /// <param name="blockName">The friendly block name</param>
        public KLog(string blockName)
        {
            blockID = Guid.NewGuid();
            this.blockName = blockName;

            string appName = Assembly.GetCallingAssembly().GetName().Name.ToUpper();
            config = KConfiguration.GetConfig(appName);

            if (config.Dynamic)
            {
                instanceId = KManager.CreateDynamicInstance(config);
                entryNode = true;
            }
            else
            {
                instanceId = KConfiguration.GetStaticInstanceId(appName);
            }

            Start();
        }

        /// <summary>
        /// Log constructor, boot strapping dynamic klog block id and block name -- nested inside primary node's instance id.
        /// </summary>
        /// <param name="blockName"></param>
        /// <param name="klog"></param>
        public KLog(string blockName, KLog klog)
        {
            blockID = Guid.NewGuid();
            this.blockName = blockName;

            string appName = Assembly.GetCallingAssembly().GetName().Name.ToUpper();
            config = KConfiguration.GetConfig(appName);

            if (!config.Dynamic)
            {
                throw new Exception($"Linked KLog's only supported in Dynamic logging mode.");
            }
            else
            {
                instanceId = klog.instanceId;
            }

            Start();
        }

        #endregion

        #region Start/Stop Log Block

        /// <summary>
        /// Log Token/Tag for Start/Stop. Used for parsing, tracking and time.
        /// </summary>

        /// <summary>
        /// Start instance.
        /// </summary>
        private void Start()
        {
            resultStatus = false;
            LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_Start, KConstants.s_StartTag);
        }

        /// <summary>
        /// Stop instance.
        /// </summary>
        private void Stop()
        {
            if (!resultStatus)
            {
                Success("Block Success, Default Disposal");
            }

            LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_Stop, KConstants.s_StopTag);
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
            if (config.Trace)
            {
                LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_TraceEvent, logData);
            }
        }

        // Info
        public void Info(string logData)
        {
            if (config.Info)
            {
                LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_InfoEvent, logData);
            }
        }

        /// <summary>
        /// Warning entry.
        /// </summary>
        /// <param name="logData"></param>
        public void Warning(string logData)
        {
            if (config.Warning)
            {
                LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_WarningEvent, logData);
            }
        }

        /// <summary>
        /// Error entry.
        /// </summary>
        /// <param name="logData"></param>
        public void Error(string logData)
        {
            if (config.Error)
            {
                LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_ErrorEvent, logData);
            }
        }

        /// <summary>
        /// Error entry. Unwarp inner exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logData"></param>
        public void Error(Exception ex, string logData)
        {
            if (config.Error)
            {
                // new logic to pull all inner exceptions in layer, tagging each layer, appending to sting.
                var ex2 = ex.ToString();

                // pass both the inner exception collection and any additiona information passed in on the method down the injector.
                var logPayload = "Exception Stack:" + ex + " Additonal information: " + logData;

                LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_ErrorEvent, logData);
            }
        }

        /// <summary>
        /// Metric entry.
        /// </summary>
        /// <param name="metricName">Metric Name</param>
        /// <param name="metricValue">Metric Value</param>
        public void Metric(string metricName, object metricValue)
        {
            if (config.Metric)
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

                LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_MetricEvent, logData);
            }
        }

        /// <summary>
        /// Mark the KLOG Block result as a success.
        /// </summary>
        /// <param name="logData">Success message</param>
        public void Success(string logData = "Block Success")
        {
            resultStatus = true;
            var resultData = ResultBuilder(1, logData);
            LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_ResultEvent, resultData);
        }

        /// <summary>
        /// Mark the KLOG Block result as a failure.
        /// </summary>
        /// <param name="logData">Failure message</param>
        public void Failure(string logData = "Block Failure")
        {
            resultStatus = true;
            var resultData = ResultBuilder(0, logData);
            LogInjector(instanceId, config.FullFilePath, blockID, blockName, KConstants.s_ResultEvent, resultData);
        }

        #endregion

        #region Metric Builder

        /// <summary>
        /// Convert Metric parameters into a single safe JSON string for logging.
        /// </summary>
        /// <param name="metricName"></param>
        /// <param name="metricType"></param>
        /// <param name="metricValue"></param>
        /// <returns></returns>
        private static string MetricBuilder(string metricName, string metricType, object metricValue)
        {
            Dictionary<string, string> metricRecord = new Dictionary<string, string>();
            metricRecord.Add("MetricName", metricName);
            metricRecord.Add("MetricType", metricType);
            metricRecord.Add("MetricValue", metricValue.ToString());

            var jsonString = JsonConvert.SerializeObject(metricRecord);

            return jsonString.Replace("\"", "#");
        }

        #endregion

        #region Result Builder

        /// <summary>
        /// Convert Result parameters into a single safe JSON string for logging.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="resultData"></param>
        /// <returns></returns>
        private static string ResultBuilder(int result, string resultData)
        {
            Dictionary<string, string> resultRecord = new Dictionary<string, string>();
            resultRecord.Add("Result", result.ToString());
            resultRecord.Add("ResultData", resultData);

            var jsonString = JsonConvert.SerializeObject(resultRecord);

            return jsonString.Replace("\"", "#");
        }

        #endregion

        #region Log Injector

        /// <summary>
        /// The log injection funnel is a layer between the Log Type "switch" class and the Logging action classes.
        /// The entry is evaluated for both a (1) log write action and (2) verbose console feedback action.
        /// </summary>
        /// <param name="blockID">Log block (GUID) ID</param>
        /// <param name="blockName">Log block name</param>
        /// <param name="logType">Log type</param>
        /// <param name="logData">Log data payload</param>
        private void LogInjector(Guid instanceId, string filepath, Guid blockID, string blockName, string logType, string logData)
        {
            using (LogRecord logBase = new LogRecord())
            {
                logBase.BlockID = blockID;
                logBase.LogType = logType;
                logBase.BlockName = blockName;
                logBase.LogData = logData;
                logBase.InstanceID = instanceId;
                logBase.FilePath = filepath;

                if (config.WriteLog)
                {
                    LogFileWriter.AddRecordEvent(logBase);
                }

                if (config.WriteVerbose)
                {
                    LogVerboseWriter.WriteToConsole(logBase);
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

                    if (entryNode)
                    {
                        KManager.CloseDynamicInstance(instanceId, config);
                    }
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
