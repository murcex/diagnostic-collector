using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Configurator.Audit.Embedded.Core.Startup))]

namespace Configurator.Audit.Embedded.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = ConfiguratorManager.Execute();

            if (config.TryGetValue("embedded", out var components))
            {
                Global.TestValue = components["TestKey"];
            }
            else
            {
                throw new InvalidOperationException("embedded index not found in config payload");
            }
        }
    }
}
