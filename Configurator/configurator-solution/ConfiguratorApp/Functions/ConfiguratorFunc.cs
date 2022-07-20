namespace ConfiguratorApp
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Configurator.Service;
    using KirokuG2;
    using ConfiguratorApp.Core;

    public static class ConfiguratorFunc
    {
        [FunctionName("Configurator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                Setup.Execute();

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
