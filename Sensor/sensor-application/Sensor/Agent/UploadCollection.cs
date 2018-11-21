using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public static class UploadCollection
    {
        public static void Execute()
        {
            DataUpload.UpsertSensor(Capsule.DNSResolutionRecords);

            // TODO: Add sql table, proc and csharp logic for both loops

            Console.WriteLine("DNS Count:");
            foreach (var x in Capsule.DNSDistributionRecords)
            {
                Console.WriteLine($"{x.HostName},{x.IP},{x.Count}");
            }

            AddDNSDistribution.Execute(Capsule.DNSDistributionRecords);

            Console.WriteLine("");
            Console.WriteLine("TCP Latency:");

            foreach (var x in Capsule.TCPLatencyRecords)
            {
                Console.WriteLine($"{x.HostName},{x.IP},{x.Latency}");
            }

            AddTCPLatency.Execute(Capsule.TCPLatencyRecords);
        }
    }
}
