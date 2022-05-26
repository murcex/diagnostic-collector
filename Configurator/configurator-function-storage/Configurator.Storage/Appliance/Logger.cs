namespace Configurator.Storage.Appliance
{
    using System.Collections.Generic;
    using Kiroku;

    class Logger
    {
        /// <summary>
        /// Configure Kiroku logger for Configurator.
        /// </summary>
        public static bool Configure(Dictionary<string,Dictionary<string,string>> configuration)
        {
            if (configuration.TryGetValue("Kiroku", out Dictionary<string, string> config))
            {
                var legacyKirokuConfiguration = ConvertToKvpList(config);

                KManager.Configure(legacyKirokuConfiguration, dynamic: true);

                return true;
            }

            return false;
        }

        private static List<KeyValuePair<string,string>> ConvertToKvpList(Dictionary<string, string> configuration)
        {
            return null;
        }
    }
}
