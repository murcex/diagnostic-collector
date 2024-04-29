using Configurator;
//using KirokuG2;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using PlyQor.Engine;

[assembly: FunctionsStartup(typeof(PlyQor.Storage.Core.Startup))]

namespace PlyQor.Storage.Core
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var config = ConfiguratorManager.Execute();

			PlyQorManager.Initialize(config);

			//KManager.Configure(true);
		}
	}
}
