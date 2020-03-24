namespace SensorApp
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Sensor;

    public static class RetentionFunc
    {
        [FunctionName("Retention")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation($"Config Status: {Configuration.ConfigStatus("SensorApp")}");

            try
            {
                var result = SensorManager.Retention();

                log.LogInformation($"Sensor Retention Result: {result}");
            }
            catch (Exception ex)
            {
                log.LogInformation($"Sensor Retention Exception: {ex}");
            }
        }
    }
}
