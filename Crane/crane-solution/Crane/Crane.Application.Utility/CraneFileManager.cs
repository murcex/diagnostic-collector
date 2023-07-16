using Implements.Configuration;

namespace Crane.Application.Utility
{
	public class CraneFileManager
	{
		public Dictionary<string, Dictionary<string, string>> LoadCraneConfig()
		{
			// file path
			var configFilePath = "asdf";

			// file exist
			if (!File.Exists(configFilePath))
			{
				throw new Exception();
			}

			// read all lines
			var lines = File.ReadAllLines(configFilePath).ToList();

			// de-serial lines to cfg and return
			using ConfigurationUtility configurationUtility = new();
			return configurationUtility.Deserialize(lines);
		}

		public Dictionary<string, Dictionary<string, string>> LoadCraneScript(Dictionary<string, Dictionary<string, string>> craneCfg, string script)
		{
			// file path
			var root = string.Empty;

			// file exist
			var targetScript = $"{root}\\{script}";
			if (!File.Exists(targetScript))
			{
				throw new Exception();
			}

			// read all lines
			var lines = File.ReadAllLines(targetScript).ToList();

			// de-serial lines to cfg
			using ConfigurationUtility configurationUtility = new();
			var scriptCfg = configurationUtility.Deserialize(lines);

			// check type name
			if (scriptCfg.TryGetValue("crane", out var scriptHeader))
			{
				var type = scriptHeader["type"];

				if (string.IsNullOrEmpty(type))
				{
					throw new ArgumentNullException("type");
				}
			}
			else
			{
				throw new ArgumentException("missing crane header cfg");
			}

			return scriptCfg;
		}
	}
}
