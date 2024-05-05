namespace KirokuG2.Internal
{
	using System.Collections.Concurrent;
	using System.Text;

	public class KStorage
	{
		private ConcurrentDictionary<string, StringBuilder> _storage;

		private bool _upload;

		private bool _active;

		private int _delay = 10000;

		public KStorage(bool upload)
		{
			_storage = new ConcurrentDictionary<string, StringBuilder>();

			_upload = upload;

			_active = false;
		}

		public bool AddLogs(StringBuilder logs)
		{
			if (_upload)
			{
				// send directly to telemetry collector
				return DataProvider.Transmission(logs.ToString());
			}
			else
			{
				// add to storage, trigger async uploader
				if (_storage.TryAdd(Guid.NewGuid().ToString(), logs))
				{
					return UploadLogs();
				}
				else
				{
					throw new Exception($"Failed to add logs to KStorage");
				}
			}
		}

		public bool UploadLogs()
		{
			if (_active)
			{
				// no action required -- wait for AsyncUpload to complete
			}
			else
			{
				_active = true;

				Task.Factory.StartNew(() => AsyncUpload(), TaskCreationOptions.LongRunning).ConfigureAwait(false);
			}

			return true;
		}

		private void SendLogs()
		{
			foreach (var log in _storage)
			{
				if (DataProvider.Transmission(log.Value.ToString()))
				{
					_storage.Remove(log.Key, out var _);
				}
			}

			_active = false;
		}

		private async Task AsyncUpload()
		{
			await Task.Delay(_delay).ContinueWith(_ => { SendLogs(); });
		}
	}
}
