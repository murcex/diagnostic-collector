using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sensor
{
	class DataUpload
	{
        //public static void UpsertSensor(List<DNSResolutionRecord> transferCase)
        //{
        //    try
        //    {
        //        foreach (var target in transferCase)
        //        {
        //            // Console Verbose
        //            Console.WriteLine("-- SQL Upsert Check --");
        //            Console.WriteLine("Session: {0}", target.Session);
        //            Console.WriteLine("Source: {0}", target.Source);
        //            Console.WriteLine("DataCenter: {0}", target.Datacenter);
        //            Console.WriteLine("DataCenter Tag: {0}", target.DatacenterTag);
        //            Console.WriteLine("DNS: {0}", target.DNS);
        //            Console.WriteLine("IPAddress: {0}", target.IP);
        //            Console.WriteLine("Status: {0}\n", target.Status);

        //            using (SqlConnection connection = new SqlConnection(Global.SQLConnectionString))
        //            {
        //                // SQLCommand & Command Type -- Add SQL Insert Stored Procedure
        //                SqlCommand command = new SqlCommand("usp_Sensor_DNS_Stage_Insert", connection);
        //                command.CommandType = System.Data.CommandType.StoredProcedure;

        //                // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
        //                // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
        //                command.Parameters.AddWithValue("dt_session", target.Session);
        //                command.Parameters.AddWithValue("nvc_source", target.Source);
        //                command.Parameters.AddWithValue("nvc_datacenter", target.Datacenter);
        //                command.Parameters.AddWithValue("nvc_datacentertag", target.DatacenterTag);
        //                command.Parameters.AddWithValue("nvc_dns", target.DNS);
        //                command.Parameters.AddWithValue("nvc_ip", target.IP);
        //                command.Parameters.AddWithValue("nvc_status", target.Status);

        //                // Execute SQL
        //                connection.Open();
        //                SqlDataReader reader = command.ExecuteReader();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"SQL Expection: {ex.ToString()}");
        //    }  
        //}
    }
}
