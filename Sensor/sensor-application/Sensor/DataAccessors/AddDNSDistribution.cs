using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sensor
{
	class AddDNSDistribution
    {
        public static void Execute(List<DNSDistributionRecord> transferCase)
        {
            var session = Global.SessionDatetime;
            var source = Global.SensorLocation;

            try
            {
                foreach (var target in transferCase)
                {
                    // Console Verbose
                    Console.WriteLine("-- SQL Upsert Check --");
                    Console.WriteLine("Session: {0}", session);
                    Console.WriteLine("Source: {0}", source);
                    Console.WriteLine("DNS: {0}", target.HostName);
                    Console.WriteLine("IPAddress: {0}", target.IP);
                    Console.WriteLine("Count: {0}\n", target.Count);

                    using (SqlConnection connection = new SqlConnection(Global.SQLConnectionString))
                    {
                        // SQLCommand & Command Type -- Add SQL Insert Stored Procedure
                        SqlCommand command = new SqlCommand("usp_Sensor_DNSDistribution_Insert", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                        // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                        command.Parameters.AddWithValue("dt_session", session);
                        command.Parameters.AddWithValue("nvc_source", source);
                        command.Parameters.AddWithValue("nvc_dns", target.HostName);
                        command.Parameters.AddWithValue("nvc_ip", target.IP.ToString());
                        command.Parameters.AddWithValue("i_count", target.Count);

                        // Execute SQL
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddDNSDistribution - SQL Expection: {ex.ToString()}");
            }  
        }
    }
}
