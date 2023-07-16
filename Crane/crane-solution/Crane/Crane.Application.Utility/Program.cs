namespace Crane.Application.Utility
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			CraneFileManager fileManager = new CraneFileManager();
			ScriptExecutor executor = new ScriptExecutor();

			var script = string.Empty;

			// load Config.ini
			var craneCfg = fileManager.LoadCraneConfig();

			// read script.ini
			var scriptCfg = fileManager.LoadCraneScript(craneCfg, script);

			// switch type
			executor.Execute(scriptCfg);

			// execute cfg with type
		}
	}
}