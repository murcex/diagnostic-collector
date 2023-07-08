namespace Sensor
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;

    class AddRecords
    {
        /// <summary>
        /// Add sensor records to Azure SQL Server.
        /// </summary>
        /// <param name="records"></param>
        public static string Insert(List<SQLRecord> records)
        {
            string result = "No Result Set.";

            int recordCount = -1;

            int exceptionCounter = 0;
            string exceptionMessage = "No Message.";

            try
            {
                if (records != null)
                {
                    recordCount = records.Count;

                    foreach (var record in records)
                    {
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(Configuration.SQLConnectionString))
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
                        catch (Exception ex)
                        {
                            if (exceptionCounter > 0)
                            {
                                exceptionCounter++;
                            }
                            else
                            {
                                exceptionMessage = ex.ToString();

                                exceptionCounter++;
                            }
                        }
                    }
                }

                result = $"Result: {exceptionCounter == 0 && recordCount > -1}," +
                    $" Record Count: {recordCount}," +
                    $" Exception Count: {exceptionCounter}," +
                    $" Exception Message: {exceptionMessage}";

                return result;
            }
            catch (Exception ex)
            {
                result = $"AddRecords::Insert Exception: {ex}";

                return result;
            }
        }
    }
}
