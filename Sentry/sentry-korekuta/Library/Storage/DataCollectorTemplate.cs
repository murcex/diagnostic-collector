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
    //// Replace:
    //// *AddObject* = Class name, object name to fill JSON data from Satellite collection
    //// *AddMethodNmame* = Data Collection Method; DataCollection_<ClassName>
    //// *AddStoreProcedure* = Insert Stored Procedure
    
    //public class *AddObject*
    //{
        //// Set Object Fields to Columns -- Copy from Satellite

        //// Data Collection Method -- Add Method Name
        //public void *AddMethodName*(string url, string connectionString)
        //{
            //// Setup HttpClient Instance
            //HttpClient client = new HttpClient();

            //// Set JSON Request Format
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //// Create and invoke asynchronously an instance of HttpResponse
            //HttpResponseMessage response = client.GetAsync(url).Result;

            //// Check for Http Status code 200
            //if (response.IsSuccessStatusCode)
            //{
                //// Read response content result into string variable
                //string JSON = response.Content.ReadAsStringAsync().Result;

                //// Post Results to Console
                //Console.WriteLine(JSON);
                //Console.ReadKey();

                //// Deserialize -- Add varName and Object
                //var dataCollection = new JavaScriptSerializer().Deserialize<IEnumerable<*AddObject*>>(JSON);

                //// SQL Operation -- Add varName
                //foreach (var item in dataCollection)
                //{
                    //using (SqlConnection connection = new SqlConnection(connectionString))
                    //{
                        //// SQLCommand & Command Type -- Add SQL Insert Stored Procedure
                        //SqlCommand command = new SqlCommand("*AddStoreProcedure*", connection);
                        //command.CommandType = System.Data.CommandType.StoredProcedure;

                        //// Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);            
                        //Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);

                        //// Execute SQL
                        //connection.Open();
                        //SqlDataReader reader = command.ExecuteReader();
                    //}
                //}
                //// Console Notice
                //Console.WriteLine("Insert Complete");
                //Console.ReadKey();
            //}
        //}
    //}
}