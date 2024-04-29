using System;
using System.Collections.Generic;

namespace PlyQor.Injektr.Core
{
	class Configuration
	{
		private static string _url;

		private static string _container;

		private static string _token;

		public static string Url => _url;

		public static string Container => _container;

		public static string Token => _token;

		public static void Load(Dictionary<string, Dictionary<string, string>> cfg)
		{
			if (cfg.TryGetValue("worker", out Dictionary<string, string> worker_cfg))
			{
				foreach (var kv in worker_cfg)
				{
					_ = kv.Key switch
					{
						"url" => _url = kv.Value,
						"container" => _container = kv.Value,
						"token" => _token = kv.Value,
						_ => throw new Exception("Switch Failure"),
					};
				}
			}
			else
			{
				throw new Exception("worker not found in config");
			}
		}
	}
}
