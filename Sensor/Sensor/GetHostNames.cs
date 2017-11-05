using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sensor
{
	class GetHostNames
	{

		public class HostName
		{
			public string DNSHostName { get; set; }
			public string DNSProbe { get; set; }
			public string DNSConfiguration { get; set; }
		}
		public static List<HostName> GetHostName(string connectionString)
		{
			var hostnameCollection = new List<HostName>();

			var hostname = new HostName();

			using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				var cmd = new SqlCommand("usp_Sensor_HostNames_Load", connection);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				connection.Open();

				var reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					var item = new HostName();
					item.DNSHostName = (string)reader["nvc_dns"];
					item.DNSProbe = (string)reader["nvc_probe"];
					item.DNSConfiguration = (string)reader["nvc_configuration"];
					hostnameCollection.Add(item);
				}

				return hostnameCollection;
			}
		}
	}
}
