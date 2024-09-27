using Implements.Function.Queue.Target.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Implements.Function.Queue.Target.Functions
{
	public static class UpdateQueueFunc
	{
		[FunctionName("UpdateQueue")]
		public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
			ILogger log)
		{
			//log.LogInformation("C# HTTP trigger function processed a request.");

			string token = req.Query["token"];
			string id = req.Query["id"];
			string responseMessage = "400";

			// check token
			if (string.Equals(Configuration.Token, token))
			{
				// add to queue
				if (Configuration.Queue.Enqueue(id))
				{
					//Console.WriteLine($"Adding {id} to queue");
					responseMessage = "200";
				}
				else
				{
					responseMessage = "424";
				}
			}

			return new OkObjectResult(responseMessage);
		}
	}
}