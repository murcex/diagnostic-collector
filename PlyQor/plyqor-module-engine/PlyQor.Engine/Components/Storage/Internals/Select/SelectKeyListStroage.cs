namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    public class SelectKeyListStroage
    {
        public static List<string> Execute(string collection, string tag, int top)
        {
            List<string> ids = new List<string>();

            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand(SqlColumns.SelectKeyListStroage, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(SqlColumns.Top, top);
                    cmd.Parameters.AddWithValue(SqlColumns.Collection, collection);
                    cmd.Parameters.AddWithValue(SqlColumns.Data, tag);

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = (string)reader[SqlColumns.Id];

                        ids.Add(id);
                    }

                    return ids;
                }
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    SqlExceptionCheck.Execute(ex);
                }

                throw new JavelinException(StatusCode.ERR010, ex);
            }
        }
    }
}
