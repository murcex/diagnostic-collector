namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    public class SelectTagCountStroage
    {
        public static int Execute(string collection, string tag)
        {
            int count = 0;

            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand("usp_PlyQor_Tag_Count", connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nvc_collection", collection);
                    cmd.Parameters.AddWithValue("nvc_data", tag);

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count = (int)reader["i_count"];
                    }

                    return count;
                }
            }
            catch (Exception ex)
            {
                throw new JavelinException(StatusCode.ERR010, ex);
            }
        }
    }
}
