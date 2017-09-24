using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Globalization;

namespace Library
{
    public class DatabaseCapacity
    {
        #region Object Properties
        // Set Object Fields to Columns -- copy from Satellite API
        public System.DateTime execution_runtime { get; set; }
        public string artical_name { get; set; }
        public string database_name { get; set; }
        public Nullable<decimal> allocated_capacity { get; set; }
        public Nullable<decimal> utilized_capacity { get; set; }
        public Nullable<decimal> available_capacity { get; set; }
        public Nullable<decimal> available_capacity_percentage { get; set; }
        public Nullable<decimal> average_capacity_growth { get; set; }
        public Nullable<decimal> capacity_threshold { get; set; }
        #endregion

        #region Data Collection Method
        // Data Collection Method
        public static void DataCollection(string url, string connectionString)
        {
            // Setup HttpClient Instance
            HttpClient client = new HttpClient();

            // Set JSON Request Format
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Create and invoke asynchronously an instance of HttpResponse
            HttpResponseMessage response = client.GetAsync(url).Result;

            // Check for Http Status code 200
            if (response.IsSuccessStatusCode)
            {
                //Read response content result into string variable
                string JSON = response.Content.ReadAsStringAsync().Result;

                // Deserialize
                var dataCollection = new JavaScriptSerializer().Deserialize<IEnumerable<DatabaseCapacity>>(JSON);

                // SQL Insert Operation
                foreach (var item in dataCollection)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        // SQLCommand & Command Type
                        SqlCommand command = new SqlCommand("usp_DatabaseCapacity_Insert_vNext", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                        command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                        command.Parameters.AddWithValue("artical_name", item.artical_name);
                        command.Parameters.AddWithValue("database_name", item.database_name);
                        command.Parameters.AddWithValue("allocated_capacity", item.allocated_capacity);
                        command.Parameters.AddWithValue("utilized_capacity", item.utilized_capacity);
                        command.Parameters.AddWithValue("available_capacity", item.available_capacity);
                        command.Parameters.AddWithValue("available_capacity_percentage", item.available_capacity_percentage);
                        command.Parameters.AddWithValue("average_capacity_growth", item.average_capacity_growth);
                        command.Parameters.AddWithValue("capacity_threshold", item.capacity_threshold);

                        // Execute SQL
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                    }
                }
            }
        }
        #endregion
    }
}