using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Library
{ 
    public class ConnectionController
    {
        #region Connection Object
        public class Connection
        {
            // Connections Properties
            public string Release { get; set; }
            public string Workflow { get; set; }
            public string Service { get; set; }
            public string Environment { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        #endregion

        #region Collection Object
        public class Collection
        {
            // URI One
            public string Uri_1_ServiceName { get; set; }
            public string Uri_1_Environment { get; set; }
            public string Uri_1_DatabaseCapacity { get; set; }
            public string Uri_1_ServiceAccount { get; set; }
            public string Uri_1_Certificate { get; set; }
            public string Uri_1_Replication { get; set; }
            public string Uri_1_SQLAvailability { get; set; }

            // URI Two
            public string Uri_2_ServiceName { get; set; }
            public string Uri_2_Environment { get; set; }
            public string Uri_2_DatabaseCapacity { get; set; }
            public string Uri_2_Replication { get; set; }
            public string Uri_2_SQLAvailability { get; set; }

            // URI Three
            public string Uri_3_ServiceName { get; set; }
            public string Uri_3_Environment { get; set; }
            public string Uri_3_DatabaseCapacity { get; set; }
            public string Uri_3_Replication { get; set; }
            public string Uri_3_SQLAvailability { get; set; }
        }

        #endregion

        #region Connection Controller
        // Return Connections by Enviroment
        public static Collection GetConnections(string connectionString, string enviromentType)
        {
            var connections = new List<Connection>();

            var collection = new Collection();

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("usp_Article_Connections_Load", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nvc_release", enviromentType);

                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var item = new Connection();
                    item.Release = (string)reader["nvc_release"];
                    item.Workflow = (string)reader["nvc_workflow"];
                    item.Service = (string)reader["nvc_service"];
                    item.Environment = (string)reader["nvc_environment"];
                    item.Key = (string)reader["nvc_key"];
                    item.Value = (string)reader["nvc_value"];
                    connections.Add(item);
                }
            }

            //// URI One
            // Service Name & Environment
            collection.Uri_1_ServiceName = connections.Where(x => x.Workflow == "1").Select(x => x.Service).FirstOrDefault();

            collection.Uri_1_Environment = connections.Where(x => x.Workflow == "1").Select(x => x.Environment).FirstOrDefault();
            
            // URI ID's
            collection.Uri_1_DatabaseCapacity = connections.Where(x => x.Workflow == "1").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "DatabaseCapacity").Select(x => x.Value).FirstOrDefault();

            collection.Uri_1_ServiceAccount = connections.Where(x => x.Workflow == "1").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "ServiceAcount").Select(x => x.Value).FirstOrDefault();

            collection.Uri_1_Certificate = connections.Where(x => x.Workflow == "1").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "Certificate").Select(x => x.Value).FirstOrDefault();

            collection.Uri_1_Replication = connections.Where(x => x.Workflow == "1").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "Replication").Select(x => x.Value).FirstOrDefault();

            collection.Uri_1_SQLAvailability = connections.Where(x => x.Workflow == "1").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "SQLAvailability").Select(x => x.Value).FirstOrDefault();

            //// URI Two
            // Service Name & Environment
            collection.Uri_2_ServiceName = connections.Where(x => x.Workflow == "2").Select(x => x.Service).FirstOrDefault();

            collection.Uri_2_Environment = connections.Where(x => x.Workflow == "2").Select(x => x.Environment).FirstOrDefault();
            
            // URI ID's
            collection.Uri_2_DatabaseCapacity = connections.Where(x => x.Workflow == "2").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "DatabaseCapacity").Select(x => x.Value).FirstOrDefault();

            collection.Uri_2_Replication = connections.Where(x => x.Workflow == "2").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "Replication").Select(x => x.Value).FirstOrDefault();

            collection.Uri_2_SQLAvailability = connections.Where(x => x.Workflow == "2").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "SQLAvailability").Select(x => x.Value).FirstOrDefault();

            //// URI Three
            // Service Name & Environment
            collection.Uri_3_ServiceName = connections.Where(x => x.Workflow == "3").Select(x => x.Service).FirstOrDefault();

            collection.Uri_3_Environment = connections.Where(x => x.Workflow == "3").Select(x => x.Environment).FirstOrDefault();

            // URI ID's
            collection.Uri_3_DatabaseCapacity = connections.Where(x => x.Workflow == "3").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "DatabaseCapacity").Select(x => x.Value).FirstOrDefault();

            collection.Uri_3_Replication = connections.Where(x => x.Workflow == "3").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "Replication").Select(x => x.Value).FirstOrDefault();

            collection.Uri_3_SQLAvailability = connections.Where(x => x.Workflow == "3").Select(x => x.Value).FirstOrDefault() + connections.Where(x => x.Service == "SQLAvailability").Select(x => x.Value).FirstOrDefault();

            return collection;
        }

        #endregion
    }
}
