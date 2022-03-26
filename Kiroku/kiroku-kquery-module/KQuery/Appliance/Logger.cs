namespace KQuery.Appliance
{
    using System.Collections.Generic;
    using Kiroku;

    class Logger
    {
        /// <summary>
        /// Configure Kiroku logger for KQuery.
        /// </summary>
        public static bool Configure(List<KeyValuePair<string, string>> kirokuConfig)
        {
            if (kirokuConfig == null 
                || kirokuConfig.Count < 1)
            {
                return false;
            }

            KManager.Configure(kirokuConfig, dynamic: true);

            return true;
        }
    }
}
