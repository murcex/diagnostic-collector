using Configurator.Audit.Embedded.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;

namespace Configurator.Audit.Embedded.Functions
{
    public static class AuditFunc
    {
        [FunctionName("Audit")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = $"Test Result: {string.Equals(Global.TestValue, "TestValue", StringComparison.OrdinalIgnoreCase)}";

            return new OkObjectResult(responseMessage);
        }
    }
}
