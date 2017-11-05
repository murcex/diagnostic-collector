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

namespace Sensor
{
	class Program
	{
		static void Main(string[] args)
		{

			// Master debug switch
			int debugSwitch = 1;

			// Import App.Config values
			//TODO: Static Global class
			var source = ConfigurationManager.AppSettings["sensor"].ToString();
			var connectionString = ConfigurationManager.AppSettings["sql"].ToString();
			DateTime sensorSession = DateTime.Now.ToUniversalTime();

			// Master Sensor collection
			List<Sensor> sensorCollection = new List<Sensor>();

			#region debug-master.switch

			if (debugSwitch == 1)
			{
				Console.WriteLine(connectionString);
			}

			#endregion

			// ADD COLLECTION ALL HOSTNAMES FROM SQL
			//TODO: Clean-up GetHostNames class
			var hostNames = GetHostNames.GetHostName(connectionString);

			#region JSONTesting

			//

			#endregion

			#region debug-hostname.import.check
			if (debugSwitch == 1)
			{
				foreach (var testhost in hostNames)
				{
					Console.WriteLine(testhost.DNSHostName);
					Console.WriteLine(testhost.DNSProbe);
				}
			}

			#endregion

			// ADD FOREACH HOSTNAME
			foreach (var hostname in hostNames)
			{
				Sensor sensor = new Sensor();

				#region IPCollect
				//TODO: Create IPCollect method
				CollectIP(hostname, sensor);

				#endregion

				#region HTTPCollect

				//TODO: Create HTTPCollect method
				string address;
				HttpWebResponse response;
				TimeSpan timeTaken;

				CollectHTTPRequest(hostname, sensor, out address, out response, out timeTaken);

				#endregion

				#region debug-collect.http.check

				if (debugSwitch == 1)
				{
					Console.WriteLine("HTTP Web Response Address: {0}", address);
					Console.WriteLine("HTTP Web Response Status: {0}", response.StatusDescription);
					Console.WriteLine("HTTP Web Response Latency: {0}", timeTaken);
				}

				#endregion

				#region CompilesensorObject

				// set object with collected data
				LoadSensor(sensorSession, source, sensorCollection, hostname, sensor, response);

				#endregion
			}

			#region SQLInsert
			foreach (var upsert in sensorCollection)
			{
				#region debug3

				if (debugSwitch == 1)
				{
					Console.WriteLine(upsert.dt_session);
					Console.WriteLine(upsert.nvc_source);
					Console.WriteLine(upsert.nvc_dns);
					Console.WriteLine(upsert.nvc_ip);
					Console.WriteLine(upsert.nvc_status);
					Console.WriteLine(upsert.i_latency);
				}

				#endregion

				Injector.SQLUpsert(upsert, connectionString);
			}

			#endregion

			#region debug-console.read

			if (debugSwitch == 1)
			{
				Console.Read();
			}
			
			#endregion

			Environment.Exit(0);
		}

		private static void CollectHTTPRequest(GetHostNames.HostName hostname, Sensor sensor, out string address, out HttpWebResponse response, out TimeSpan timeTaken)
		{
			string uriHeader = "https://";
			address = uriHeader + hostname.DNSHostName + hostname.DNSProbe;
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

		private static void CollectIP(GetHostNames.HostName hostname, Sensor sensor)
		{
			IPAddress[] ips = Dns.GetHostAddresses(hostname.DNSHostName);

			if (ips.Length < 2)
			{
				foreach (var ip in ips)
				{
					sensor.nvc_ip = ip.ToString();
				}

				// find datacenter from json object with json.ip == sensor.nvc_ip
			}

			else
			{
				sensor.nvc_ip = "0.0.0.0";
			}
		}

		private static void LoadSensor(DateTime sensorSession, string source, List<Sensor> sensorCollection, GetHostNames.HostName hostname, Sensor sensor, HttpWebResponse response)
		{
			sensor.dt_session = sensorSession;
			sensor.nvc_source = source;
			sensor.nvc_dns = hostname.DNSHostName;
			sensor.nvc_status = response.StatusDescription;

			sensorCollection.Add(sensor);
		}
	}
}
