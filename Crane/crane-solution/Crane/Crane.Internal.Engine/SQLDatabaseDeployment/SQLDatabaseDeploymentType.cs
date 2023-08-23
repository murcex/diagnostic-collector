using Crane.Internal.Engine.SQLDatabaseDeployment.Internal;
using Crane.Internal.Loggie;

namespace Crane.Internal.Engine.SQLDatabaseDeployment
{
	public class SQLDatabaseDeploymentType : ICraneTaskType
	{
		public (bool result, string message) Execute(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> script, ISQLAccess sqlAccess)
		{
			logger.Info($"crane_operation=sql-database-deployment");

			var database = string.Empty;
			var account = string.Empty;
			var login = string.Empty;
			var key = string.Empty;
			var storage = string.Empty;

			// get all the values from cfg
			if (script == null)
			{
				logger.Error($"invalid_parameter=script_null");
				throw new Exception("script is null");
			}
			if (script.Count == 0)
			{
				logger.Error($"invalid_parameter=script_empty");
				throw new Exception("script is empty");
			}

			var jobCfg = script["job"];

			if (jobCfg == null)
			{
				logger.Error($"invalid_parameter=job_config_null");
				throw new Exception("job is missing from config");
			}
			if (jobCfg.Count == 0)
			{
				logger.Error($"invalid_parameter=job_config_empty");
				throw new Exception("job is empty");
			}

			string? local;
			// local
			if (jobCfg.TryGetValue("local", out local))
			{
				if (string.IsNullOrEmpty(local))
				{
					local = "true";
				}
			}
			else
			{
				local = "true";
			}
			var isLocal = string.Equals(bool.TrueString, local, StringComparison.OrdinalIgnoreCase);

			// database
			if (jobCfg.TryGetValue("database", out database))
			{
				if (string.IsNullOrEmpty(database))
				{
					logger.Error($"invalid_parameter=database");
					throw new Exception();
				}
			}
			else
			{
				logger.Error($"invalid_parameter=database");
				throw new Exception();
			}

			// account
			if (jobCfg.TryGetValue("account", out account))
			{
				if (!isLocal && string.IsNullOrEmpty(account))
				{
					throw new Exception();
				}
			}
			else
			{
				if (!isLocal)
				{
					logger.Error($"invalid_parameter=account");
					throw new Exception();
				}
			}

			// login
			if (jobCfg.TryGetValue("login", out login))
			{
				if (!isLocal && string.IsNullOrEmpty(login))
				{
					throw new Exception();
				}
			}
			else
			{
				if (!isLocal)
				{
					logger.Error($"invalid_parameter=login");
					throw new Exception();
				}
			}

			// key
			if (jobCfg.TryGetValue("key", out key))
			{
				if (!isLocal && string.IsNullOrEmpty(key))
				{
					throw new Exception();
				}
			}
			else
			{
				if (!isLocal)
				{
					logger.Error($"invalid_parameter=key");
					throw new Exception();
				}
			}

			// storage
			if (jobCfg.TryGetValue("storage", out storage))
			{
				if (!isLocal && string.IsNullOrEmpty(storage))
				{
					throw new Exception();
				}
			}
			else
			{
				logger.Error($"invalid_parameter=storage");
				throw new Exception();
			}

			// build connection string
			string? connectionString;
			if (isLocal)
			{
				// local conn
				connectionString = $"Integrated Security=SSPI; Persist Security Info=False; Initial Catalog={database}; Data Source=localhost";
			}
			else
			{
				// azure conn
				connectionString = $"Server=tcp:{account},1433; Initial Catalog={database}; Persist Security Info=False; User ID={login}; Password={key}; MultipleActiveResultSets=False;";
			}

			if (isLocal)
			{
				logger.Info($"type=local;database={database}");
			}
			else
			{
				logger.Info($"type=remote;account={account};database={database};login={login}");
			}

			// set sql credentials
			sqlAccess.SetCredentials(connectionString);

			int success = 0;
			int error = 0;

			// foreach enum type
			foreach (var sqlObjectType in Enum.GetNames(typeof(SqlObjectType)))
			{
				try
				{
					var directory = Path.Combine(storage, sqlObjectType);

					if (!Directory.Exists(directory))
					{
						logger.Info($"sql_object_missing={sqlObjectType}");
						continue;
					}

					string[] fileEntries = Directory.GetFiles(directory);

					logger.Info($"executing_sql_object={sqlObjectType};count={fileEntries.Length}");

					foreach (var fileName in fileEntries)
					{
						//Console.WriteLine("\t-> '{0}'", Path.GetFileName(fileName));
						//logger.Info($"Executing File: {Path.GetFileName(fileName)}");

						try
						{
							var sqlScript = File.ReadAllText(fileName);

							var result = sqlAccess.Execute(sqlScript);

							if (result.result)
							{
								// console green
								logger.Info($"file={Path.GetFileName(fileName)};result={result.code};message={result.message}");
								success++;
							}
							else
							{
								// console red
								logger.Error($"file={Path.GetFileName(fileName)};result={result.code};message={result.message}");
								error++;
							}
						}
						catch (Exception e)
						{
							logger.Error($"Crane Exception: Read and Execute Payload Exception: {e}");
							error++;
						}
					}
				}
				catch (Exception e)
				{
					logger.Error($"Crane Exception: Read Directory Exception: {e}");
					error++;
				}
			}

			logger.Info($"total={success + error};success={success};error={error}");
		}
	}
}
