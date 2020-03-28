namespace SensorApp
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Sensor;

    public static class SensorFunc
    {
        [FunctionName("Sensor")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation($"Config Status: {Configuration.ConfigStatus("SensorApp")}");

            try
            {
                var result = SensorManager.Execute();

                log.LogInformation($"Sensor Result: {result}");
            }
            catch (Exception ex)
            {
                log.LogInformation($"Sensor Exception: {ex}");
            }
        }
    }
}
