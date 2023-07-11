using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PlyQor.Engine;

namespace PlyQor.Functions
{
    public class RetentionFunc
    {
        [FunctionName("Retention")]
        public void Run([TimerTrigger("0 0 1 * * *")] TimerInfo myTimer, ILogger log)
        {
            PlyQorManager.Retention();
        }
    }
}
