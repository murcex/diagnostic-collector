using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Implements.Function.Queue.Target.Core.Startup))]

namespace Implements.Function.Queue.Target.Core
{
	internal class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			// 

			throw new NotImplementedException();
		}
	}
}
