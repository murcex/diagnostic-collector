namespace Configurator
{
    using System.Collections.Generic;

    class CfgValidation
    {
        /// <summary>
        /// Check if ReadCfg operation has failed.
        /// </summary>
        /// <param name="lines"></param>
        public static bool ReadCfgResults(List<string> lines)
        {
            bool result = true;

            if (lines.Count < 2 && (lines.Contains(Errors.READCFG_AZURE_EX) || lines.Contains(Errors.READCFG_LOCAL_EX)))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Check if GetAzureVars has failed.
        /// </summary>
        /// <param name="dict"></param>
        public static bool GetAzureVarsResults(Dictionary<string, string> dict)
        {
            bool result = true;

            if (dict.TryGetValue(Constants.CfgKey, out string cfgKey))
            {
                if (cfgKey.Contains(Errors.AZVAR_CFGKEY_EX))
                {
                    result = false;
                }
            }

            if (dict.TryGetValue(Constants.CfgUri, out string cfgUri))
            {
                if (cfgUri.Contains(Errors.AZVAR_CFGURI_EX))
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Check if CreateCfgUri has failed.
        /// </summary>
        /// <param name="url"></param>
        public static bool CreateCfgUriResults(string url)
        {
            bool result = true;

            if (url == Errors.EMPTY || url == Errors.NULLEMPTY)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Check if CallCfgUrl has failed.
        /// </summary>
        /// <param name="lines"></param>
        public static bool CallCfgUrlResults(List<string> lines)
        {
            bool result = true;

            if (lines.Count < 2 && lines.Contains(Errors.ERROR))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Check if WriteCfg has failed.
        /// </summary>
        /// <param name="output"></param>
        public static bool WriteCfgResults(string output)
        {
            bool result = true;

            if (output.Contains(Errors.WRITECFG_APP_NULL) || output.Contains(Errors.WRITECFG_EX))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Check if GetCfg has failed.
        /// </summary>
        /// <param name="lines"></param>
        public static bool GetCfgResults(List<string> lines)
        {
            return CallCfgUrlResults(lines);
        }
    }
}
