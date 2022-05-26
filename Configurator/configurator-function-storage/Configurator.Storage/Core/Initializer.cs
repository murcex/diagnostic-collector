namespace Configurator.Storage.Core
{
    using Configurator.Storage.Appliance;
    using System.Collections.Generic;

    class Initializer
    {
        private static Dictionary<string, Dictionary<string, string>> _config;

        public static bool Execute(Dictionary<string, Dictionary<string, string>> configuration)
        {
            if (configuration == null 
                || configuration.Count == 0)
            {
                return false;
            }

            _config = configuration;

            return (SetAppliacation()
                && SetAppliances());
        }

        private static bool SetAppliacation()
        {
            return Configuration.Load(_config);
        }

        private static bool SetAppliances()
        {
            return (Logger.Configure(_config)
                && StorageClient.Configure());
        }
    }
}
