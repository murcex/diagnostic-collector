namespace PlyQor.AdminTool
{
    using System.Data.SqlClient;

    public class Storage
    {
        /// <summary>
        /// Query the PlyQor System container for current container config.
        /// </summary>
        public static string SelectContainerConfig()
        {
            string data = string.Empty;

            using (var connection = new SqlConnection(Configuration.DatabaseConnection))
            {
                var cmd = new SqlCommand(Configuration.SelectStoredProcedure, connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(Configuration.ParameterId, Configuration.ContainersId);

                cmd.CommandTimeout = 0;

                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data = (string)reader[Configuration.ParameterData];
                }

                return data;
            }
        }

        /// <summary>
        /// Upsert a serialized container config Json string to the PlyQor System container.
        /// </summary>
        public static bool UpsertContainerConfig(string container_config)
        {
            using (var connection = new SqlConnection(Configuration.DatabaseConnection))
            {
                var cmd = new SqlCommand(Configuration.UpsertStoredProcedure, connection);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(Configuration.ParameterTimeStamp, DateTime.UtcNow);
                cmd.Parameters.AddWithValue(Configuration.ParameterId, Configuration.ContainersId);
                cmd.Parameters.AddWithValue(Configuration.ParameterData, container_config);

                cmd.CommandTimeout = 0;

                connection.Open();

                var reader = cmd.ExecuteReader();

                var recordCount = reader.RecordsAffected;

                return true;
            }
        }
    }
}
