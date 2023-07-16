using Crane.Internal.Engine.SQLDatabaseDeployment;

namespace Crane.Application.Utility
{
	public class ScriptExecutor
	{
		public void Execute(Dictionary<string, Dictionary<string, string>> script)
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

			// switch
			//type.ToUpper() switch
			//{
			//	"A" => new SQLDatabaseDeploymentType().Execute(script),
			//	_ => throw new Exception($"{type} no hit")
			//};
		}

	}
}
