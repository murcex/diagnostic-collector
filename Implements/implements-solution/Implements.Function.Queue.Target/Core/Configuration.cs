using Implements.Module.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Function.Queue.Target.Core
{
	public class Configuration
	{
		public static QueueManager Queue { get; set; }

		public static string ConnectionString { get; set; }

		public static string AccessToken { get; set; }
	}
}
