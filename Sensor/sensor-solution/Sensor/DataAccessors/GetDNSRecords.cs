namespace Sensor
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;

    class GetDNSRecords
    {
        /// <summary>
        /// Get all Sensor endpoint records.
        /// </summary>
        /// <returns></returns>
        public static List<DNSRecord> GetArticle()
        {
            var targetList = new List<DNSRecord>();

            try
            {
                using (var connection = new SqlConnection(Configuration.SQLConnectionString))
                {
                    var cmd = new SqlCommand("usp_Sensor_DNS_Catalog_Select", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var item = new DNSRecord();
                        item.DNSName = (string)reader["nvc_dns"];
                        item.DNSConfiguration = (string)reader["nvc_configuration"];
                        targetList.Add(item);
                    }

                    return targetList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Download Article - SQL Expection: {ex}");
            }
        }
    }
}
