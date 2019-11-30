namespace Configurator
{
    using System.Collections.Generic;

    public class CfgManager
    {
        /// <summary>
        /// Get the config.ini of an Azure Function operation by Application name.
        /// Checking if the config is already locally available on the Azure Function. 
        /// If not, calling the Cfg API for the config which will be written locally.
        /// Generates required metadata from Azure Function Environment Variables.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="recycle"></param>
        /// <returns>Config.ini in List of strings.</returns>
        public static List<string> GetCfg(string app, bool recycle = false)
        {
            List<string> cfgLines = new List<string>();

            // check if azure function or local execution (testing)
            bool isAzure = CfgAz.CheckAzureFunc();

            if (isAzure)
            {
                // try and read local config first
                if (!recycle)
                {
                    cfgLines = CfgIO.ReadCfg(app, azure: true);

                    if (CfgValidation.ReadCfgResults(cfgLines))
                    {
                        return cfgLines;
                    }
                    else
                    {
                        cfgLines.Clear();
                    }
                }

                // get the azure function environment variables
                var azureVars = CfgAz.GetAzureVariables();

                if (CfgValidation.GetAzureVarsResults(azureVars))
                {
                    // create the cfg uri with azure function environment variables
                    string url = CfgApi.CreateCfgUri(app, azureVars);

                    if (CfgValidation.CreateCfgUriResults(url))
                    {
                        // call the cfg api
                        var cfg = CfgApi.CallCfgUrl(url);

                        if (CfgValidation.CallCfgUrlResults(cfg))
                        {
                            // write the cfg locally to azure function
                            var writeResult = CfgIO.WriteCfg(app, cfg);

                            if (CfgValidation.WriteCfgResults(writeResult))
                            {
                                // read the cfg back from azure function
                                var readCfgLines = CfgIO.ReadCfg(app, azure: true);

                                if (CfgValidation.ReadCfgResults(readCfgLines))
                                {
                                    cfgLines.AddRange(readCfgLines);
                                }
                                else
                                {
                                    cfgLines.Add(Errors.READCFG_AZURE_FAILURE);
                                }
                            }
                            else
                            {
                                cfgLines.Clear();
                                cfgLines.Add(writeResult);
                            }
                        }
                        else
                        {
                            cfgLines.Add(Errors.CFGAPI_FAILURE);
                        }
                    }
                    else
                    {
                        cfgLines.Add(Errors.CFGURL_NULLEMPTY);
                    }
                }
                else
                {
                    cfgLines.Add(Errors.ENVIRONMENT_VAR_EMPTY);
                }
            }
            else
            {
                //  try and read local cfg _IF_ "is local testing" 
                cfgLines = CfgIO.ReadCfg();
            }

            return cfgLines;
        }

        /// <summary>
        /// Check if GetCfg results contain errors.
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="errorMsg"></param>
        /// <returns>Error Bool and Error Massage.</returns>
        public static bool CheckCfg(List<string> cfg, out string errorMsg)
        {
            bool result = false;

            if (CfgValidation.GetCfgResults(cfg))
            {
                result = true;
                errorMsg = Constants.PASS;
            }
            else
            {
                errorMsg = string.Join(",", cfg);
            }

            return result;
        }
    }
}
