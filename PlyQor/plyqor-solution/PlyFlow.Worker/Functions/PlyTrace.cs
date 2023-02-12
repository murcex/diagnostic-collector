using System;
using Javelin.Worker;
using KirokuG2;
using KirokuG2.Internal;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using PlyQor.Client;

namespace PlyQor.Injektr.Functions
{
    public class PlyTrace
    {
        [FunctionName("PlyQor-Injektr")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                Initializer.Execute();

                using (var klog = KManager.NewInstance(executionContext.FunctionName))
                {
                    try
                    {
                        var id = Guid.NewGuid().ToString();
                        var data_1 = Guid.NewGuid().ToString();
                        var data_2 = Guid.NewGuid().ToString();

                        using (var insert_block = klog.NewBlock("PlyQorInjektrInsert"))
                        {
                            Configuration.PlyClient.Insert(id, data_1, "default");
                        }

                        using (var update_block = klog.NewBlock("PlyQorInjektrUpdate"))
                        {
                            Configuration.PlyClient.UpdateData(id, data_2);
                        }

                        using (var select_block = klog.NewBlock("PlyQorInjektrSelect"))
                        {
                            Configuration.PlyClient.Select(id);
                        }

                        using (var delete_block = klog.NewBlock("PlyQorInjektrDelete"))
                        {
                            Configuration.PlyClient.Delete(id);
                        }
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
