using Microsoft.Data.SqlClient;

namespace KirokuG2.Loader.Components.Internal
{
    public class InsertInstanceOperation
    {
        public static bool Execute(LogInstance logInstance, string dataconnectionstring)
        {
            using (var connection = new SqlConnection(dataconnectionstring))
            {
                var cmd = new SqlCommand("usp_KirokuG2_Instance_Insert", connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("dt_session", logInstance.Start);
                cmd.Parameters.AddWithValue("nvc_id", logInstance.Id);
                cmd.Parameters.AddWithValue("nvc_source", logInstance.Source);
                cmd.Parameters.AddWithValue("nvc_function", logInstance.Function);
                cmd.Parameters.AddWithValue("i_duration", logInstance.Duration);
                cmd.Parameters.AddWithValue("i_errors", logInstance.Errors);

                cmd.CommandTimeout = 0;

                connection.Open();

                var reader = cmd.ExecuteReader();

                var recordCount = reader.RecordsAffected;

                return true;
            }
        }
    }
}
