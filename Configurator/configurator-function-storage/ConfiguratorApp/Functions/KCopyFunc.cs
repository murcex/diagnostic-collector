namespace ConfiguratorApp
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using KCopy;
    using ConfiguratorApp.Core;

    public static class KCopyFunc
    {
        [FunctionName("KCopy")]
        public static void Execute([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            Initializer.Execute();

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
