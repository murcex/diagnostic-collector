namespace KirokuG2.Processor.Functions
{
    using System;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using KirokuG2.Processor.Core;
    using KirokuG2.Loader;

    public class ProcessorFunc
    {
        [FunctionName("Kiroku-Processor")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log, ExecutionContext executionContext)
        {
            Setup.Execute();

            using (var klog = KManager.NewInstance(executionContext.FunctionName))
            {
                try
                {
                    KLoaderManager.ProcessLogs();
                }
                catch (Exception ex)
                {
                    klog.Error(ex.ToString());
                }
            }
        }
    }
}
