using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Configurator.Audit.Memory.Core.Startup))]

namespace Configurator.Audit.Memory.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = ConfiguratorManager.Execute();

            if (config.TryGetValue("default", out var components))
            {
                if (components.TryGetValue("TestKey", out var test))
                {
                    Global.TestValue = test;
                }
            }
        }
    }
}
