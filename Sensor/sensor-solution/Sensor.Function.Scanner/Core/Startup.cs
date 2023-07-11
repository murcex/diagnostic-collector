using Configurator;
using KirokuG2;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Sensor.Function.Core.Startup))]

namespace Sensor.Function.Core
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = ConfiguratorManager.Execute();

            SensorManager.Initialize(config);

            KManager.Configure(true);
        }
    }
}
