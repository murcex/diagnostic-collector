using System;
using System.Collections.Generic;
using System.Threading;

namespace Implements.Function.Queue.Target.Components
{
	public class QueueProcessor
	{
		public static void Execute(List<object> items)
		{
			foreach (var item in items)
			{
				try
				{
					var id = item.ToString();

					var result = SQLStorage.UpdateRecord(id);

					Console.WriteLine($"Updating {id} => {result}");

					Thread.Sleep(1000);
				}
				catch { }
			}
		}

		public static void Logger(string message)
		{
			Console.WriteLine($"LOGGER: {message}");
		}
	}
}
