using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PlyQor.TempCleaner.Components;
using PlyQor.TempCleaner.Core;
using System;
using System.ComponentModel;

namespace PlyQor.TempCleaner.Functions
{
	public class CleanerFunc
	{
		[FunctionName("PlyQor-TempRetention")]
		public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
		{
			//Console.WriteLine($"-- Start Retention --");

			foreach (var container in Global.Containers)
			{
				// counters
				var opPassCounter = 0;
				var opFailCounter = 0;
				var dataDeleteCounter = 0;
				var tagDeleteCounter = 0;

				var ids = GetId.Execute(container);

				foreach (var id in ids)
				{
					Console.WriteLine(id);

					var dataDeleteResult = DeleteId.Execute(container, id, true);

					var tagDeleteResult = DeleteId.Execute(container, id, false);

					if (dataDeleteResult.result && tagDeleteResult.result)
					{
						dataDeleteCounter = +dataDeleteResult.recordCount;

						tagDeleteCounter = +tagDeleteResult.recordCount;

						opPassCounter++;
					}
					else
					{
						if (!dataDeleteResult.result)
						{

						}

						if (!tagDeleteResult.result)
						{

						}

						opFailCounter++;
					}
				}

				//Console.WriteLine($"Container: {container}");
				//Console.WriteLine($"Records: {ids.Count}");
				//Console.WriteLine($"Operation Pass: {opPassCounter}");
				//Console.WriteLine($"Operation Fail: {opFailCounter}");
				//Console.WriteLine($"Data Delete: {dataDeleteCounter}");
				//Console.WriteLine($"Tag Delete: {tagDeleteCounter}");
				//Console.WriteLine($"");
			}

			//Console.WriteLine($"Retention Complete");
			//Console.WriteLine($"");
		}
	}
}
