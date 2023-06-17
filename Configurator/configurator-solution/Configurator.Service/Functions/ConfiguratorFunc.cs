namespace ConfiguratorApp.Functions
{
    using Configurator.Storage;
    using KirokuG2;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using System;

    public static class ConfiguratorFunc
    {
        [FunctionName("Configurator")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                using (var klog = KManager.NewInstance("Configurator-Storage"))
                {
                    try
                    {
                        var document = CfgSvcManager.Execute(req, klog);

                        return document;
                    }
                    catch (Exception ex)
                    {
                        klog.Error(ex.ToString());

                        return new OkObjectResult(null);
                    }
                }
            }
            catch (Exception ex)
            {
                KManager.Critical(ex.ToString());

                return new OkObjectResult(null);
            }
        }
    }
}
