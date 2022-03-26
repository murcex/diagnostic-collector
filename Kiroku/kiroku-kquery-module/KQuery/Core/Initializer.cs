namespace KQuery.Core
{
    using System.Collections.Generic;
    using KQuery.Appliance;

    class Initializer
    {
        private static Dictionary<string, bool> _status;

        /// <summary>
        /// Internal KQuery initializer.
        /// </summary>
        public static bool Execute(
            List<KeyValuePair<string, string>> kqueryConfig,
            List<KeyValuePair<string, string>> kirokuConfig)
        {
            _status = new Dictionary<string, bool>();

            _status.Add("SetApplication", SetApplication(kqueryConfig));

            _status.Add("SetAppliances", SetAppliances(kirokuConfig));

            return true;
        }

        /// <summary>
        /// Setup KQuery service configuration.
        /// </summary>
        private static bool SetApplication(List<KeyValuePair<string, string>> kqueryConfig)
        {
            return Configuration.SetKQueryConfig(kqueryConfig);
        }

        /// <summary>
        /// Setup KQuery internal appliances.
        /// </summary>
        private static bool SetAppliances(List<KeyValuePair<string, string>> kirokuConfig)
        {
            return (
                Logger.Configure(kirokuConfig) 
                && StorageClient.Configure());
        }
    }
}
