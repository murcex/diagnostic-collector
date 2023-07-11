using Microsoft.Data.SqlClient;

namespace KirokuG2.Loader.Components.Internal
{
    public class InsertMetricOperation
    {
        public static bool Execute(LogMetric logMetric, string dataconnectionstring)
        {
            using (var connection = new SqlConnection(dataconnectionstring))
            {
                var cmd = new SqlCommand("usp_KirokuG2_Metric_Insert", connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("dt_session", logMetric.Timestamp);
                cmd.Parameters.AddWithValue("nvc_id", logMetric.Id);
                cmd.Parameters.AddWithValue("nvc_source", logMetric.Source);
                cmd.Parameters.AddWithValue("nvc_function", logMetric.Function);
                cmd.Parameters.AddWithValue("i_type", logMetric.Type);
                cmd.Parameters.AddWithValue("nvc_key", logMetric.Key);
                cmd.Parameters.AddWithValue("nvc_value", logMetric.Value);

                cmd.CommandTimeout = 0;

                connection.Open();

                var reader = cmd.ExecuteReader();

                var recordCount = reader.RecordsAffected;

                return true;
            }
        }
    }
}
