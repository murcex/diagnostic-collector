using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using PlyQor.Engine;
using System.IO;
using System.Threading.Tasks;

namespace PlyQor.Functions
{
	public static class StorageFunc
	{
		[FunctionName("Storage")]
		public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
		{
			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

			var result = PlyQorManager.Query(requestBody);

			return new OkObjectResult(result);
		}
	}
}
