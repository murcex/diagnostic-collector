namespace KirokuG2
{
	using KirokuG2.Internal;
	using System.Globalization;

	public class KManager
	{
		private static bool _initialized = false;

		private static KStorage _kstorage;

		private static string _source;

		/// <summary>
		/// Initialized state of Kiroku
		/// </summary>
		public static bool Initialized => _initialized;

		/// <summary>
		/// Configure Kiroku
		/// </summary>
		public static bool Configure(bool upload = false)
		{
			_source = DataProvider.GetSource();

			_kstorage = new KStorage(upload);

			Activation();

			_initialized = true;

			return true;
		}

		/// <summary>
		/// Create a new Kiroku logging instance
		/// </summary>
		public static KLog NewInstance(string function)
		{
			if (string.IsNullOrEmpty(function))
			{
				throw new ArgumentNullException(nameof(function));
			}

			if (!_initialized)
			{
				throw new Exception($"Kiroku has not been initialized");
			}

			return new KLog(_kstorage, _source, function);
		}

		/// <summary>
		/// Critical event logger, doesn't require configuring Kiroku
		/// </summary>
		public static void Critical(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				data = "Critical data IsNullOrEmpty";
			}

			var source = DataProvider.GetSource();

			var message = $"{DateTime.UtcNow.ToString("o", DateTimeFormatInfo.InvariantInfo)},C,{source}${data}";

			DataProvider.Transmission(message);
		}

		/// <summary>
		/// Upload all logs currently stored in the cache
		/// </summary>
		public static bool UploadLogs()
		{
			if (_kstorage == null)
			{
				throw new Exception($"Storage has not been initialized");
			}

			return _kstorage.UploadLogs();
		}

		/// <summary>
		/// Send the Kiroku activation signal
		/// </summary>
		private static bool Activation()
		{
			var activation_log_data = $"{DateTime.UtcNow.ToString("o", DateTimeFormatInfo.InvariantInfo)},A,{_source}";

			return DataProvider.Transmission(activation_log_data);
		}
	}
}
