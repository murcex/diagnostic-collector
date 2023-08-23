using System.Text;

namespace Crane.Internal.Loggie
{
	public class Logger : ICraneLogger
	{
		private string _logFile;

		private bool _write;

		private StringBuilder _storage;

		public Logger(string rootPath = null)
		{
			if (string.IsNullOrEmpty(rootPath))
			{
				_write = false;
				_storage = new();
			}
			else
			{
				Enable(rootPath);
			}
		}

		public void Info(string logInput) //, string? consoleInput = null)
		{
			var entry = $"{DateTime.Now},I,{logInput}";

			if (_write)
			{
				File.AppendAllText(_logFile, entry);
			}
			else
			{
				_storage.AppendLine(entry);
			}
		}

		public void Error(string logInput) //, string consoleInput)
		{
			var entry = $"{DateTime.Now},E,{logInput}";

			if (_write)
			{
				File.AppendAllText(_logFile, entry);
			}
			else
			{
				_storage.AppendLine(entry);
			}

		}

		public void Enable(string rootPath)
		{
			// if dir exist
			if (!Directory.Exists(rootPath))
			{
				throw new Exception($"Directory is missing: {rootPath}");
			}

			// create file name
			var logSessionName = $"{Guid.NewGuid()}-{(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"))}.txt";

			_logFile = Path.Combine(rootPath, logSessionName);

			// create file + header
			File.WriteAllText(_logFile, "New Log\r\n");

			if (_storage != null && _storage.Length > 0)
			{
				File.AppendAllText(_logFile, _storage.ToString());
			}

			_write = true;

			_storage = null;
		}

		public bool Enabled()
		{
			return _write;
		}
	}
}
