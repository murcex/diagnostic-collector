using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Sensor
{
	class Injector
	{
		public static void SQLUpsert(Sensor transferCase, string connectionString)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// SQLCommand & Command Type -- Add SQL Insert Stored Procedure
				SqlCommand command = new SqlCommand("usp_Sensor_Staging_Upsert", connection);
				command.CommandType = System.Data.CommandType.StoredProcedure;

				// Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
				// Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
				command.Parameters.AddWithValue("dt_session", transferCase.dt_session);
				command.Parameters.AddWithValue("nvc_source", transferCase.nvc_source);
				command.Parameters.AddWithValue("nvc_dns", transferCase.nvc_dns);
				command.Parameters.AddWithValue("nvc_ip", transferCase.nvc_ip);
				command.Parameters.AddWithValue("nvc_status", transferCase.nvc_status);
				command.Parameters.AddWithValue("i_latency", transferCase.i_latency);

				// Execute SQL
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
			}
		}
	}
}
