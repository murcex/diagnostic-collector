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

			#region console.connectionstring

            Console.WriteLine("-- Connection String Check --");
            Console.WriteLine("Connection String: {0} \r\n", Global.SQLConnectionString);

            #endregion

            // Get Target List
            var targetList = TargetAcquisition.GetTargetList();

            #region console.importcheck

            Console.WriteLine("-- Import Check --");
            foreach (var targetCheck in targetList)
			{
				Console.WriteLine("DNS Name: {0}", targetCheck.DNSName);
				Console.WriteLine("DNS Probe: {0} \r\n", targetCheck.DNSProbe);
			}

			#endregion

			// Loop target list, collect data and load sensor object
			foreach (var target in targetList)
			{
                // Create object and set timer
                // TODO: Add target.DNSName into object creation
                // TODO: Move session and source into object constructor
                // TODO: Move timer into HTTP Request method
                Sensor sensor = new Sensor();
                System.Diagnostics.Stopwatch timer = new Stopwatch();

                // Pre-collect set
                sensor.dt_session = Global.SessionDatetime;
                sensor.nvc_source = Global.SensorLocation;
                sensor.nvc_dns = target.DNSName;

                // Collect IP
                Collection.CollectIP(target, sensor);

                // Collect HTTP Request
                Collection.CollectHTTPRequest(target, sensor, timer);

                // Add to collection
                sensorCollection.Add(sensor);
            }

            // Upsert collection to SQL
            TargetDispatch.SQLUpsert(sensorCollection);

            #region console.debug

            if (Global.DebugMode == "1")
            {
                Console.WriteLine("-- Operation Complete --");
                Console.Read();
            }

            #endregion
            
            // Close application
            Environment.Exit(0);
		}
    }
}
