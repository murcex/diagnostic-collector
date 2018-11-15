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
            DataUpload.UpsertSensor(Capsule.DNSRecords);

            // TODO: Add sql table, proc and csharp logic
            foreach (var x in Capsule.DNSCounts)
            {
                //Console.WriteLine($"{x.HostName},{x.IP},{x.Count}");
            }
        }
    }
}
