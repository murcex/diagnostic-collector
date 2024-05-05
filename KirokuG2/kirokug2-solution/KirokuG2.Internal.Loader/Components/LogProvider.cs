using KirokuG2.Internal.Loader.Interface;
using PlyQor.Client;

namespace KirokuG2.Internal.Loader.Components
{
	public class LogProvider : ILogProvider
	{
		private static PlyClient _plyClient;

		private static IKLogSeralializer _seralializer;

		public LogProvider(PlyClient plyClient, IKLogSeralializer seralializer)
		{
			_plyClient = plyClient;
			_seralializer = seralializer;
		}

		public List<string> GetLogIds(string tag, int top)
		{
			return _plyClient.Select(tag, top).GetPlyList();
		}

		public Dictionary<string, List<string>> GetLogsById(string id)
		{
			var logData = _plyClient.Select(id).GetPlyData();

			return _seralializer.DeseralizalizeLogSet(logData);
		}

		public string Select(string id)
		{
			return _plyClient.Select(id).GetPlyData();
		}

		public void UpdateTag(string id, string tag, string newTag)
		{
			_plyClient.UpdateTag(id, tag, newTag);
		}
	}
}
