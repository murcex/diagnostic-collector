namespace KQuery
{
    using System.Collections.Generic;

    class Configuration
    {
        /// Fields

        private static string _azureStorage;
        private static string _azureContainer;

        /// Properties

        public static string AzureStorage => _azureStorage;
        public static string AzureContainer => _azureContainer;

        /// <summary>
        /// Parse and set KQuery configuration package.
        /// </summary>
        public static bool SetKQueryConfig(List<KeyValuePair<string, string>> kqueryConfig)
        {
            if (kqueryConfig == null 
                || kqueryConfig.Count < 1)
            {
                return false;
            }

            foreach (var kvp in kqueryConfig)
            {
                switch (kvp.Key.ToString())
                {
                    case "storage":
                        _azureStorage = kvp.Value;
                        break;
                    case "container":
                        _azureContainer = kvp.Value;
                        break;

                    default:
                        { }
                        break;
                }
            }

            return true;
        }
    }
}
