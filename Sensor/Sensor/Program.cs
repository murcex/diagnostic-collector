using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Sensor
{
	class Program
	{
        static void Main(string[] args)
		{
			// Master Sensor collection
			List<Sensor> sensorCollection = new List<Sensor>();

			#region debug-master.switch

			//if (Global.DebugMode == 1)
			//{
                Console.WriteLine("-- Connection String Check --");
                Console.WriteLine("Connection String: {0} \r\n", Global.SQLConnectionString);
			//}

            #endregion

            // ADD COLLECTION ALL targetList FROM SQL
            var targetList = TargetAcquisition.GetTargetList();

			#region debug-hostname.import.check
			//if (Global.DebugMode == 1)
			//{
                Console.WriteLine("-- Import Check --");

                foreach (var testhost in targetList)
				{
					Console.WriteLine("DNS Name: {0}", testhost.DNSName);
					Console.WriteLine("DNS Probe: {0} \r\n", testhost.DNSProbe);
				}
			//}

			#endregion

			// ADD FOREACH HOSTNAME
			foreach (var target in targetList)
			{
				Sensor sensor = new Sensor();

				#region IPCollect

				//TODO: Create IPCollect method
				CollectIP(target, sensor);

				#endregion

				#region HTTPCollect

				//TODO: Create HTTPCollect method
				string address;
				HttpWebResponse response;
				TimeSpan timeTaken;

				CollectHTTPRequest(target, sensor, out address, out response, out timeTaken);

				#endregion

				#region debug-collect.http.check

				//if (Global.DebugMode == 1)
				//{
                    Console.WriteLine("-- Collection Check --");
                    Console.WriteLine("HTTP Web Response Address: {0}", address);
					Console.WriteLine("HTTP Web Response Status: {0}", response.StatusDescription);
					Console.WriteLine("HTTP Web Response Latency: {0} \r\n", timeTaken);
				//}

				#endregion

				#region CompilesensorObject

				// set object with collected data
				LoadSensor(Global.SessionDatetime, Global.SensorLocation, sensorCollection, target, sensor, response);

				#endregion
			}

			#region SQLInsert

            TargetDispatch.SQLUpsert(sensorCollection);

			#endregion

			#region debug-console.read

			if (Global.DebugMode == "1")
			{
                Console.WriteLine("-- Operation Complete --");
                Console.Read();
			}
			
			#endregion

			Environment.Exit(0);
		}

        // Auto generated methods
        //TODO: Move to classes, clean-up
		private static void CollectHTTPRequest(TargetAcquisition.Target hostname, Sensor sensor, out string address, out HttpWebResponse response, out TimeSpan timeTaken)
		{
			string uriHeader = "https://";
			address = uriHeader + hostname.DNSName + hostname.DNSProbe;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);

			System.Diagnostics.Stopwatch timer = new Stopwatch();

			timer.Start();

			response = (HttpWebResponse)request.GetResponse();

			timer.Stop();

			timeTaken = timer.Elapsed;
			if (timeTaken.TotalMilliseconds > 1000)
			{
				sensor.i_latency = 1000;
			}

			else
			{
				sensor.i_latency = timeTaken.TotalMilliseconds;
			}
		}

		private static void CollectIP(TargetAcquisition.Target hostname, Sensor sensor)
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

		private static void LoadSensor(DateTime sensorSession, string source, List<Sensor> sensorCollection, TargetAcquisition.Target hostname, Sensor sensor, HttpWebResponse response)
		{
			sensor.dt_session = sensorSession;
			sensor.nvc_source = source;
			sensor.nvc_dns = hostname.DNSName;
			sensor.nvc_status = response.StatusDescription;

			sensorCollection.Add(sensor);
		}
	}
}
