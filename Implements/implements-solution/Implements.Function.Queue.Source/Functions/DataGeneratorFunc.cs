using Implements.Function.Queue.Source.Components;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
				}
			}

			foreach (var sample in samples)
			{
				// http client request
				if (HttpRequest.SendRequest(sample))
				{
					// sql update
					SQLStorage.UpdateRecord(sample);
				}
			}
		}
	}
}
