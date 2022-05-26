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
    using ConfiguratorApp.Core;

    public static class ConfiguratorFunc
    {
        [FunctionName("Configurator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            Initializer.Execute();

            try
            {
                var document = CfgSvcManager.Execute(req);

                log.LogInformation($"PASS");

                return document;
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.ToString());

                return new OkObjectResult(null);
            }
        }
    }
}
