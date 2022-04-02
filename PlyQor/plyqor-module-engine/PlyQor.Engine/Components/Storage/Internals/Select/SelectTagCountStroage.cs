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
                    var cmd = new SqlCommand(SqlColumns.SelectTagCountStroage, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(SqlColumns.Collection, collection);
                    cmd.Parameters.AddWithValue(SqlColumns.Data, tag);

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count = (int)reader[SqlColumns.Count];
                    }

                    return count;
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
