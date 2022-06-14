namespace KCopy
{
    using System;
    using System.Collections.Generic;
    using Kiroku;
    using KCopy.Component;
    using KCopy.Core;
    using KCopy.Appliance;
    using KCopy.Model;

    public class KCopyManager
    {
        private static bool _configOnline = false;

        private static bool _appConfigStatus = false;

        private static bool _logConfigStatus = false;

        /// <summary>
        /// Initialize the KCopy instance, providing the application and logging configs.
        /// </summary>
        /// <param name="kcopyConfig"></param>
        /// <param name="kirokuConfig"></param>
        /// <returns></returns>
        public static bool Initialize(
            List<KeyValuePair<string, string>> kcopyConfig, 
            List<KeyValuePair<string, string>> kirokuConfig)
        {
            // Null checks
            if (kcopyConfig != null)
            {
                _appConfigStatus = true;
            }
            if (kirokuConfig != null)
            {
                _logConfigStatus = true;
            }

            // Push config packages to Configuration logic
            try
            {
                return _configOnline = Initializer.Execute(
                    kcopyConfig, 
                    kirokuConfig);
            }
            catch
            {
                return _configOnline = false;
            }
        }

        /// <summary>
        /// Execute the KCopy application.
        /// </summary>
        /// <returns></returns>
        public static bool Execute()
        {
            if (!_configOnline)
            {
                return false;
            }

            try
            {
                // Start instance level logging
                Logger.StartLogging();

                // Log global properties
                using (KLog logConfig = new KLog("ClassExecute-LogicConfig"))
                {
                    logConfig.Trace($"Config Local Directory: {Configuration.LocalDirectory}");
                    logConfig.Trace($"Config Azure Container: {Configuration.AzureContainer}");
                    logConfig.Trace($"Config Retention: {Configuration.RetentionDays} {Configuration.RetentionThreshold}");
                    logConfig.Trace($"Config Cleanse: {Configuration.CleanseHours} {Configuration.CleanseThreshold}");
                }

                var logFiles = CollectLogs.Execute(Configuration.LocalDirectory);

                Capsule.AddLogFiles(logFiles);

                DeleteLogs.Execute();

                SendLogs.Execute();

                CleanseLogs.Execute();

                Logger.StopLogging();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"KCOPY EXCEPTION: {ex}");
            }
        }
    }
}
