using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sensor
{
    //TODO: Clean-up - remove legacy connectionString commented ref
    class TargetAcquisition
    {
		public class Target
		{
			public string DNSName { get; set; }
			public string DNSProbe { get; set; }
			public string DNSConfiguration { get; set; }
		}
		public static List<Target> GetTargetList() //(string connectionString)
		{
			var targetList = new List<Target>();

			var target = new Target();

            //using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
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
	}
}
