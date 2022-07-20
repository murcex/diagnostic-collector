namespace Configurator.Service
{
    using System;
    using System.Collections.Generic;

    static class Configuration
    {
        /// <summary>
        /// Set the Configurator application operating configs.
        /// </summary>
        public static bool SetConfigs(List<KeyValuePair<string, string>> kirokuConfig)
        {
            return StorageClient.Initialize(StorageAccount, StorageContainer);
        }

        /// <summary>
        /// Get the storage account name and key from a Azure environment variable.
        /// </summary>
        public static string StorageAccount
        {
            get
            {
                string key = string.Empty;

                try
                {
                    key = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT", EnvironmentVariableTarget.Process);
                }
                catch
                { }

                return key;
            }
        }

        /// <summary>
        /// Get the storage container name from a Azure environment variable.
        /// </summary>
        public static string StorageContainer
        {
            get
            {
                string container = string.Empty;

                try
                {
                    container = Environment.GetEnvironmentVariable("STORAGE_CONTAINER", EnvironmentVariableTarget.Process);
                }
                catch
                { }

                return container;
            }
        }
    }
}
