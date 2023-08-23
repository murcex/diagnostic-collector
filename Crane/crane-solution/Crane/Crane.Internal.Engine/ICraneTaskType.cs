using Crane.Internal.Engine.SQLDatabaseDeployment.Internal;
using Crane.Internal.Loggie;

namespace Crane.Internal.Engine
{
	public interface ICraneTaskType
	{
		(bool result, string message) Execute(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> script, ISQLAccess sqlAccess);
	}
}