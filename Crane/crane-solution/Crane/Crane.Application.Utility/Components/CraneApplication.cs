using Crane.Application.Utility.Interface;
using Crane.Application.Utility.Model;
using Crane.Internal.Engine;
using Crane.Internal.Loggie;

namespace Crane.Application.Utility.Components
{
	public class CraneApplication
	{

		ICraneLogger logger;
		ICraneFileManager fileManager;
		ICraneTaskManager taskManager;

		public void Configure(ICraneLogger logger, ICraneFileManager fileManger, ICraneTaskManager taskManager)
		{
			this.logger = logger;
			this.fileManager = fileManger;
			this.taskManager = taskManager;
		}

		public void Execute(string[] args)
		{
			// add check for internal resources

			try
			{
				var taskFile = string.Empty;
				if (args.Length > 0)
				{
					taskFile = args[0];

					if (string.IsNullOrEmpty(taskFile))
					{
						logger.Error($"");
						throw new CraneException();
					}
				}
				else
				{
					logger.Error($"");
					throw new CraneException();
				}

				// load Config.ini
				var craneCfg = fileManager.LoadCraneConfig(logger);

				// setup logger
				var loggerFilePath = fileManager.GetCraneLoggerFilePath(logger, craneCfg);

				logger.Enable(loggerFilePath);

				// read task cfg
				var taskCfg = fileManager.LoadCraneTask(logger, craneCfg, taskFile);

				// switch type
				taskManager.Execute(logger, taskCfg);
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
