using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(KirokuG2.Injektr.Core.Startup))]

namespace KirokuG2.Injektr.Core
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			KManager.Configure(false);
		}
	}
}
