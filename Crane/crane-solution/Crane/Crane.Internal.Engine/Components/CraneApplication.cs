using Crane.Internal.Engine.Interface;
using Crane.Internal.Engine.Model;

namespace Crane.Internal.Engine.Components
{
	public class CraneApplication
	{
		readonly ICraneLogger logger;
		readonly ICraneFileManager fileManager;
		readonly ICraneTaskManager taskManager;
		readonly ICraneConsole craneConsole;

		public CraneApplication(ICraneLogger logger, ICraneConsole craneConsole, ICraneFileManager fileManger, ICraneTaskManager taskManager)
		{
			this.logger = logger;
			this.fileManager = fileManger;
			this.taskManager = taskManager;
			this.craneConsole = craneConsole;
		}

		public void Execute(string[] args)
		{
			try
			{
				craneConsole.Starter();

				var taskFile = string.Empty;
				if (args.Length > 0)
				{
					taskFile = args[0];

					if (string.IsNullOrEmpty(taskFile))
					{
						logger.Error($"crane_error=task_arg_empty");
						throw new CraneException();
					}
				}
				else
				{
					logger.Error($"crane_error=arg_count_zero");
					throw new CraneException();
				}

				// load Config.ini
				var craneCfg = fileManager.LoadCraneConfig(logger);

				// setup logger
				var loggerFilePath = fileManager.GetCraneLoggerFilePath(logger, craneCfg);

				logger.Enable(loggerFilePath);

				// read task cfg
				var taskCfg = fileManager.LoadCraneTask(logger, craneCfg, taskFile);

				// conformation
				if (fileManager.CheckForConformation(logger, craneCfg))
				{
					craneConsole.Conformation(logger);
				}

				// switch type
				var taskResult = taskManager.Execute(logger, taskCfg);

				if (taskResult.result)
				{
					logger.Success($"crane_task_complete={taskResult.message}");
				}
				else
				{
					logger.Error($"crane_task_complete={taskResult.message}");
				}

				craneConsole.Close();
			}
			catch (Exception ex)
			{
				if (!logger.Enabled())
				{
					logger.Enable(Directory.GetCurrentDirectory());
				}

				if (ex is not CraneException || ex is not CraneTaskException)
				{
					logger.Error($"general_exception={ex}");
				}

				craneConsole.Close();
			}
		}
	}
}
