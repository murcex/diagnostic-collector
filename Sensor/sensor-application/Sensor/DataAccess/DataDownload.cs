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
		public static List<Target> GetTargetList()
		{
			var targetList = new List<Target>();

			var target = new Target();

            using (var connection = new System.Data.SqlClient.SqlConnection(Global.SQLConnectionString))
            {
                var cmd = new SqlCommand("usp_Sensor_HostNames_Load", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new Target();
                    item.DNSName = (string)reader["nvc_dns"];
                    item.DNSProbe = (string)reader["nvc_probe"];
                    item.DNSConfiguration = (string)reader["nvc_configuration"];
                    targetList.Add(item);
                }

                return targetList;
            }
		}

        // Sensor v2 - Get Articles
        public static List<Article> GetArticle()
        {
            var targetList = new List<Article>();

            var target = new Target();

            using (var connection = new System.Data.SqlClient.SqlConnection(Global.SQLConnectionStringv2))
            {
                var cmd = new SqlCommand("usp_DNS_Catalog_Select", connection);
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
    }
}
