using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PlyQor.TempCleaner.Components;
using System;

namespace PlyQor.TempCleaner.Functions
{
    public class CleanerFunc
    {
        [FunctionName("Cleaner")]
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var ids = GetId.Execute();

            foreach (var id in ids)
            {
                DeleteId.Execute(id, true);

                DeleteId.Execute(id, false);

                Console.WriteLine(id);
            }
        }
    }
}
