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
    public class Certificate
    {
        #region Object Properties
        // Set Object Fields to Columns -- Copy from Satellite
        public Nullable<System.DateTime> dt_session { get; set; }
        public string nvc_certificate { get; set; }
        public Nullable<bool> b_enabled { get; set; }
        public string nvc_service { get; set; }
        public string nvc_environment { get; set; }
        public Nullable<System.DateTime> dt_expiration { get; set; }
        public string nvc_ssladmin { get; set; }
        public string nvc_notes { get; set; }
        #endregion

        #region Data Collection Method
        // Data Collection Method -- Add Method Name
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

                // Deserialize -- Add varName and Object
                var dataCollection = new JavaScriptSerializer().Deserialize<IEnumerable<Certificate>>(JSON);

                // SQL Operation -- Add varName
                foreach (var item in dataCollection)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // SQLCommand & Command Type -- Add SQL Insert Stored Procedure
                        SqlCommand command = new SqlCommand("usp_Certificate_Insert", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                        // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                        command.Parameters.AddWithValue("dt_session", item.dt_session);
                        command.Parameters.AddWithValue("nvc_certificate", item.nvc_certificate);
                        command.Parameters.AddWithValue("b_enabled", item.b_enabled);
                        command.Parameters.AddWithValue("nvc_service", item.nvc_service);
                        command.Parameters.AddWithValue("nvc_environment", item.nvc_environment);
                        command.Parameters.AddWithValue("dt_expiration", item.dt_expiration);
                        command.Parameters.AddWithValue("nvc_sslrecord", ""); // TODO: Correct
                        command.Parameters.AddWithValue("nvc_notes", item.nvc_notes);

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