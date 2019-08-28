namespace Sensor
{
    using System;
    using System.Data.SqlClient;

    public class DataRetention
    {
        /// <summary>
        /// Sensor data retention on Azure SQL Server staging table.
        /// </summary>
        public static void Execute()
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(Global.SQLConnectionString))
                {
                    var cmd = new SqlCommand("usp_Sensor_DNS_Stage_Retention", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();
                    var reader = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Expection: {0}", ex.ToString());
            }
        }
    }
}
