namespace PlyQor.Function.Processor.Functions
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using PlyQor.Engine;
    using PlyQor.Function.Processor.Core;

    public class MaintenanceFunc
    {
        [FunctionName("Maintenance")]
        public void Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            Initializer.Execute();

            PlyQorManager.Maintenance();
        }
    }
}
