using System.Collections.Generic;

namespace Implements.Function.Queue.Source.Core
{
	public class Configuration
	{
		private static string _database;

		private static string _token;

		private static string _endPoint;

		public static string Database => _database;

		public static string Token => _token;

		public static string Endpoint => _endPoint;

		public static bool Load(Dictionary<string, string> configuration)
		{
			foreach (var kvp in configuration)
			{
				Sort(kvp.Key, kvp.Value);
			}

			PostLoad();

			return true;
		}

		private static string Sort(string key, string value) => key.ToUpper() switch
		{
			"DATABASE" => _database = value,
			"TOKEN" => _token = value,
			"ENDPOINT" => _endPoint = value,
			_ => null,
		};

		private static bool PostLoad()
		{
			return true;
		}

	}
}
