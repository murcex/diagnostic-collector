using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sensor
{
	class TargetDispatch
	{
		public static void SQLUpsert(List<Sensor> transferCase)
		{
            foreach (var target in transferCase)
            {
                //if (Global.DebugMode == 1)
                //{
                Console.WriteLine("-- SQL Upsert Check --");
                Console.WriteLine("Session: {0}", target.dt_session);
                Console.WriteLine("Source: {0}", target.nvc_source);
                Console.WriteLine("DataCenter: {0}", target.nvc_datacenter);
                Console.WriteLine("DataCenter Tag: {0}", target.nvc_datacentertag);
                Console.WriteLine("DNS: {0}", target.nvc_dns);
                Console.WriteLine("IPAddress: {0}", target.nvc_ip);
                Console.WriteLine("Status: {0}", target.nvc_status);
                Console.WriteLine("Latency: {0} \r\n", target.i_latency);
                //}

                using (SqlConnection connection = new SqlConnection(Global.SQLConnectionString))
                {
                    // SQLCommand & Command Type -- Add SQL Insert Stored Procedure
                    SqlCommand command = new SqlCommand("usp_Sensor_Staging_Upsert_v2", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                    // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                    command.Parameters.AddWithValue("dt_session", target.dt_session);
                    command.Parameters.AddWithValue("nvc_source", target.nvc_source);
                    command.Parameters.AddWithValue("nvc_datacenter", target.nvc_datacenter);
                    command.Parameters.AddWithValue("nvc_datacentertag", target.nvc_datacentertag);
                    command.Parameters.AddWithValue("nvc_dns", target.nvc_dns);
                    command.Parameters.AddWithValue("nvc_ip", target.nvc_ip);
                    command.Parameters.AddWithValue("nvc_status", target.nvc_status);
                    command.Parameters.AddWithValue("i_latency", target.i_latency);

                    // Execute SQL
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                }
            }
		}
	}
}
