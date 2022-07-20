namespace Configurator.Service
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using KirokuG2;

    public class CfgSvcManager
    {
        /// <summary>
        /// Initialize the Configurator API.
        /// </summary>
        public static bool Initialize(List<KeyValuePair<string, string>> kirokuConfig = null)
        {
            return Configuration.SetConfigs(kirokuConfig);
        }

        /// <summary>
        /// Get and serve configuration document response.
        /// </summary>
        public static OkObjectResult Execute(HttpRequest req, IKLog klog)
        {
            try
            {
                string cfkKey = req.Query["key"];

                string cfgApp = req.Query["app"];

                if (string.IsNullOrEmpty(cfkKey))
                {
                    klog.Info("NULL CFG KEY");
                    return new OkObjectResult(null);
                }

                if (string.IsNullOrEmpty(cfgApp))
                {
                    klog.Info("NULL APP KEY");
                    return new OkObjectResult(null);
                }

                klog.Info($"CfgKey: {cfkKey}, CfkApp: {cfgApp}");

                var document = GetCfg(cfkKey, cfgApp);

                if (document != null)
                {
                    klog.Info($"PASS");
                    return new OkObjectResult(document);
                }
                else
                {
                    klog.Error($"FAIL: NULL DOC");
                    return new OkObjectResult(null);
                }
            }
            catch (Exception ex)
            {
                klog.Error($"Exception: {ex.ToString()}");
                return new OkObjectResult(null);
            }

        }

        /// <summary>
        /// Get a single configure document from the storage blob.
        /// </summary>
        private static byte[] GetCfg(string cfgKey, string cfgApp)
        {
            byte[] document = null;

            var fileName = $"{cfgKey}_{cfgApp}.txt";

            try
            {
                document = StorageClient.GetDocument(fileName);

                return document;
            }
            catch
            {
                return document;
            }
        }
    }
}
