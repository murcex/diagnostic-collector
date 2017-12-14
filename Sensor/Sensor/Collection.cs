using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json;
// HTTP Request 2
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sensor
{
    class Collection
    {
        public static void CollectIP(TargetAcquisition.Target hostname, Sensor sensor)
        {
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

            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            timer.Stop();

            TimeSpan timeTaken2 = timer.Elapsed;
            if (timeTaken2.TotalMilliseconds > 1000)
            {
                sensor.i_latency = 1000;
            }

            else
            {
                sensor.i_latency = timeTaken2.TotalMilliseconds;
            }

            sensor.nvc_status = response.StatusDescription;
        }

        public static void CollectHTTPRequestTesting(TargetAcquisition.Target target)
        {
            HttpClient client1 = new HttpClient();
            HttpClient client2 = new HttpClient();

            string uriHeader1 = "https://";
            string address1 = uriHeader1 + target.DNSName + target.DNSProbe;

            string uriHeader2 = "http://";
            string address2 = uriHeader2 + target.DNSName + target.DNSProbe;

            HttpResponseMessage response1 = client1.GetAsync(address1).Result;
            HttpResponseMessage response2 = client2.GetAsync(address2).Result;

            Console.WriteLine("HttpResponseMessage 1: {0}", response1.IsSuccessStatusCode);
            Console.WriteLine("HttpResponseMessage 2: {0}", response2.IsSuccessStatusCode);

            Console.Read();
        }
    }
}
