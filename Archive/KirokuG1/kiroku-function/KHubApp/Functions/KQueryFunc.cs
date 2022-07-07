namespace KHubApp
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using KQuery;

    public static class KQueryFunc
    {
        [FunctionName("KQuery")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation($"Config Status: {Configuration.ConfigStatus("KQueryApp")}");

            try
            {
                return KQueryManger.Execute(req);
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.ToString());

                return new OkObjectResult("");
            }
        }
    }
}
