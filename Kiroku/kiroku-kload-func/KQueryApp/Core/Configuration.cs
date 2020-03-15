namespace KQueryApp
{
    using System.Collections.Generic;
    using Implements;
    using Configurator;
    using KCopy;
    using KQuery;
    using System;
    using KLoad;

    class Configuration
    {
        static Configuration()
        {
            RegisterConfigs();
        }

        private static bool _extractCfg = false;

        private static bool _setCfg = false;

        private static bool RegisterConfigs()
        {
            if (_extractCfg = GetAppConfigs())
            {
                 return _setCfg = SetAppConfig();
            }

            return _extractCfg;
        }

        public static string ConfigStatus(string app)
        {
            if (string.IsNullOrEmpty(app))
            {
                return "App Name is NullOrEmpty.";
            }

            List<string> msg = new List<string>();

            if (app == "KQueryApp")
            { 
                if (KQueryAppCfg == null)
                {
                    msg.Add("AppCfg=False");
                }
                else
                {
                    msg.Add("AppCfg=True");
                }

                if (KQueryKLogCfg == null)
                {
                    msg.Add("KLogCfg=False");
                }
                else
                {
                    msg.Add("KLogCfg=True");
                }
            }

            if (app == "KCopyApp")
            {
                if (KCopyAppCfg == null)
                {
                    msg.Add("AppCfg=False");
                }
                else
                {
                    msg.Add("AppCfg=True");
                }

                if (KCopyKLogCfg == null)
                {
                    msg.Add("KLogCfg=False");
                }
                else
                {
                    msg.Add("KLogCfg=True");
                }
            }

            if (app == "KLoadApp")
            {
                if (KLoadAppCfg == null)
                {
                    msg.Add("AppCfg=False");
                }
                else
                {
                    msg.Add("AppCfg=True");
                }

                if (KLoadKLogCfg == null)
                {
                    msg.Add("KLogCfg=False");
                }
                else
                {
                    msg.Add("KLogCfg=True");
                }
            }

            return String.Join(String.Empty, msg.ToArray());
        }

        private static bool GetAppConfigs()
        {
            var cfg = CfgManager.GetCfg("kquery");

            if (CfgManager.CheckCfg(cfg, out string errorMsg))
            {
                using (Deserializer deserilaizer = new Deserializer())
                {
                    var _file = @"D:\home\data\kquery\Config.ini";

                    deserilaizer.Execute(_file);

                    _kqueryAppCfg = deserilaizer.GetTag("kquery");

                    _kqueryKLogCfg = deserilaizer.GetTag("kiroku_kquery");

                    _kcopyAppCfg = deserilaizer.GetTag("kcopy");

                    _kcopyKLogCfg = deserilaizer.GetTag("kiroku_kcopy");

                    _kloadAppCfg = deserilaizer.GetTag("kload");

                    _kloadKLogCfg = deserilaizer.GetTag("kiroku_kload");
                }

                return true;
            }
            else
            {
                _errorMsg = errorMsg;
            }

            return false;
        }

        private static bool SetAppConfig()
        {
            var kqueryInt = KQueryManger.Initialize(KQueryAppCfg, KQueryKLogCfg);

            var kcopyInt = KCopyManager.Initialize(KCopyAppCfg, KCopyKLogCfg);

            var kloadInt = KLoadManager.Initialize(KLoadAppCfg, KLoadKLogCfg);

            return (kqueryInt && kcopyInt && kloadInt);
        }

        // Error Message
        private static string _errorMsg;

        /// ---
        /// Application config properties and backing fields.
        /// ---

        // KQuery
        private static List<KeyValuePair<string, string>> KQueryAppCfg { get { return _kqueryAppCfg; } }
        private static List<KeyValuePair<string, string>> _kqueryAppCfg;
        private static List<KeyValuePair<string, string>> KQueryKLogCfg { get { return _kqueryKLogCfg; } }
        private static List<KeyValuePair<string, string>> _kqueryKLogCfg;

        // KCopy
        private static List<KeyValuePair<string, string>> KCopyAppCfg { get { return _kcopyAppCfg; } }
        private static List<KeyValuePair<string, string>> _kcopyAppCfg;
        private static List<KeyValuePair<string, string>> KCopyKLogCfg { get { return _kcopyKLogCfg; } }
        private static List<KeyValuePair<string, string>> _kcopyKLogCfg;
        
        // KLoad
        private static List<KeyValuePair<string, string>> KLoadAppCfg { get { return _kloadAppCfg; } }
        private static List<KeyValuePair<string, string>> _kloadAppCfg;
        private static List<KeyValuePair<string, string>> KLoadKLogCfg { get { return _kloadKLogCfg; } }
        private static List<KeyValuePair<string, string>> _kloadKLogCfg;
    }
}
