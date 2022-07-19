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

        public bool AddLogs(string instance, StringBuilder logs)
        {
            if (_storage.TryAdd(instance, logs))
            {
                return UploadLogs();
            }
            else
            {
                throw new Exception($"Failed to add logs to KStorage");
            }
        }

        public bool UploadLogs()
        {
            if (_upload)
            {
                SendLogs();
            }
            else
            {
                if (_active)
                {
                    // no action required -- wait for AsyncUpload to trigger
                }
                else
                {
                    Task.Factory.StartNew(() => AsyncUpload(), TaskCreationOptions.LongRunning).ConfigureAwait(false);
                }
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
        }

        private async Task AsyncUpload()
        {
            _active = true;

            await Task.Delay(_delay).ContinueWith(_ => { SendLogs(); _active = false; });
        }
    }
}
