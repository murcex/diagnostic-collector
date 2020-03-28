namespace KCopy
{
    using System;
    using System.Collections.Generic;
    using Kiroku;

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
        public static bool Initialize(List<KeyValuePair<string, string>> kcopyConfig, List<KeyValuePair<string, string>> kirokuConfig)
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
                return _configOnline = Configuration.SetConfigs(kcopyConfig, kirokuConfig);
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
                Configuration.StartLogging();

                // Log global properties
                using (KLog logConfig = new KLog("ClassExecute-LogicConfig"))
                {
                    logConfig.Info($"Config Local Directory: {Configuration.LocalDirectory}");
                    logConfig.Info($"Config Azure Container: {Configuration.AzureContainer}");
                    logConfig.Info($"Config Retention: {Configuration.RetentionDays}");
                    logConfig.Info($"Config Cleanse: {Configuration.CleanseHours}");
                }

                Capsule.AddLogFiles(CollectLogs.Execute(Configuration.LocalDirectory));

                DeleteLogs.Execute();

                SendLogs.Execute();

                CleanseLogs.Execute();

                Configuration.StopLogging();

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception($"KCOPY EXCEPTION: {ex}");
            }
        }
    }
}
