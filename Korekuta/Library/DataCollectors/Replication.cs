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

namespace Library
{
    public class Replication
    {
        #region Object Properties
        // Set Object Fields to Columns -- Copy from Satellite
        public Nullable<System.DateTime> replication_latency_runtime { get; set; }
        public string replication_instance_name { get; set; }
        public string replication_stream_name { get; set; }
        public string replication_latency { get; set; }

        #endregion

        #region Data Collection Method
        // Data Collection Method -- Add Method Name
        public static void DataCollection(string url, string connectionString, string serviceName, string environmentName)
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
                // Read response content result into string variable
                string JSON = response.Content.ReadAsStringAsync().Result;

                // Deserialize -- Add varName and Object
                var dataCollection = new JavaScriptSerializer().Deserialize<IEnumerable<Replication>>(JSON);

                // SQL Operation -- Add varName
                foreach (var item in dataCollection)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // SQLCommand & Command Type -- Add SQL Insert Stored Procedure
                        SqlCommand command = new SqlCommand("usp_Replication_Insert", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);            
                        command.Parameters.AddWithValue("dt_session", item.replication_latency_runtime);
                        command.Parameters.AddWithValue("nvc_service", serviceName);
                        command.Parameters.AddWithValue("nvc_environment", environmentName);
                        command.Parameters.AddWithValue("nvc_stream", item.replication_stream_name);
                        command.Parameters.AddWithValue("i_latency", item.replication_latency);

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