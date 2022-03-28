namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    class UpdateKeyWithTagsStroage
    {
        public static int Execute(string collection, string oldid, string newid)
        {
            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand("usp_PlyQor_Tag_Update_Id", connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nvc_collection", collection);
                    cmd.Parameters.AddWithValue("nvc_old_id", oldid);
                    cmd.Parameters.AddWithValue("nvc_new_id", newid);

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    { }

                    var recordCount = reader.RecordsAffected;

                    return recordCount;
                }
            }
            catch (Exception ex)
            {
                throw new JavelinException(StatusCode.ERR010, ex);
            }
        }
    }
}
