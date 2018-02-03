using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace Sensor
{
    public class DNSCollector
    {
        public static void Execute(Article hostname, DNSSensor sensor)
        {
            //TODO: vNext; Add Loop with Counter
            // Collect IP's from hostname
            IPAddress[] ips = Dns.GetHostAddresses(hostname.DNSName);

            // Check number of IP's
            if (ips.Length < 2)
            {
                foreach (var ip in ips)
                {
                    sensor.nvc_ip = ip.ToString();
                }

                Console.WriteLine("-- DataCenter Mapping --");

                // Check if configuration data exsits and deserialize
                if (hostname.DNSConfiguration.Contains("IpAddress"))
                {
                    var jsonObject = JsonConvert.DeserializeObject<List<Endpoint>>(hostname.DNSConfiguration);

                    foreach (Endpoint checkIpAddress in jsonObject)
                    {
                        Console.WriteLine("DataCenter Check: {0}", checkIpAddress.DataCenter);
                    }

                    // Match IPAddress with Data Center
                    var matchDatacenter = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenter).FirstOrDefault();
                    var matchDatacenterTag = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenterTag).FirstOrDefault();

                    // Set sensor with Data Center
                    sensor.nvc_datacenter = matchDatacenter;
                    sensor.nvc_datacentertag = matchDatacenterTag;

                    Console.WriteLine("Datacenter Match: {0}", matchDatacenter);
                    Console.WriteLine("Datacenter Tag Match: {0} \r\n", matchDatacenterTag);
                }

                else
                {
                    sensor.nvc_datacenter = "UNKNOWN";
                    sensor.nvc_datacentertag = "UNK";

                    Console.WriteLine("DataCenter Match: NONE \r\n");
                }
            }

            //TODO: Add ips.Count == 0 {status=fail}
            //TODO: Add status = true if passing
            //TODO: Add expection for DNS failure

            else
            {
                sensor.nvc_ip = "0.0.0.0";
            }

            sensor.nvc_status = "ONLINE";
        }
    }
}
