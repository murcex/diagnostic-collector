namespace KCopy.Core
{
    using System;
    using System.Collections.Generic;
    using Kiroku;
    using Implements.Substrate.Blob;
    using KCopy.Appliance;

    static class Initializer
    {
        private static Dictionary<string, bool> _status;

        /// <summary>
        /// Set KCopy app and logging configs.
        /// </summary>
        /// <param name="kcopyConfig"></param>
        /// <param name="kirokuConfig"></param>
        public static bool Execute(
            List<KeyValuePair<string, string>> kcopyConfig, 
            List<KeyValuePair<string, string>> kirokuConfig)
        {
            _status = new Dictionary<string, bool>();

            _status.Add("SetConfiguration", SetApplication(kcopyConfig));

            _status.Add("SetGeneral", SetAppliances(kirokuConfig));

            return true;
        }

        /// <summary>
        /// Parse and set KCopy config package.
        /// </summary>
        private static bool SetApplication(List<KeyValuePair<string, string>> kcopyConfig)
        {
            Configuration.SetKCopyConfig(kcopyConfig);

            return true;
        }

        private static bool SetAppliances(List<KeyValuePair<string, string>> kirokuConfig)
        {
            ComputeVariables();

            // Setup KLOG
            var logStatus = Logger.Configure(kirokuConfig);

            // Setup Storage Client
            var storageStatus = StorageClient.Configure();

            return true;
        }

        // Computed Vars
        private static bool ComputeVariables()
        {
            Configuration.AddRetentionThreshold(GetRetentionThreshold());

            Configuration.AddCleanseThreshold(GetCleanseThreshold());

            return true;
        }

        private static DateTime GetRetentionThreshold()
        {
            var _retention = Convert.ToInt32(Configuration.RetentionDays);

            if (_retention <= 0)
            {
                return (DateTime.UtcNow.AddDays(0));
            }
            else
            {
                return (DateTime.UtcNow.AddDays(_retention));
            }
        }

        private static DateTime GetCleanseThreshold()
        {
            var _cleanse = Convert.ToInt32(Configuration.CleanseHours);

            if (_cleanse <= 0)
            {
                return (DateTime.UtcNow.AddHours(0));
            }
            else
            {
                return (DateTime.UtcNow.AddHours(_cleanse));
            }
        }
    }
}
