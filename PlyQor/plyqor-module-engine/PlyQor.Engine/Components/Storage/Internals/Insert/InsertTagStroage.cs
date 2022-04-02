namespace PlyQor.Engine.Components.Storage.Internals
{
    using System;
    using Microsoft.Data.SqlClient;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;

    class InsertTagStroage
    {
        public static int Execute(
            DateTime timestamp,
            string collection,
            string id,
            string data)
        {
            try
            {
                using (var connection = new SqlConnection(Configuration.DatabaseConnection))
                {
                    var cmd = new SqlCommand(SqlColumns.InsertTagStroage, connection);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(SqlColumns.TimeStamp, timestamp);
                    cmd.Parameters.AddWithValue(SqlColumns.Collection, collection);
                    cmd.Parameters.AddWithValue(SqlColumns.Id, id);
                    cmd.Parameters.AddWithValue(SqlColumns.Data, data);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

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
