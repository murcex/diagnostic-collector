namespace Configurator.Storage.Core
{
    using System;
    using System.Collections.Generic;
    using Kiroku;
    using Newtonsoft.Json;

    static class Configuration
    {
        /// <summary>
        /// Get the storage account name and key from a Azure environment variable.
        /// </summary>
        //public static string StorageAccount
        //{
        //    get
        //    {
        //        string key = string.Empty;

        //        try
        //        {
        //            key = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT", EnvironmentVariableTarget.Process);
        //        }
        //        catch 
        //        { }

        //        return key;
        //    }
        //}

        /// <summary>
        /// Get the storage container name from a Azure environment variable.
        /// </summary>
        //public static string StorageContainer
        //{
        //    get
        //    {
        //        string container = string.Empty;

        //        try
        //        {
        //            container = Environment.GetEnvironmentVariable("STORAGE_CONTAINER", EnvironmentVariableTarget.Process);
        //        }
        //        catch
        //        { }

        //        return container;
        //    }
        //}

        /// <summary>
        /// Get the Kiroku config from a Azure environment variable.
        /// </summary>
        //public static List<KeyValuePair<string, string>> KirokuConfig
        //{
        //    get
        //    {
        //        List<KeyValuePair<string, string>> kvp = new List<KeyValuePair<string, string>>();
        //        string config = string.Empty;

        //        try
        //        {
        //            config = Environment.GetEnvironmentVariable("KIROKU_CFG", EnvironmentVariableTarget.Process);
        //        }
        //        catch
        //        { }

        //        if (!string.IsNullOrEmpty(config))
        //        {
        //            // split by , into array
        //            var records = config.Split(",");

        //            foreach(var record in records)
        //            {
        //                var recordArray = record.Split("=");

        //                var key = recordArray[0];

        //                var value = recordArray[1];

        //                kvp.Add(new KeyValuePair<string, string>(key, value));
        //            }
        //        }

        //        return kvp;
        //    }
        //}

        public static string GetPylonValue
        {
            get
            {
                string pylonValue = string.Empty;

                try
                {
                    pylonValue = Environment.GetEnvironmentVariable("PYLON_VALUE", EnvironmentVariableTarget.Process);
                }
                catch
                { }

                return pylonValue;
            }
        }

        public static Dictionary<string,Dictionary<string,string>> GenerateConfigDictionary(string pylonCfg)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(pylonCfg);
        }

        public static List<KeyValuePair<string,string>> ConvertToTag(Dictionary<string,string> dictionary)
        {
            List<KeyValuePair<string, string>> kvps = new List<KeyValuePair<string, string>>();

            foreach (var record in dictionary)
            {
                KeyValuePair<string, string> kvp = 
                    new KeyValuePair<string, string>(record.Key, record.Value);

                kvps.Add(kvp);
            }

            return kvps;
        }

        // Configs
        private static List<KeyValuePair<string, string>> _kirokuTagList;
        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }

        /// <summary>
        /// Set the config for the Kiroku instance.
        /// </summary>
        private static bool SetKirokuConfig()
        {
            // TODO: move to appliance
            KManager.Configure(KirokuConfig, dynamic: true);

            return true;
        }

        /// <summary>
        /// Set the Configurator application operating configs.
        /// </summary>
        public static bool SetConfigs(List<KeyValuePair<string, string>> kirokuConfig)
        {
            _kirokuTagList = kirokuConfig;

            if (RemoteStorage.Initialize(StorageAccount, StorageContainer))
            {
                return SetKirokuConfig();
            }

            return false;
        }
    }
}
