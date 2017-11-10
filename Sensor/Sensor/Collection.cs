using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Sensor
{
    class Collection
    {
        public static void CollectIP(TargetAcquisition.Target hostname, Sensor sensor)
        {
            IPAddress[] ips = Dns.GetHostAddresses(hostname.DNSName);

            if (ips.Length < 2)
            {
                foreach (var ip in ips)
                {
                    sensor.nvc_ip = ip.ToString();
                }

                Console.WriteLine("-- DataCenter Mapping --");

                //if (hostname.DNSConfiguration != "NOTMAPPED")
                if (hostname.DNSConfiguration.Contains("IpAddress"))
                {
                    var jsonObject = JsonConvert.DeserializeObject<List<Endpoint>>(hostname.DNSConfiguration);

                    foreach (Endpoint checkIpAddress in jsonObject)
                    {
                        Console.WriteLine("DataCenter Check: {0}", checkIpAddress.DataCenter);
                    }

                    var matchDatacenter = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenter).FirstOrDefault();
                    var matchDatacenterTag = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenterTag).FirstOrDefault();

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

            else
            {
                sensor.nvc_ip = "0.0.0.0";
            }
        }

        public static void CollectHTTPRequest(TargetAcquisition.Target target, Sensor sensor, Stopwatch timer)
        {
            string uriHeader = "https://";
            string address = uriHeader + target.DNSName + target.DNSProbe;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);

            //System.Diagnostics.Stopwatch timer = new Stopwatch();

            Console.WriteLine("Pre-Start Timer: {0}",timer.Elapsed);

            timer.Start();

            Console.WriteLine("Started Timer: {0}",timer.Elapsed);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            timer.Stop();

            Console.WriteLine("Stopping Timer {0}",timer.Elapsed);

            TimeSpan timeTaken2 = timer.Elapsed;
            if (timeTaken2.TotalMilliseconds > 1000)
            {
                sensor.i_latency = 1000;
            }

            else
            {
                sensor.i_latency = timeTaken2.TotalMilliseconds;
            }

            Console.WriteLine("Time Normal: {0}",timeTaken2.Milliseconds);
            Console.WriteLine("Time Total: {0} \r\n",timeTaken2.TotalMilliseconds);

            sensor.nvc_status = response.StatusDescription;
        }
    }
}
