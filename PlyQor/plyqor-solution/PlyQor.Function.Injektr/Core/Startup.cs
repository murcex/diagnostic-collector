using Configurator;
using KirokuG2;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using PlyQor.Injektr.Core;

[assembly: FunctionsStartup(typeof(Startup))]

namespace PlyQor.Injektr.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = ConfiguratorManager.Execute();

            Configuration.Load(config);

            KManager.Configure(true);
        }
    }
}
