using Microsoft.Data.SqlClient;

namespace KirokuG2.Loader.Components.Internal
{
	public class InsertActivationOperation
	{
		public static bool Execute(DateTime session, string record_id, string source, string dataconnectionstring)
		{
			using (var connection = new SqlConnection(dataconnectionstring))
			{
				var cmd = new SqlCommand("usp_KirokuG2_Activation_Insert", connection);

				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("dt_session", session);
				cmd.Parameters.AddWithValue("nvc_id", record_id);
				cmd.Parameters.AddWithValue("nvc_source", source);

				cmd.CommandTimeout = 0;

				connection.Open();

				var reader = cmd.ExecuteReader();

				var recordCount = reader.RecordsAffected;

				return true;
			}
		}
	}
}
