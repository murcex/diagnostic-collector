namespace KirokuG2.Processor.Functions
{
    using KirokuG2.Loader;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using System;

    public class ProcessorFunc
    {
        [FunctionName("Kiroku-Processor")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log, ExecutionContext executionContext)
        {
            using (var klog = KManager.NewInstance(executionContext.FunctionName))
            {
                try
                {
                    KLoaderManager.ProcessLogs(klog);
                }
                catch (Exception ex)
                {
                    klog.Error(ex.ToString());
                }
            }
        }
    }
}
