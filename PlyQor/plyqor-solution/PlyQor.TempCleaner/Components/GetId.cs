using Microsoft.Data.SqlClient;
using PlyQor.TempCleaner.Core;
using System;
using System.Collections.Generic;

namespace PlyQor.TempCleaner.Components
{
    public class GetId
    {
        public static List<string> Execute()
        {
            List<string> ids = new List<string>();

            try
            {
                var query = "SELECT TOP (1000) [nvc_id] FROM [tbl_PlyQor_Data] WHERE [nvc_container] = 'KIROKUG2-LOGS' AND [dt_timestamp] < DATEADD(DAY,-7,GETDATE())";

                using (var connection = new SqlConnection(Global.DatabaseConnection))
                {
                    var cmd = new SqlCommand(query, connection);

                    cmd.CommandTimeout = 0;

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = (string)reader["nvc_id"];

                        ids.Add(id);
                    }

                    return ids;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return null;
            }
        }
    }
}
