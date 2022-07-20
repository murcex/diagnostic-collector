namespace SensorApp
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using KCopy;

    public static class KCopyFunc
    {
        [FunctionName("KCopy")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation($"Config Status: {Configuration.ConfigStatus("KCopyApp")}");

            try
            {
                var result = KCopyManager.Execute();

                log.LogInformation($"KCopy Result: {result}");
            }
            catch (Exception ex)
            {
                log.LogInformation($"KCopy Exception: {ex.ToString()}");
            }
        }
    }
}
