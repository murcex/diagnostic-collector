namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    class AddRecords
    {
        public static void Insert(List<SQLRecord> records)
        {
            try
            {
                foreach (var record in records)
                {
                    using (SqlConnection connection = new SqlConnection(Global.SQLConnectionString))
                    {
                        // SQLCommand & Command Type -- Add SQL Insert Stored Procedure
                        SqlCommand command = new SqlCommand("usp_Sensor_Stage_Insert", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("dt_session", record.Session);
                        command.Parameters.AddWithValue("nvc_source", record.Source);
                        command.Parameters.AddWithValue("nvc_dns", record.DNSName);
                        command.Parameters.AddWithValue("nvc_dnsstatus", record.DNSStatus);
                        command.Parameters.AddWithValue("nvc_ip", record.IP);
                        command.Parameters.AddWithValue("nvc_ipstatus", record.IPStatus);
                        command.Parameters.AddWithValue("nvc_datacenter", record.Datacenter);
                        command.Parameters.AddWithValue("nvc_datacentertag", record.DatacenterTag);
                        command.Parameters.AddWithValue("i_port", record.Port);
                        command.Parameters.AddWithValue("i_latency", record.Latency);

                        // Execute SQL
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQL Expection: {ex.ToString()}");
            }
        }
    }
}
