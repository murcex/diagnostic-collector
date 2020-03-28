using System;
using System.Collections.Generic;
using System.Text;
using Kiroku;

namespace Configurator.Service
{
    static class Configuration
    {
        /// <summary>
        /// Set the Configurator application operating configs.
        /// </summary>
        public static bool SetConfigs(List<KeyValuePair<string, string>> kirokuConfig)
        {
            _kirokuTagList = kirokuConfig;

            if (StorageClient.Initialize(StorageAccount, StorageContainer))
            {
                return SetKirokuConfig();
            }

            return false;
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

        /// <summary>
        /// Get the Kiroku config from a Azure environment variable.
        /// </summary>
        public static List<KeyValuePair<string, string>> KirokuConfig
        {
            get
            {
                List<KeyValuePair<string, string>> kvp = new List<KeyValuePair<string, string>>();
                string config = string.Empty;

                try
                {
                    config = Environment.GetEnvironmentVariable("KIROKU_CFG", EnvironmentVariableTarget.Process);
                }
                catch
                { }

                if (!string.IsNullOrEmpty(config))
                {
                    // split by , into array
                    var records = config.Split(",");

                    foreach(var record in records)
                    {
                        var recordArray = record.Split("=");

                        var key = recordArray[0];

                        var value = recordArray[1];

                        kvp.Add(new KeyValuePair<string, string>(key, value));
                    }
                }

                return kvp;
            }
        }

        /// <summary>
        /// Set the config for the Kiroku instance.
        /// </summary>
        private static bool SetKirokuConfig()
        {
            KManager.Configure(KirokuConfig, dynamic: true);

            return true;
        }

        // Configs
        private static List<KeyValuePair<string, string>> _kirokuTagList;
        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }
    }
}
