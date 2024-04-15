using Configurator;
using KirokuG2.Internal.Loader.Components;
using KirokuG2.Loader;
using KirokuG2.Loader.Components;
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

            LogProvider logProvider = new(Configuration.Storage, new KLogSeralializer());

            SQLProvider sqlProvider = new();
            sqlProvider.Initialized(Configuration.Database);

            KLoaderManager.Configuration(logProvider, sqlProvider);

            KManager.Configure(true);
        }
    }
}
