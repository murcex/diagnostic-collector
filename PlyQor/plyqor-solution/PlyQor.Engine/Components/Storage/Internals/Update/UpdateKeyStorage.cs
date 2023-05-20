namespace PlyQor.Engine.Components.Storage.Internals
{
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;
    using System;

    class UpdateKeyStorage
    {
        public static int Execute(
            string container,
            string oldid,
            string newid)
        {
            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand(SqlValues.UpdateKeyStorage, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(SqlValues.Container, container);
                    cmd.Parameters.AddWithValue(SqlValues.OldId, oldid);
                    cmd.Parameters.AddWithValue(SqlValues.NewId, newid);

                    cmd.CommandTimeout = 0;

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

                throw new PlyQorException(StatusCode.ERR010, ex);
            }
        }
    }
}
