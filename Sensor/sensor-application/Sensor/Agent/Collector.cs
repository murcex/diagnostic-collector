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
    public class Collector
    {
        public static List<DNSSensor> Execute(Article hostname)
        {
            //TODO: vNext; Add Loop with Counter
            // Collect IP's from hostname

            List<DNSSensor> sensorDnsSubCollection = new List<DNSSensor>();

            //IPAddress[] ips;

            try
            {
                //ips = Dns.GetHostAddresses(hostname.DNSName);

                var ips2 = DNSEvaluation.Execute(hostname.DNSName);

                foreach (var ip in ips2)
                {
                    Console.WriteLine("Distict: {0}", ip.ToString());
                }

                foreach (var ip in ips2)
                {
                    var sensor = IPAddressTagger.Execute(hostname, ip);
                    sensorDnsSubCollection.Add(sensor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.ToString());
                // TODO: Test resolve error, match off it
                // TODO: Test json deser, match off it
                DNSSensor sensor = new DNSSensor(hostname.DNSName);

                sensor.nvc_ip = "0.0.0.0";
                sensor.nvc_datacenter = "ERROR";
                sensor.nvc_datacentertag = "ERR";
                sensor.nvc_status = "EXCEPTION";

                sensorDnsSubCollection.Add(sensor);
            }

            return sensorDnsSubCollection;
        }   
    }
}
