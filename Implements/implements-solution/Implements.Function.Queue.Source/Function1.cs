using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Implements.Function.Queue.Source
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            List<string> samples = new();
            var capacity = 0;
            while (capacity < 21)
            {
                samples.Add(Guid.NewGuid().ToString());
            }

            // sql insert

            // http client request

            // sql update
        }
    }
}
