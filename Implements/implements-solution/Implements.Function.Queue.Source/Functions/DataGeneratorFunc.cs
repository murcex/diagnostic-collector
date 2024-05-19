using Implements.Function.Queue.Source.Components;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Implements.Function.Queue.Source.Functions
{
	public class DataGeneratorFunc
	{
		[FunctionName("DataGenerator")]
		public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
		{
			List<string> samples = new();
			var capacity = 0;
			while (capacity < 11)
			{
				var sample = Guid.NewGuid().ToString();

				// sql insert
				if (SQLStorage.InsertRecord(sample))
				{
					samples.Add(sample);
					Console.WriteLine($"{sample} added to SQL");
				}
				else
				{
					Console.WriteLine($"{sample} failed to insert to database");
				}

				Thread.Sleep(1000);

				capacity++;
			}

			foreach (var sample in samples)
			{
				// http client request
				if (HttpRequest.SendRequest(sample))
				{
					// sql update
					if (!SQLStorage.UpdateRecord(sample))
					{
						Console.WriteLine($"{sample} failed to update to database");
					}
					else
					{
						Console.WriteLine($"{sample} sent and updated in SQL");
					}
				}
				else
				{
					Console.WriteLine($"{sample} failed to be sent");
				}
			}

			if (!SQLStorage.Retention())
			{
				Console.WriteLine($"Retention failed to complete");
			}
		}
	}
}
