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

		public CraneApplication(ICraneLogger logger, ICraneConsole console, ICraneFileManager fileManger, ICraneTaskManager taskManager)
		{
			this.logger = logger;
			this.fileManager = fileManger;
			this.taskManager = taskManager;
			this.craneConsole = console;
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

				// console conformation
				if (fileManager.CheckForConformation(logger, craneCfg))
				{
					craneConsole.GeneralConformation(logger);
				}

				// switch on type and execute task
				var taskResult = taskManager.Execute(logger, craneConsole, taskCfg);

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
			catch (CraneException craneEx)
			{
				if (!logger.Enabled())
				{
					logger.Enable(Directory.GetCurrentDirectory());
				}

                logger.Error($"general_exception={craneEx}");

                craneConsole.Close();
			}
			catch (Exception ex)
			{
				if (!logger.Enabled())
				{
					logger.Enable(Directory.GetCurrentDirectory());
				}

				logger.Error($"general_exception={ex}");

				craneConsole.Close();
			}
		}
	}
}
