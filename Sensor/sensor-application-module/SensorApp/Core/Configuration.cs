namespace SensorApp
{
    using System.Collections.Generic;
    using Implements;
    using Configurator;
    using Sensor;

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
            var cfg = CfgManager.GetCfg();

            if (CfgManager.CheckCfg(cfg, out string errorMsg))
            {
                using (Deserializer deserilaizer = new Deserializer())
                {
                    var _file = @"D:\home\data\app\cfg\Config.ini";

                    deserilaizer.Execute(_file);

                    _sensorAppCfg = deserilaizer.GetTag("sensor");
                }

                return true;
            }
            else
            {
                // TODO: add logging
            }

            return false;
        }

        /// <summary>
        /// Initialize all applications from config properties through config backing fields.
        /// </summary>
        private static bool SetAppConfig()
        {

            AddInitStatus("SensorApp", SensorManager.Initialize(SensorAppCfg, SensorKLogCfg));

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

        /// ---
        /// Application config properties and backing fields.
        /// ---
        
        // Sensor
        private static List<KeyValuePair<string, string>> SensorAppCfg { get { return _sensorAppCfg; } }
        private static List<KeyValuePair<string, string>> _sensorAppCfg;
        private static List<KeyValuePair<string, string>> SensorKLogCfg { get { return _sensorKLogCfg; } }
        private static List<KeyValuePair<string, string>> _sensorKLogCfg;
    }
}
