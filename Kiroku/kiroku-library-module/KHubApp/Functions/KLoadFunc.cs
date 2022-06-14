namespace KHubApp
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using KLoad;

    public static class KLoadFunc
    {
        [FunctionName("KLoad")]
        public static void Execute([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation($"Config Status: {Configuration.ConfigStatus("KLoadApp")}");

            try
            {
                var result = KLoadManager.Execute();

                log.LogInformation($"Result: {result}");
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.ToString());
            }
        }
    }
}
