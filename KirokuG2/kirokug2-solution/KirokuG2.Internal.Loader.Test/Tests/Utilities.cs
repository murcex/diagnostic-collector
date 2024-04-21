using KirokuG2.Internal.Loader.Test.Mocks;
using KirokuG2.Internal.Loader.Test.Models;
using KirokuG2.Loader;

namespace KirokuG2.Internal.Loader.Test.Tests
{
	public static class Utilities
	{
		public static void TestProcessor(Dictionary<string, string> logs, Dictionary<string, string> logTracker, Dictionary<string, string> sqlTracker, List<string> klogTracker)
		{
			MockLogProvider mockLogProvider = new(logs, logTracker);

			MockSQLProvider mockSQLProvider = new(sqlTracker);

			KLoaderManager.Configuration(mockLogProvider, mockSQLProvider);

			MockKLog mockKLog = new MockKLog(klogTracker);

			KLoaderManager.ProcessLogs(mockKLog);
		}

		public static bool CheckTrackerTypeCount(this Dictionary<string, string> tracker, string type, int count)
		{
			var itemCount = 0;

			foreach (var item in tracker)
			{
				var itemType = item.Key.Split("-")[1];

				if (string.Equals(type, itemType, StringComparison.OrdinalIgnoreCase))
				{
					itemCount++;
				}
			}

			return itemCount == count;
		}

		public static bool CheckTrackerContains(this Dictionary<string, string> tracker, string type, params string[] inputs)
		{
			foreach (var item in tracker)
			{
				var itemType = item.Key.Split("-")[1];

				if (string.Equals(type, itemType, StringComparison.OrdinalIgnoreCase))
				{
					var matchTotal = inputs.Length;
					var matchCount = 0;
					foreach (var input in inputs)
					{
						if (item.Value.Contains(input, StringComparison.OrdinalIgnoreCase))
						{
							matchCount++;
						}
					}

					if (matchCount == matchTotal)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
