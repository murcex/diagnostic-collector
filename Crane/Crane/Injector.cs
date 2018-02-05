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
		public static void Execute(string articleCase)
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
					if (reader.RecordsAffected == -1) { Console.WriteLine("\t\t<!> Success"); }
					if (reader.RecordsAffected > 0) { Console.WriteLine("\t\t<!> Success (Rows Affected: {0})", reader.RecordsAffected); }
					if (reader.RecordsAffected == 0) { Console.WriteLine("\t\t<!> Failure"); }
				}

				catch (Exception e)
				{
					// Check Exception for 'IF EXISTS'
					var check = e.ToString();
					if (check.Contains("There is already an object named"))
					{
						Console.WriteLine("\t\t<!> Success (Object Exists)");
					}

					// Return All Other Exceptions
					else
					{
						Console.WriteLine("\t\tResult: Failure (SQL Exception)");
					}
				}
			}
		}
	}
}
