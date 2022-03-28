namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    class SelectKeyListRetentionStorage
    {
        public static List<string> Execute(string collection, int days)
        {
            List<string> ids = new List<string>();

            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand("usp_PlyQor_Data_Select_Retention-V2", connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nvc_collection", collection);
                    cmd.Parameters.AddWithValue("i_days", days);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var id = (string)reader["nvc_id"];

                        ids.Add(id);
                    }

                    return ids;
                }
            }
            catch (Exception ex)
            {
                throw new JavelinException(StatusCode.ERR010, ex);
            }
        }
    }
}
