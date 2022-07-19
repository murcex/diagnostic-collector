using System.Data.SqlClient;

namespace KirokuG2.Loader.Components.Internal
{
    public class InsertErrorOperation
    {
        public static bool Execute(LogError logError, string dataconnectionstring)
        {
            using (var connection = new SqlConnection(dataconnectionstring))
            {
                var cmd = new SqlCommand("usp_KirokuG2_Error_Insert", connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("dt_session", logError.Timestamp);
                cmd.Parameters.AddWithValue("nvc_id", logError.Id);
                cmd.Parameters.AddWithValue("nvc_source", logError.Source);
                cmd.Parameters.AddWithValue("nvc_function", logError.Function);
                cmd.Parameters.AddWithValue("nvc_message", logError.Message);

                cmd.CommandTimeout = 0;

                connection.Open();

                var reader = cmd.ExecuteReader();

                var recordCount = reader.RecordsAffected;

                return true;
            }
        }
    }
}
