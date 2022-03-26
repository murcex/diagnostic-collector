namespace KCopy.Appliance
{
    using System.Collections.Generic;
    using Kiroku;

    public class Logger
    {
        /// <summary>
        /// Configure Kiroku logger.
        /// </summary>
        public static bool Configure(List<KeyValuePair<string, string>> kirokuConfig)
        {
            KManager.Configure(kirokuConfig);

            return true;
        }

        /// <summary>
        /// Start Kiroku logging.
        /// </summary>
        public static void StartLogging()
        {
            KManager.Open();
        }

        /// <summary>
        /// Stop Kiroku logging.
        /// </summary>
        public static void StopLogging()
        {
            KManager.Close();
        }
    }
}
