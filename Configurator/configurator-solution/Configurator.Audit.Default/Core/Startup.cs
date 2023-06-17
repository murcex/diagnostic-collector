using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.IO;

[assembly: FunctionsStartup(typeof(Configurator.Audit.Default.Core.Startup))]

namespace Configurator.Audit.Default.Core
{
    public class Startup : FunctionsStartup
    {
        private const string _configPath = @"D:\home\data\app\cfg\Config.ini";

        public override void Configure(IFunctionsHostBuilder builder)
        {

            // pre-check config
            if (File.Exists(_configPath))
            {
                File.Delete(_configPath);
            }

            if (Directory.Exists(@"D:\home\data\app"))
            {
                Directory.Delete(@"D:\home\data\app", true);
            }

            // execute configurator
            var config = ConfiguratorManager.Execute();

            if (config.TryGetValue("default", out var components))
            {
                if (components.TryGetValue("TestKey", out var test))
                {
                    Global.TestValue = test;
                }
            }

            // post-check config
            Global.ConfigExists = File.Exists(_configPath);
        }
    }
}
