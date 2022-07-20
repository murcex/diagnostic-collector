using Configurator;
using Implements;
using KirokuG2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor.Function.Core
{
    public class Setup
    {
        private static bool _initialized = false;

        public static void Execute()
        {
            if (!_initialized)
            {
                var file = IsAzureFunction();

                var cfg = CfgManager.GetCfg();

                if (CfgManager.CheckCfg(cfg, out string erroMsg))
                {
                    using (Deserializer deseralizer = new Deserializer())
                    {
                        deseralizer.Execute(file);

                        var kiroku_cfg = deseralizer.GetTag("sensor");

                        SensorManager.Initialize(kiroku_cfg);

                        KManager.Configure(true);

                        _initialized = true;
                    }
                }
                else
                {
                    throw new Exception(erroMsg);
                }
            }

            _initialized = true;
        }

        private static string IsAzureFunction()
        {
            var check = Environment.GetEnvironmentVariable("FUNCTIONS_EXTENSION_VERSION", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(check))
            {
                return Directory.GetCurrentDirectory() + @"\Config.ini";
            }
            else
            {
                return @"D:\home\data\app\cfg\Config.ini";
            }
        }
    }
}
