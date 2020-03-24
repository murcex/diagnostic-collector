using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Library
{
    public static class ServiceLog
    {
        // Insert Method
        public static void AddEntry(string connectionString, DateTime sessionTime, int set, string key, string subkey, string value)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQLCommand & Command Type
                SqlCommand command = new SqlCommand("usp_ServiceLog_AddEntry", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                command.Parameters.AddWithValue("dt_session", sessionTime);
                command.Parameters.AddWithValue("i_set", set);
                command.Parameters.AddWithValue("nvc_key", key);
                command.Parameters.AddWithValue("nvc_subkey", subkey);
                command.Parameters.AddWithValue("nvc_value", value);

                // Execute SQL
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
        }
    }
}
