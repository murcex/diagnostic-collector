using Crane.Internal.Loggie;

namespace Crane.Internal.Engine
{
	public interface ICraneTaskManager
	{
		void Execute(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> script);
	}
}