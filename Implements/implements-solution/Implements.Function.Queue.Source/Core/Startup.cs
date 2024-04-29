using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;

namespace Implements.Function.Queue.Source.Core
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			Configuration.AccessToken = Environment.GetEnvironmentVariable("AccessToken");

			Configuration.ConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");

			Configuration.Endpoint = Environment.GetEnvironmentVariable("Endpoint");
		}
	}
}
