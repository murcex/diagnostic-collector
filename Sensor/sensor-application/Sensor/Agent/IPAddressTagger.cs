using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace Sensor
{
    public class IPAddressTagger
    {
        private static string unknownDataCenter = "UNKNOWN";

        private static string unknownDataCenterTag = "UNK";

        public static DNSSensor Execute(Article hostname, IPAddress ip)
        {
            DNSSensor sensor = new DNSSensor(hostname.DNSName);
            sensor.nvc_ip = ip.ToString();

            Console.WriteLine("-- DataCenter Mapping --");

            // Check if configuration data exsits and deserialize
            if (hostname.DNSConfiguration.Contains("IpAddress")) // TODO: bette way to check if null? && "IpAddress"?
            {
                var jsonObject = JsonConvert.DeserializeObject<List<Endpoint>>(hostname.DNSConfiguration);

                // Write DataCenters to Console
                foreach (Endpoint checkIpAddress in jsonObject)
                {
                    Console.WriteLine("DataCenter Check: {0}", checkIpAddress.DataCenter);
                }

                // Match IPAddress with Data Center
                sensor.nvc_datacenter = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenter).FirstOrDefault();
                sensor.nvc_datacentertag = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenterTag).FirstOrDefault();

                // Set sensor with Data Center
                //sensor.nvc_datacenter = matchDatacenter;
                //sensor.nvc_datacentertag = matchDatacenterTag;
                    sensor.nvc_status = "ONLINE";

                Console.WriteLine("Datacenter Match: {0}", sensor.nvc_datacenter);
                Console.WriteLine("Datacenter Tag Match: {0} \r\n", sensor.nvc_datacentertag);
            }
            else
            {
                sensor.nvc_datacenter = "UNKNOWN";
                sensor.nvc_datacentertag = "UNK";
                sensor.nvc_status = "NOMATCH";

                Console.WriteLine("DataCenter Match: NONE \r\n"); 
            }

            if (sensor.nvc_datacenter == null)
            {
                sensor.nvc_datacenter = unknownDataCenter;
                sensor.nvc_datacentertag = unknownDataCenterTag;

                Console.WriteLine("IpAddress exist, but there is no DataCenter match");
            }

            return sensor;
        }
    }
}
