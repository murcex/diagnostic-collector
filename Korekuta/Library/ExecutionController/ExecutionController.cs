using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Library
{
    public class ExecutionController
    {
        #region Evaluate
        public static bool Evaluate(string connectionString, string controlSet, string machineName)
        {
            // if-else Flow:
            // START: (1.0) Check Status    T-> (1.1) Take Ownership    -> (1.2) Verify Ownership
            //                              F-> (2.0) Check Session

            // Part 1.0 - Check Status
            if (Controller(connectionString, 1, controlSet, machineName) == true)
            {
                // Part 1.1 - Take Ownership
                if (Controller(connectionString, 2, controlSet, machineName) == true)
                {
                    // Part 1.2 - Verify Ownership
                    if (Controller(connectionString, 3, controlSet, machineName) == true) { return true; }
                    else { return false; }
                }
                else { return false; }
            }

            else
            {
                // Part 2 - Check Session
                if (Controller(connectionString, 4, controlSet, machineName) == true) { return false; }
                else { return false; }
            }
        }

        #endregion

        #region Deallocate
        public static void Deallocate(string connectionString, string controlSet, string machineName)
        {
            Controller(connectionString, 5, controlSet, machineName);
        }
        #endregion

        #region Evaluation Switch Sub-Steps

        public class Result
        {
            public bool Socket { get; set; }
        }

        internal static bool Controller(string connectionString, int controllerSwitch, string controlSet, string machineName)
        {
            Result Payload = new Result();

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                var command = new SqlCommand("usp_ExecutionController", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (controllerSwitch == 1) // Check Ownership
                {
                    command.Parameters.AddWithValue("Switch", 1);
                    command.Parameters.AddWithValue("Controller", controlSet);
                    command.Parameters.AddWithValue("Status", "active");
                    command.Parameters.AddWithValue("Machine", machineName);
                }

                if (controllerSwitch == 2) // Set Ownership
                {
                    command.Parameters.AddWithValue("Switch", 2);
                    command.Parameters.AddWithValue("Controller", controlSet);
                    command.Parameters.AddWithValue("Status", "active");
                    command.Parameters.AddWithValue("Machine", machineName);
                }

                if (controllerSwitch == 3) // Verify Ownership
                {
                    command.Parameters.AddWithValue("Switch", 3);
                    command.Parameters.AddWithValue("Controller", controlSet);
                    command.Parameters.AddWithValue("Status", "active");
                    command.Parameters.AddWithValue("Machine", machineName);
                }

                if (controllerSwitch == 4) // Check Session
                {
                    command.Parameters.AddWithValue("Switch", 4);
                    command.Parameters.AddWithValue("Controller", controlSet);
                    command.Parameters.AddWithValue("Status", "active");
                    command.Parameters.AddWithValue("Machine", machineName);
                }

                if (controllerSwitch == 5) // Deallocate 
                {
                    command.Parameters.AddWithValue("Switch", 5);
                    command.Parameters.AddWithValue("Controller", controlSet);
                    command.Parameters.AddWithValue("Status", "active");
                    command.Parameters.AddWithValue("Machine", machineName);
                }

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Payload.Socket = (bool)reader["Result"];
                    }
                }

                if (Payload.Socket == true)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

        #endregion

    }
}
