using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sensor
{
    class DataDownload
    {
        public static List<Article> GetArticle()
        {
            var targetList = new List<Article>();

            try
            {
                //var target = new Target();

                using (var connection = new System.Data.SqlClient.SqlConnection(Global.SQLConnectionStringv2))
                {
                    var cmd = new SqlCommand("usp_Sensor_DNS_Catalog_Select", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var item = new Article();
                        item.DNSName = (string)reader["nvc_dns"];
                        item.DNSConfiguration = (string)reader["nvc_configuration"];
                        targetList.Add(item);
                    }

                    return targetList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Download Article - SQL Expection: {0}", ex.ToString());

                return targetList;
            }
        }

        public static void DataRetention()
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(Global.SQLConnectionStringv2))
                {
                    var cmd = new SqlCommand("usp_Sensor_DNS_Catalog_Select", connection); // TODO: Add Retention Proc here.
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
