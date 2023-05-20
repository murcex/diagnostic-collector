using Microsoft.Data.SqlClient;
using PlyQor.TempCleaner.Core;
using System;

namespace PlyQor.TempCleaner.Components
{
    public class DeleteId
    {
        public static void Execute(string id, bool type)
        {
            var table = type ? "Data" : "Tag";

            try
            {
                var query = $"DELETE FROM [dbo].[tbl_PlyQor_{table}] WHERE [nvc_container] = 'KIROKUG2-LOGS' AND [nvc_id] = '{id}'";

                using (var connection = new SqlConnection(Global.DatabaseConnection))
                {
                    var cmd = new SqlCommand(query, connection);

                    cmd.CommandTimeout = 0;

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    { }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
