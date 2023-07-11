using Configurator;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(KirokuG2.Service.Core.Startup))]

namespace KirokuG2.Service.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = ConfiguratorManager.Execute();

            var kiroku_cfg = config["kiroku-service"];

            Configuration.Load(kiroku_cfg);
        }
    }
}
