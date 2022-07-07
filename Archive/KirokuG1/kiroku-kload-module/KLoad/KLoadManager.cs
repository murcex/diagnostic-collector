namespace KLoad
{
    using System;
    using System.Collections.Generic;

    public class KLoadManager
    {
        private static bool _configOnline = false;

        private static bool _appConfigStatus = false;

        private static bool _logConfigStatus = false;

        public static bool Initialize(List<KeyValuePair<string, string>> kloadConfig, List<KeyValuePair<string, string>> kirokuConfig)
        {
            // Null checks
            if (kloadConfig != null)
            {
                _appConfigStatus = true;
            }
            if (kirokuConfig != null)
            {
                _logConfigStatus = true;
            }

            // Push config packages to Configuration logic
            try
            {
                return _configOnline = Configuration.SetConfigs(kloadConfig, kirokuConfig);
            }
            catch
            {
                return _configOnline = false;
            }
        }

        public static bool Execute()
        {
            if (!_configOnline)
            {
                return false;
            }

            try
            {
                Configuration.StartLogging();

                BlobClient.Set();

                BlobFileCollector.Execute();

                BlobFileCheck.Execute();

                BlobFileUploader.Execute();

                BlobFileRetention.Execute();

                Configuration.StopLogging();

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception($"KLOAD EXCEPTION: {ex}");
            }
        }

        public static bool Retention()
        {
            return true;
        }
    }
}
