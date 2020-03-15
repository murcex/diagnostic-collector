namespace KQueryApp
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
                KLoadManager.Execute();
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.ToString());
            }
        }
    }
}
