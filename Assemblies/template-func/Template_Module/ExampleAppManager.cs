namespace ExampleModule
{
    using System;
    using System.Collections.Generic;
    using Kiroku;

    public class ExampleAppManager
    {
        /// <summary>
        /// Application initialization status.
        /// </summary>
        public static bool IsInitialized { get; private set; }

        // TODO: *** Initialize application with required config packages ***
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appCfg">Application config</param>
        /// <param name="kirokuCfg">Kiroku loging config</param>
        public static bool Initialize(List<KeyValuePair<string, string>> appCfg, List<KeyValuePair<string, string>> kirokuCfg)
        {
            // Push config packages to Function Configuration logic
            try
            {
                return IsInitialized = Configuration.SetConfigs(appCfg, kirokuCfg);
            }
            catch (Exception ex)
            {
                // TODO: add logging

                return IsInitialized = false;
            }
        }

        // TODO: *** Application entry point exposed to Function ***
        /// <summary>
        /// 
        /// </summary>
        public static bool Execute()
        {
            if (!IsInitialized)
            {
                return false;
            }

            try
            {
                using (KLog klog = new KLog("KLogging"))
                {
                    Configuration.StartLogging();

                    // TODO: *** entry point into core application logic ***

                    klog.Info($"Logging.");

                    Configuration.StopLogging();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"EXCEPTION: {ex.ToString()}");
            }
        }
    }
}