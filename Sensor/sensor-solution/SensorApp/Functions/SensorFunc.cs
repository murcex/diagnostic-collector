namespace SensorApp
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Sensor;
    using Sensor.Function.Core;
    using KirokuG2;

    public static class SensorFunc
    {
        [FunctionName("Sensor-Scanner")]
        public static void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                Setup.Execute();

                using (var klog = KManager.NewInstance("Sensor-Scanner"))
                {
                    try
                    {
                        var result = SensorManager.Execute(klog);
                    }
                    catch (Exception ex)
                    {
                        klog.Error(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                KManager.Critical(ex.ToString());
            }
        }
    }
}
