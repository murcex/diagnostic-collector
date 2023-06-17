namespace Configurator.Storage.Core
{
    using Configurator.Storage.Internal;
    using System;
    using System.Collections.Generic;

    static class Configuration
    {
        /// <summary>
        /// Set the Configurator application operating configs.
        /// </summary>
        public static bool SetConfigs(Dictionary<string, Dictionary<string, string>> config)
        {
            if (config.TryGetValue("embedded", out var components))
            {
                var storageAccount = components["STORAGE_ACCOUNT"];

                var storageContainer = components["STORAGE_CONTAINER"];

                return StorageClient.Initialize(storageAccount, storageContainer);
            }

            throw new InvalidOperationException("embedded index not found in config payload");
        }
    }
}
