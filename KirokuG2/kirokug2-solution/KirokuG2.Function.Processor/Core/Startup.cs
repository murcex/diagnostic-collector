using Configurator;
using KirokuG2.Loader;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(KirokuG2.Processor.Core.Startup))]

namespace KirokuG2.Processor.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = ConfiguratorManager.Execute();

            var kiroku_cfg = config["kiroku-processor"];

            Configuration.Load(kiroku_cfg);

            KLoaderManager.Configuration(Configuration.Storage, Configuration.Database);

            KManager.Configure(true);
        }
    }
}
