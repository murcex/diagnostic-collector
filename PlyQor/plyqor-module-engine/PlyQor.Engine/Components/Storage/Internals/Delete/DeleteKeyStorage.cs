namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    public class DeleteKeyStorage
    {
        public static int Execute(string collection, string id)
        {
            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand(SqlColumns.DeleteKeyStorage, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(SqlColumns.Collection, collection);
                    cmd.Parameters.AddWithValue(SqlColumns.Id, id);

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
                if (ex is SqlException)
                {
                    SqlExceptionCheck.Execute(ex);
                }

                throw new JavelinException(StatusCode.ERR010, ex);
            }
        }
    }
}
