namespace Sensor
{
    using System;
    using System.Data.SqlClient;

    public class DataRetention
    {
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
                    while (reader.Read())
                    {
                        //var item = new Article();
                        //item.DNSName = (string)reader["nvc_dns"];
                        //item.DNSConfiguration = (string)reader["nvc_configuration"];
                        //targetList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL Expection: {0}", ex.ToString());
            }
        }
    }
}
