using Microsoft.Data.SqlClient;
using PlyQor.TempCleaner.Core;
using System;

namespace PlyQor.TempCleaner.Components
{
    public class DeleteId
    {
        public static (bool result, int recordCount) Execute(string container, string id, bool type)
        {
            var table = type ? "Data" : "Tag";

            try
            {
                var query = $"DELETE FROM [dbo].[tbl_PlyQor_{table}] WHERE [nvc_container] = '{container}' AND [nvc_id] = '{id}'";

                using (var connection = new SqlConnection(Global.DatabaseConnection))
                {
                    var cmd = new SqlCommand(query, connection);

                    cmd.CommandTimeout = 0;

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    { }

                    return (true, 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return (false, 0);
            }
        }
    }
}
