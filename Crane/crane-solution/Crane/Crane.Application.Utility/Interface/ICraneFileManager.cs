using Crane.Internal.Loggie;

namespace Crane.Application.Utility.Interface
{
	public interface ICraneFileManager
	{
		string GetCraneLoggerFilePath(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> cfg);
		Dictionary<string, Dictionary<string, string>> LoadCraneConfig(ICraneLogger logger);
		Dictionary<string, Dictionary<string, string>> LoadCraneTask(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> cfg, string name);
	}
}