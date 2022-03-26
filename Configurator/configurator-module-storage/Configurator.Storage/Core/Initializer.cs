namespace Configurator.Storage.Core
{
    using Configurator.Storage.Appliance;

    class Initializer
    {
        public static bool Execute()
        {
            Pylon.GetConfiguration();
        }

        private static bool SetAppliacation()
        {
            return Configuration.SetConfiguratorConfig();
        }

        private static bool SetAppliances()
        {
            return (
                Logger.Configure(kirokuConfig)
                && StorageClient.Configure());
        }
    }
}
