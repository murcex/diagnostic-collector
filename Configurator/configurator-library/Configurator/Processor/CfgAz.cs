namespace Configurator
{
    using System;
    using System.Collections.Generic;

    class CfgAz
    {
        /// <summary>
        /// Check is operation is running on a Azure Function.
        /// </summary>
        public static bool CheckAzureFunc()
        {
            bool isAzure = false;

            var funcCheck = Environment.GetEnvironmentVariable(Constants.FUNCTIONS_EXTENSION_VERSION, EnvironmentVariableTarget.Process);

            if (funcCheck != null)
            {
                isAzure = true;
            }

            return isAzure;
        }

        /// <summary>
        /// Collect the target Cfg Uri and Cfg Key from the Azure Function, used to call the Cfg API.
        /// </summary>
        public static Dictionary<string, string> GetAzureVariables()
        {
            Dictionary<string, string> azVars = new Dictionary<string, string>();

            try
            {
                var cfgKey = Environment.GetEnvironmentVariable(Constants.CONFIGURATION_KEY, EnvironmentVariableTarget.Process);

                azVars.Add(Constants.CfgKey, cfgKey);
            }
            catch (Exception)
            {
                azVars.Add(Constants.CfgUri, Errors.AZVAR_CFGKEY_EX);
            }

            try
            {
                var cfgUri = Environment.GetEnvironmentVariable(Constants.CONFIGURATION_URI, EnvironmentVariableTarget.Process);

                azVars.Add(Constants.CfgUri, cfgUri);
            }
            catch (Exception)
            {
                azVars.Add(Constants.CfgUri, Errors.AZVAR_CFGURI_EX);
            }

            return azVars;
        }
    }
}