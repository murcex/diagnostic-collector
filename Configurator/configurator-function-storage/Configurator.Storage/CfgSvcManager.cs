namespace Configurator.Storage
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Kiroku;
    using Configurator.Storage.Storage;
    using Configurator.Storage.Core;

    public class CfgSvcManager
    {
        private static bool _status;

        /// <summary>
        /// Initialize the Configurator API.
        /// </summary>
        public static bool Initialize(Dictionary<string,Dictionary<string,string>> configuration)
        {
            return _status = Initializer.Execute(configuration);
        }

        /// <summary>
        /// Get and serve configuration document response.
        /// </summary>
        public static OkObjectResult Execute(HttpRequest req)
        {
            using (KLog klog = new KLog("Configurator-Execute"))
            {
                try
                {
                    string cfkKey = req.Query["key"];

                    string cfgApp = req.Query["app"];

                    if (string.IsNullOrEmpty(cfkKey))
                    {
                        klog.Warning("NULL CFG KEY");
                        return new OkObjectResult(null);
                    }

                    if (string.IsNullOrEmpty(cfgApp))
                    {
                        klog.Warning("NULL APP KEY");
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
                    klog.Error($"CfgSvcManager::Execute - EXCEPTION: {ex}");
                    return new OkObjectResult(null);
                }
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
                document = RemoteStorage.GetDocument(fileName);

                return document;
            }
            catch
            {
                return document;
            }
        }
    }
}
