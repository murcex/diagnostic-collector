namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    class SelectRetentionKeysStorage
    {
        public static List<string> Execute(
            string container,
            int top,
            DateTime threshold)
        {
            List<string> ids = new List<string>();

            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand(SqlValues.SelectRetentionKeysStorage, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // TODO: add values to const
                    cmd.Parameters.AddWithValue(SqlValues.Container, container);
                    cmd.Parameters.AddWithValue("i_top", top);
                    cmd.Parameters.AddWithValue("dt_threshold", threshold);

                    cmd.CommandTimeout = 0;

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var id = (string)reader[SqlValues.Id];

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

                throw new PlyQorException(StatusCode.ERR010, ex);
            }
        }
    }
}
