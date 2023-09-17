using Implements.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace Crane.Internal.Test.Core
{
    public class Setup
    {
        // get config.ini
        private static string root;

        public static string CraneTestRoot => root;

        private static List<string> core = new()
        {
            "external",
            "failure",
            "local",
            "types"
        };

        private static List<string> types = new()
        {
            "sqldatabasedeployment"
        };

        // check dirs
        public static void Basic()
        {
            if (string.IsNullOrWhiteSpace(root))
            {
                root = LoadTestConfig();
            }

            if (!Directory.Exists(root))
            {
                throw new Exception($"root directory doesn't exist");
            }

            foreach (string dir in core)
            {
                var checkDir = Path.Combine(root, dir);

                if (!Directory.Exists(checkDir))
                {
                    Directory.CreateDirectory(checkDir);
                }
            }

            foreach (string dir in types)
            {
                var checkDir = Path.Combine(root, "types", dir);

                if (!Directory.Exists(checkDir))
                {
                    Directory.CreateDirectory(checkDir);
                }
            }
        }

        private static string LoadTestConfig()
        {
			// local config.ini
			var configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config.ini");

			// file exist
			if (!File.Exists(configFilePath))
			{
                throw new Exception($"crane test Config.ini not found");
			}

			// read all lines
			var lines = File.ReadAllLines(configFilePath).ToList();

			// de-serial lines to cfg and return
			using ConfigurationUtility configurationUtility = new();
			var config = configurationUtility.Deserialize(lines);

            if (config == null || config.Count == 0)
            {
				throw new Exception($"crane test config dictionary is null or empty");
			}

            if (config.TryGetValue("crane-test", out var craneTestCfg))
            {
                if(craneTestCfg.TryGetValue("root", out var rootDir))
                {
                    if (string.IsNullOrEmpty(rootDir))
                    {
                        throw new Exception($"crane test config root value is null or empty");
                    }

                    return rootDir;
                }
                else
                {
					throw new Exception($"crane test config doesn't contain root key");
				}
            }
            else
            {
                throw new Exception($"crane test config doesn't contain [crane-test] group");
            }
		}
    }
}
