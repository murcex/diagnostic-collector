using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Crane
{
	class Injector
	{
		public static string Execute(string articleCase)
		{
			using (SqlConnection connection = new SqlConnection(Global.SQLConnectionString))
			{
				// Apply SQL Article to SQL Instance
				SqlCommand command = new SqlCommand(articleCase, connection);

				try
				{
					// Execute SQL
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();

					// Return Results to Console
					if (reader.RecordsAffected == -1) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\t<!> Success"); Console.ResetColor(); }
					if (reader.RecordsAffected > 0) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("\t\t<!> Success (Rows Affected: {0})", reader.RecordsAffected); Console.ResetColor(); }
					if (reader.RecordsAffected == 0) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\t\t<!> Failure"); Console.ResetColor(); }

                    return "Success";
				}

				catch (Exception e)
				{
					// Check Exception for 'IF EXISTS'
					var check = e.ToString();
					if (check.Contains("There is already an object named"))
					{
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\t\t<!> Success (Object Exists)");
                        Console.ResetColor();

                        return "Success (Object Exists)";
                    }

					// Return All Other Exceptions
					else
					{
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\tResult: Failure (SQL Exception)");
                        Console.ResetColor();

                        return $"Failure (SQL Exception): {e.ToString()}";
                    }
				}
			}
		}
	}
}
