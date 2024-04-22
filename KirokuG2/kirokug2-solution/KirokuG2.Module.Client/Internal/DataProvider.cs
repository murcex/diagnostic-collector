namespace KirokuG2.Internal
{
	public class DataProvider
	{
		private static string _url;

		private static string _source;

		public static string GetSource()
		{
			if (!string.IsNullOrEmpty(_source))
			{
				return _source;
			}

			try
			{
				_source = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");

				if (string.IsNullOrEmpty(_source))
				{
					throw new Exception("WEBSITE_SITE_NAME is NullOrEmpty");
				}

				return _source;
			}
			catch
			{
				throw new Exception("WEBSITE_SITE_NAME Not Found");
			}
		}

		private static string GetUrl()
		{
			try
			{
				var kiroku_cfg = Environment.GetEnvironmentVariable("KIROKU_CFG");

				if (string.IsNullOrEmpty(kiroku_cfg))
				{
					throw new Exception("KIROKU_CFG is NullOrEmpty");
				}
				else
				{
					var components = kiroku_cfg.Split(',');
					var target = components[0];
					var token = components[1];

					var url = $"{target}/api/Collector?token={token}";

					return url;
				}
			}
			catch
			{
				throw new Exception("KIROKU_CFG Not Found");
			}
		}

		public static bool Transmission(string data)
		{
			if (string.IsNullOrEmpty(_url))
			{
				_url = GetUrl();
			}

			return Transmitter.Execute(_url, data);
		}
	}
}
