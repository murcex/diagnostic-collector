namespace ConfiguratorApp
{
    using System;
    using System.Collections.Generic;
    //using Implements;
    //using Configurator;
    using Configurator.Service;
    using KCopy;

    class Configuration
    {
        /// <summary>
        /// Tracking GetAppConfig() result.
        /// </summary>
        private static bool _extractCfg = false;

        /// <summary>
        /// Tracking SetAppConfig() result.
        /// </summary>
        private static bool _setCfg = false;

        /// <summary>
        /// Tracking dictionary for tracking application initlization status.
        /// </summary>
        private static Dictionary<string, bool> _status;


        /// <summary>
        /// Static constructor. 
        /// </summary>
        static Configuration()
        {
            RegisterConfigs();
        }

        /// <summary>
        /// Initiate registration sequence for all application configs. Extract all configs and then initialize all applications.
        /// </summary>
        private static bool RegisterConfigs()
        {
            if (_extractCfg = GetAppConfigs())
            {
                return _setCfg = SetAppConfig();
            }

            return _extractCfg;
        }

        /// <summary>
        /// Verify the config status of a single application. If the app is null, check if all application initialized.
        /// </summary>
        /// <param name="app"></param>
        public static bool ConfigStatus(string app = null)
        {
            if (_status == null)
            {
                // TODO: add logging

                return false;
            }

            if (string.IsNullOrEmpty(app))
            {
                return !_status.ContainsValue(false);
            }
            else
            {
                _status.TryGetValue(app.ToUpper(), out bool result);

                return result;
            }
        }

        /// <summary>
        /// Read local master config, extracting configs for each application into backing fields.
        /// </summary>
        private static bool GetAppConfigs()
        {
            try
            {
                _kcopyAppCfg = AppendStorageAccount(GetConfig("KCOPY_APP"));

                _kcopyKLogCfg = GetConfig("KCOPY_LOG");
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Initialize all applications from config properties through config backing fields.
        /// </summary>
        private static bool SetAppConfig()
        {
            AddInitStatus("CfgApp", CfgSvcManager.Initialize());

            AddInitStatus("KCopyApp", KCopyManager.Initialize(KCopyAppCfg, KCopyKLogCfg));

            return ConfigStatus();
        }

        /// <summary>
        /// Add the application initalization result to the status tracking dictionary.
        /// </summary>
        /// <param name="app">Applicatin name</param>
        /// <param name="result">Initialize result</param>
        private static void AddInitStatus(string app, bool result)
        {
            if (_status == null)
            {
                _status = new Dictionary<string, bool>();
            }

            if (!string.IsNullOrEmpty(app) && !string.IsNullOrWhiteSpace(app))
            {
                _status[app.ToUpper()] = result;
            }
        }

        /// <summary>
        /// Get a KeyValuePair config from a Azure environment variable.
        /// </summary>
        /// <param name="target">Config target name</param>
        /// <returns>KeyValuePair config</returns>
        private static List<KeyValuePair<string, string>> GetConfig(string target)
        {
            List<KeyValuePair<string, string>> kvp = new List<KeyValuePair<string, string>>();
            string config = string.Empty;

            try
            {
                config = Environment.GetEnvironmentVariable(target, EnvironmentVariableTarget.Process);
            }
            catch
            { }

            try
            {
                if (!string.IsNullOrEmpty(config))
                {
                    // split by , into array
                    var records = config.Split(",");

                    foreach (var record in records)
                    {
                        var recordArray = record.Split("=");

                        var key = recordArray[0];

                        var value = recordArray[1];

                        kvp.Add(new KeyValuePair<string, string>(key, value));
                    }
                }
            }
            catch
            { }

            return kvp;
        }

        /// <summary>
        /// Append the storage account config to existing kvp config.
        /// </summary>
        /// <param name="kvp"></param>
        /// <returns></returns>
        private static List<KeyValuePair<string, string>> AppendStorageAccount(List<KeyValuePair<string, string>> kvp)
        {
            if (kvp == null || kvp.Count < 1)
            {
                return null;
            }

            try
            {
                var config = Environment.GetEnvironmentVariable("KCOPY_STORAGE", EnvironmentVariableTarget.Process);

                if (!string.IsNullOrEmpty(config))
                {
                    kvp.Add(new KeyValuePair<string, string>("storage", config));
                }
            }
            catch
            { }

            return kvp;
        }

        /// ---
        /// Application config properties and backing fields.
        /// ---

        // KCopy
        private static List<KeyValuePair<string, string>> KCopyAppCfg { get { return _kcopyAppCfg; } }
        private static List<KeyValuePair<string, string>> _kcopyAppCfg;
        private static List<KeyValuePair<string, string>> KCopyKLogCfg { get { return _kcopyKLogCfg; } }
        private static List<KeyValuePair<string, string>> _kcopyKLogCfg;
    }
}
