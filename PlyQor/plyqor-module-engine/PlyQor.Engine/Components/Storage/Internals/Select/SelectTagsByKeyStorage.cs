namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    public class SelectTagsByKeyStorage
    {
        public static List<string> Execute(
            string container, 
            string id)
        {
            List<string> indexes = new List<string>();

            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand(SqlColumns.SelectTagsByKeyStroage, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(SqlColumns.Container, container);
                    cmd.Parameters.AddWithValue(SqlColumns.Id, id);

                    cmd.CommandTimeout = 0;

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var index = (string)reader[SqlColumns.Data];

                        indexes.Add(index);
                    }

                    return indexes;
                }
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    SqlExceptionCheck.Execute(ex);
                }

                throw new PlyQorException(StatusCode.ERR010, ex);
            }
        }
    }
}
