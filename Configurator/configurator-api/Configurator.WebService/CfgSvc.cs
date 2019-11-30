namespace Configurator.WebService
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;

    public static class CfgSvc
    {
        public static string StorageAccount
        {
            get
            {
                string key = string.Empty;

                try
                {
                    key = Environment.GetEnvironmentVariable("STORAGE_ACCOUNT", EnvironmentVariableTarget.Process);
                }
                catch { }

                return key;
            }
        }

        public static string StorageContainer
        {
            get
            {
                string container = string.Empty;

                try
                {
                    container = Environment.GetEnvironmentVariable("STORAGE_CONTAINER", EnvironmentVariableTarget.Process);
                }
                catch { }

                return container;
            }
        }

        public static bool Set()
        {
            return StorageClient.Initialize(StorageAccount, StorageContainer);
        }

        [FunctionName("Cfg")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Storage Client Status: {Set()}");

            string cfkKey = req.Query["key"];

            string cfgApp = req.Query["app"];

            if (!string.IsNullOrEmpty(cfkKey) || !string.IsNullOrEmpty(cfgApp))
            {
                log.LogInformation($"CfgKey: {cfkKey}, CfkApp: {cfgApp}");
                var document = CfgIO.GetCfg(cfkKey, cfgApp);

                if (document != null)
                {
                    log.LogInformation($"PASS");
                    return new OkObjectResult(document);
                }
                else
                {
                    log.LogInformation($"FAIL: NULL DOC");
                    return new OkObjectResult(null);
                    //return new OkObjectResult(Encoding.ASCII.GetBytes("Level 2 Failure"));
                }
            }
            else
            {
                log.LogInformation($"FAIL: NULL KEY/APP");
                return new OkObjectResult(null);
                //return new OkObjectResult(Encoding.ASCII.GetBytes($"Level 1 Failure: {cfkKey} {cfgApp}"));
            }
        }
    }
}
