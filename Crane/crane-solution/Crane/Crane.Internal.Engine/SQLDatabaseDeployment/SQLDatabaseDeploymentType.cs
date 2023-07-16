using Crane.Internal.Engine.SQLDatabaseDeployment.SQLDatabaseDeployment;

namespace Crane.Internal.Engine.SQLDatabaseDeployment
{
	public class SQLDatabaseDeploymentType
	{
		public void Execute(Dictionary<string, Dictionary<string, string>> script)
		{
			// get all the values from cfg
			var local = "";
			var account = "";
			var database = "";
			var key = "";
			var storage = string.Empty;

			// build connection string
			var connectionString = string.Empty;
			if (local == "true")
			{
				// local conn
			}
			else
			{
				// azure conn
			}

			// build sql executor
			SQLAccess sqlAccess = new SQLAccess(connectionString);

			// foreach enum type
			foreach (var sqlObjectType in Enum.GetNames(typeof(SqlObjectType)))
			{
				try
				{
					string[] fileEntries = Directory.GetFiles(storage + @"\" + sqlObjectType);

					foreach (var fileName in fileEntries)
					{
						Console.WriteLine("\t-> '{0}'", Path.GetFileName(fileName));
						//Log.Info($"\r\nExecuting File: '{Path.GetFileName(fileName)}'");

						try
						{
							var sqlScript = File.ReadAllText(fileName);

							var result = sqlAccess.Execute(sqlScript);
						}
						catch (Exception e)
						{
							//Log.Error($"Read and Execute Payload Exception: {e.ToString()}");
						}
					}
				}
				catch (Exception e)
				{
					//Log.Error($"Read Directory Exception: {e.ToString()}");
				}
			}
		}
	}
}
