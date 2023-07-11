using Microsoft.Data.SqlClient;

namespace KirokuG2.Loader.Components.Internal
{
    public class InsertBlockOperation
    {
        public static bool Execute(LogBlock logBlock, string dataconnectionstring)
        {
            using (var connection = new SqlConnection(dataconnectionstring))
            {
                var cmd = new SqlCommand("usp_KirokuG2_Block_Insert", connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("dt_session", logBlock.Start);
                cmd.Parameters.AddWithValue("nvc_id", logBlock.Id);
                cmd.Parameters.AddWithValue("nvc_tag", logBlock.Tag);
                cmd.Parameters.AddWithValue("nvc_source", "empty");
                cmd.Parameters.AddWithValue("nvc_function", "empty");
                cmd.Parameters.AddWithValue("nvc_name", logBlock.Name);
                cmd.Parameters.AddWithValue("i_duration", logBlock.Duration);

                cmd.CommandTimeout = 0;

                connection.Open();

                var reader = cmd.ExecuteReader();

                var recordCount = reader.RecordsAffected;

                return true;
            }
        }
    }
}
