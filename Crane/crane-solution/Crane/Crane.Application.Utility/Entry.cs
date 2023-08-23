using Crane.Application.Utility.Components;
using Crane.Application.Utility.Interface;
using Crane.Application.Utility.Model;
using Crane.Internal.Engine;
using Crane.Internal.Loggie;

namespace Crane.Application.Utility
{
	public class Entry
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			ICraneLogger logger = new Logger();
			ICraneFileManager fileManager = new CraneFileManager();
			ICraneTaskManager taskManager = new CraneTaskManager();

			try
			{
				var script = string.Empty;

				if (args.Length > 0)
				{
					script = args[0];

					if (string.IsNullOrEmpty(script))
					{
						//throw
						logger.Error($"");
						throw new CraneException();
					}
				}
				else
				{
					//throw
					logger.Error($"");
					throw new CraneException();
				}

				// load Config.ini
				var craneCfg = fileManager.LoadCraneConfig(logger);

				// setup logger
				var loggerFilePath = fileManager.GetCraneLoggerFilePath(logger, craneCfg);

				logger.Enable(loggerFilePath);

				// read task cfg
				var scriptCfg = fileManager.LoadCraneTask(logger, craneCfg, script);

				// switch type
				taskManager.Execute(logger, scriptCfg);
			}
			catch (Exception ex)
			{
				if (!logger.Enabled())
				{
					logger.Enable(Directory.GetCurrentDirectory());
				}

				if (ex is not CraneException)
				{
					logger.Error(ex.ToString());
				}

				Environment.Exit(0);
			}
		}
	}
}