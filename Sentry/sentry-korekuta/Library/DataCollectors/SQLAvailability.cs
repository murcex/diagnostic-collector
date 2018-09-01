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
    public class SQLAvailability
    {
        #region Object Properties
        // Set Object Fields to Columns -- Copy from Satellite
        public Nullable<System.DateTime> dt_session { get; set; }
        public string nvc_machine { get; set; }
        public Nullable<int> i_statuscode { get; set; }
        public string nvc_status { get; set; }
        public string i_uptime { get; set; }
        public string nvc_active { get; set; }

        #endregion

        #region Data Collection Method
        // Data Collection Method -- Add Method Name
        public static void DataCollection(string url, string connectionString, string serviceName, string environmentNameg)
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
                var dataCollection = new JavaScriptSerializer().Deserialize<IEnumerable<SQLAvailability>>(JSON);

                // SQL Operation -- Add varName
                foreach (var item in dataCollection)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // SQLCommand & Command Type -- Add SQL Insert Stored Procedure
                        SqlCommand command = new SqlCommand("usp_SQLAvailability_Insert", connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);            
                        command.Parameters.AddWithValue("dt_session", item.dt_session);
                        command.Parameters.AddWithValue("nvc_service", serviceName);
                        command.Parameters.AddWithValue("nvc_environment", environmentNameg);
                        command.Parameters.AddWithValue("nvc_machine", item.nvc_machine);
                        command.Parameters.AddWithValue("i_statuscode", item.i_statuscode);
                        command.Parameters.AddWithValue("nvc_status", item.nvc_status);
                        command.Parameters.AddWithValue("i_uptime", item.i_uptime);
                        command.Parameters.AddWithValue("nvc_active", item.nvc_active);

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