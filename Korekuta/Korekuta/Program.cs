using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Library;
using System.Data.SqlClient;

namespace Korekuta
{
    class Program
    {
        static void Main(string[] args)
        {
            #region App.config & Metadata
            // Extract App.config
            // SQL & URI Targets
            var connectionSQL = ConfigurationManager.ConnectionStrings["SQL"].ToString();
            var enviromentType = ConfigurationManager.AppSettings["ENV"].ToString();
            var controllerType = ConfigurationManager.AppSettings["CTRL"].ToString();

            // Machine Name
            var machineName = System.Environment.MachineName;

            // Build Connection Payload 
            var connections = ConnectionController.GetConnections(connectionSQL, enviromentType);

            // Setup API Collection One
            var apiCollectionOneService = connections.Uri_1_ServiceName.ToString();
            var apiCollectionOneEnvironment = connections.Uri_1_Environment.ToString();
            var apiCollectionOneDatabaseCapacity = connections.Uri_1_DatabaseCapacity.ToString();
            var apiCollectionOneServiceAccount = connections.Uri_1_ServiceAccount.ToString();
            var apiCollectionOneCertificate = connections.Uri_1_Certificate.ToString();
            var apiCollectionOneReplication = connections.Uri_1_Replication.ToString();
            var apiCollectionOneSQLAvailability = connections.Uri_1_SQLAvailability;

            // Setup API Collection Two
            var apiCollectionTwoService = connections.Uri_2_ServiceName.ToString();
            var apiCollectionTwoEnvironment = connections.Uri_2_Environment.ToString();
            var apiCollectionTwoDatabaseCapacity = connections.Uri_2_DatabaseCapacity.ToString();
            var apiCollectionTwoReplication = connections.Uri_2_Replication;
            var apiCollectionSQLAvailability = connections.Uri_2_SQLAvailability;

            // Setup API Collection Three
            var apiCollectionThreeService = connections.Uri_3_ServiceName.ToString();
            var apiCollectionThreeEnvironment = connections.Uri_3_Environment.ToString();
            var apiCollectionThreeDatabaseCapacity = connections.Uri_3_DatabaseCapacity.ToString();
            var apiCollectionThreeReplication = connections.Uri_3_Replication;
            var apiCollectionThreeSQLAvailability = connections.Uri_3_SQLAvailability;

            // Service Logging
            //ServiceLog.AddEntry(connectionSQL, DateTime.Now, sessionSet, "app.config", "set", "completed");

            #endregion

            #region Execution Controller

            // Execution Controller Methods
            bool generationOneDataCollection = ExecutionController.Evaluate(connectionSQL, controllerType, machineName);

            #endregion

            #region Data Collection

            if (generationOneDataCollection == true)
            {
                // Data Collection Method -- URI One
                DatabaseCapacity.DataCollection(apiCollectionOneDatabaseCapacity, connectionSQL, apiCollectionOneService, apiCollectionOneEnvironment);
                Replication.DataCollection(apiCollectionOneReplication, connectionSQL, apiCollectionOneService, apiCollectionOneEnvironment);
                SQLAvailability.DataCollection(apiCollectionOneSQLAvailability, connectionSQL, apiCollectionOneService, apiCollectionOneEnvironment);

                // Data Collection Method -- URI Two
                DatabaseCapacity.DataCollection(apiCollectionTwoDatabaseCapacity, connectionSQL, apiCollectionTwoService, apiCollectionTwoEnvironment);
                Replication.DataCollection(apiCollectionTwoReplication, connectionSQL, apiCollectionTwoService, apiCollectionTwoEnvironment);
                SQLAvailability.DataCollection(apiCollectionSQLAvailability, connectionSQL, apiCollectionTwoService, apiCollectionTwoEnvironment);

                // Data Collection Method -- URI Three
                DatabaseCapacity.DataCollection(apiCollectionThreeDatabaseCapacity, connectionSQL, apiCollectionThreeService, apiCollectionThreeEnvironment);
                Replication.DataCollection(apiCollectionThreeReplication, connectionSQL, apiCollectionThreeService, apiCollectionThreeEnvironment);
                SQLAvailability.DataCollection(apiCollectionThreeSQLAvailability, connectionSQL, apiCollectionThreeService, apiCollectionThreeEnvironment);

            }

            #endregion

            #region Post Data Collection

            // Add Retention

            // Add Table Count

            #endregion

            #region Deallocation

            if (generationOneDataCollection == true)
            {
                // Deallocate Execution Controller
                ExecutionController.Deallocate(connectionSQL, controllerType, machineName);

            }
            #endregion

            #region Execution Logging

            if (generationOneDataCollection == true)
            {
                ExecutionLog.AddEntry(connectionSQL, DateTime.Now, machineName, controllerType, "pass,completed");
            }

            else
            {
                ExecutionLog.AddEntry(connectionSQL, DateTime.Now, machineName, controllerType, "fail,closed");
            }
            #endregion
        }
    }
}
