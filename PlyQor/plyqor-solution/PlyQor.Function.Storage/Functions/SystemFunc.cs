using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlyQor.Engine;

namespace PlyQor.Function.Storage.Functions
{
    public static class SystemFunc
    {
        [FunctionName("System")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

			var result = PlyQorManager.System(requestBody);

			return new OkObjectResult(result);
		}
    }
}
