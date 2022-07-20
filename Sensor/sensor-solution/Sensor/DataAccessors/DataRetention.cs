namespace Sensor
{
    using System;
    using Microsoft.Data.SqlClient;

    class DataRetention
    {
        /// <summary>
        /// Sensor data retention on Azure SQL Server staging table.
        /// </summary>
        public static void Execute()
        {
            try
            {
                using (var connection = new SqlConnection(Configuration.SQLConnectionString))
                {
                    var cmd = new SqlCommand("usp_Sensor_Stage_Retention", connection);
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
