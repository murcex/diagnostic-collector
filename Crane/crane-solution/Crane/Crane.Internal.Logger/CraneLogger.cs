namespace Crane.Internal.CraneLog
{
	public class CraneLogger : ICraneLogger
	{
		private string _logFile;

		private bool _write;

		private List<string> _storage;

		public CraneLogger(string rootPath = null)
		{
			if (string.IsNullOrEmpty(rootPath))
			{
				this._write = false;
				this._storage = new();
			}
			else
			{
				Enable(rootPath);
			}
		}

		public void Info(string logInput)
		{
			var entry = $"{DateTime.Now},I,{logInput}";

			if (_write)
			{
				File.AppendAllText(_logFile, entry + "\r\n");
			}
			else
			{
				_storage.Add(entry);
			}
		}

		public void Success(string logInput)
		{
			var entry = $"{DateTime.Now},I,{logInput}";

			if (_write)
			{
				File.AppendAllText(_logFile, entry + "\r\n");
			}
			else
			{
				_storage.Add(entry);
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(logInput);
			Console.ResetColor();
		}

		public void Error(string logInput)
		{
			var entry = $"{DateTime.Now},E,{logInput}";

			if (_write)
			{
				File.AppendAllText(_logFile, entry + "\r\n");
			}
			else
			{
				_storage.Add(entry);
			}

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(logInput);
			Console.ResetColor();
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
			File.AppendAllText(_logFile, $"Starting Log\r\n");

			if (_storage != null && _storage.Count > 0)
			{
				foreach (var entry in _storage)
				{
					File.AppendAllText(_logFile, entry + "\r\n");
				}
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
