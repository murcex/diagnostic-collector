using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

[assembly: FunctionsStartup(typeof(Implements.Function.Queue.Target.Core.Startup))]

namespace Implements.Function.Queue.Target.Core
{
	internal class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			//var config = ConfiguratorManager.Execute();

			Dictionary<string, string> config = new()
			{
				{ "DATABASE", Environment.GetEnvironmentVariable("Database") },
				{ "TOKEN", Environment.GetEnvironmentVariable("Token") }
			};

			Configuration.Load(config);

			//KManager.Configure(true);
		}
	}
}
