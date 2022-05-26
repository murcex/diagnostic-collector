namespace Configurator.Storage.Core
{
    using System;
    using System.Collections.Generic;
    using Kiroku;
    using Newtonsoft.Json;

    static class Configuration
    {
        // Configs
        private static string _storageAccount;

        private static string _storageContainer;

        public static string StorageAccount => _storageAccount;

        public static string StorageContainer => _storageContainer;

        public static bool Load(Dictionary<string, Dictionary<string, string>> configPackage)
        {
            if (configPackage.TryGetValue("Configurator.Storage", out Dictionary<string, string> config))
            {
                foreach (var kvp in config)
                {
                    switch (kvp.Key.ToUpper())
                    {
                        case "storage":
                            _storageAccount = kvp.Value;
                            break;
                        case "container":
                            _storageContainer = kvp.Value;
                            break;

                        default:
                            { }
                            break;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
