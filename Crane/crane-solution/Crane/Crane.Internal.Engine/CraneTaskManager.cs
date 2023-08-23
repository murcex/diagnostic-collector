using Crane.Internal.Engine.SQLDatabaseDeployment;
using Crane.Internal.Engine.SQLDatabaseDeployment.Internal;
using Crane.Internal.Loggie;

namespace Crane.Internal.Engine
{
	public class CraneTaskManager : ICraneTaskManager
	{
		public void Execute(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> script)
		{
			var type = string.Empty;

			// get type
			if (script.TryGetValue("crane", out var scriptHeader))
			{
				type = scriptHeader["type"];

				if (string.IsNullOrEmpty(type))
				{
					throw new ArgumentNullException("type");
				}
			}
			else
			{
				throw new ArgumentException("missing crane header cfg");
			}

			if (type == "asdf")
			{
				SQLAccess sqlAccess = new();
				var executor = new SQLDatabaseDeploymentType();
				executor.Execute(logger, script, sqlAccess);
			}
		}
	}
}
