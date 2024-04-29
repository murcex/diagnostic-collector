using Implements.Module.Queue;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Implements.Function.Queue.Target.Core.Startup))]

namespace Implements.Function.Queue.Target.Core
{
	internal class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			Configuration.Queue = new QueueManager(10, 10000, null);

			Configuration.AccessToken = Environment.GetEnvironmentVariable("AccessToken");

			Configuration.ConnectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
		}
	}
}
