using Javelin.Worker;
using KirokuG2;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PlyQor.Injektr.Executors;
using System;

namespace PlyQor.Injektr.Functions
{
    public class PlyQorInjectorFunc
    {
        [FunctionName("PlyQor-Injektr")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                Initializer.Execute();

                using (var klog = KManager.NewInstance($"{executionContext.FunctionName}-{Configuration.Type}"))
                {
                    try
                    {
                        var executor = ExecutionSelector.SelectExecutor(Configuration.Type);

                        executor.Execute(klog);
                    }
                    catch (Exception ex)
                    {
                        klog.Error(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                KManager.Critical($"{executionContext.FunctionName} !!! Setup Failure !!! {ex}");
            }
        }
    }
}
