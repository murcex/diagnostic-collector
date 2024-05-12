using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

[assembly: FunctionsStartup(typeof(Implements.Function.Queue.Source.Core.Startup))]

namespace Implements.Function.Queue.Source.Core
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			//var config = ConfiguratorManager.Execute();

			Dictionary<string, string> config = new()
			{
				{ "DATABASE", Environment.GetEnvironmentVariable("Database") },
				{ "TOKEN", Environment.GetEnvironmentVariable("Token") },
				{ "ENDPOINT", Environment.GetEnvironmentVariable("Endpoint") },
			};

			Configuration.Load(config);

			//KManager.Configure(true);
		}
	}
}
