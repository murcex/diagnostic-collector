using Crane.Application.Utility.Interface;
using Crane.Application.Utility.Model;
using Crane.Internal.Loggie;
using Implements.Configuration;

namespace Crane.Application.Utility.Components
{
	public class CraneFileManager : ICraneFileManager
	{
		public Dictionary<string, Dictionary<string, string>> LoadCraneConfig(ICraneLogger logger)
		{
			// local config.ini
			var configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config.ini");

			// file exist
			if (!File.Exists(configFilePath))
			{
				logger.Error($"ERROR: Config.ini is missing.");
				throw new CraneException();
			}

			// read all lines
			var lines = File.ReadAllLines(configFilePath).ToList();

			// de-serial lines to cfg and return
			using ConfigurationUtility configurationUtility = new();
			return configurationUtility.Deserialize(lines);
		}

		public string GetCraneLoggerFilePath(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> cfg)
		{
			var output = GetCraneConfigurationValue(logger, cfg, "log");

			if (!output.result || string.IsNullOrEmpty(output.craneValue))
			{
				return Directory.GetCurrentDirectory();
			}
			else
			{
				return output.craneValue;
			}
		}

		public Dictionary<string, Dictionary<string, string>> LoadCraneTask(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> cfg, string name)
		{
			// file path
			var output = GetCraneConfigurationValue(logger, cfg, "storage");

			var root = Directory.GetCurrentDirectory();
			if (output.result || !string.IsNullOrEmpty(output.craneValue))
			{
				root = output.craneValue;
			}

			// file exist
			var taskFilePath = Path.Combine(root, name);

			if (!File.Exists(taskFilePath))
			{
				logger.Error($"");
				throw new CraneException();
			}

			// read all lines
			var lines = File.ReadAllLines(taskFilePath).ToList();

			// de-serial lines to cfg
			using ConfigurationUtility configurationUtility = new();
			var taskCfg = configurationUtility.Deserialize(lines);

			// check type name
			if (taskCfg.TryGetValue("crane", out var taskHeader))
			{
				var taskType = taskHeader["type"];

				if (string.IsNullOrEmpty(taskType))
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

			return taskCfg;
		}

		private (bool result, string craneValue) GetCraneConfigurationValue(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> cfg, string craneKey)
		{
			var result = false;
			var craneValue = string.Empty;
			if (cfg.TryGetValue("crane", out var craneCfg))
			{
				if (craneCfg == null)
				{
					logger.Error($"");
					throw new CraneException();
				}

				result = craneCfg.TryGetValue(craneKey, out craneValue);
			}

			return (result, craneValue);
		}
	}
}
