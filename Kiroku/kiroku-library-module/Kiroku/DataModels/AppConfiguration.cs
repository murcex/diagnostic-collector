namespace Kiroku
{
    using System.Collections.Generic;
    using System.Linq;

    class AppConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public AppConfiguration(List<KeyValuePair<string, string>> config, string version)
        {
            _configDictionary = config.ToDictionary(x => x.Key, x => x.Value);

            Version = version;

            _configDictionary.TryGetValue(KConstants.s_Dynamic.ToLower(), out _dynamic);
            Dynamic = ConvertValueToBool(_dynamic);

            _configDictionary.TryGetValue(KConstants.s_TraceEvent.ToLower(), out _trace);
            Trace = ConvertValueToBool(_trace);

            _configDictionary.TryGetValue(KConstants.s_InfoEvent.ToLower(), out _info);
            Info = ConvertValueToBool(_info);

            _configDictionary.TryGetValue(KConstants.s_WarningEvent.ToLower(), out _warning);
            Warning = ConvertValueToBool(_warning);

            _configDictionary.TryGetValue(KConstants.s_ErrorEvent.ToLower(), out _error);
            Error = ConvertValueToBool(_error);

            _configDictionary.TryGetValue(KConstants.s_MetricEvent.ToLower(), out _metric);
            Metric = ConvertValueToBool(_metric);

            _configDictionary.TryGetValue(KConstants.s_Write.ToLower(), out _writelog);
            WriteLog = ConvertValueToBool(_writelog);

            _configDictionary.TryGetValue(KConstants.s_Verbose.ToLower(), out _writeverbose);
            WriteVerbose = ConvertValueToBool(_writeverbose);
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> _configDictionary = new Dictionary<string, string>();

        // Kiroku metadata

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApplicationId 
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationId))
                {
                    _configDictionary.TryGetValue(KConstants.s_ApplicationId.ToLower(), out _applicationId);
                }

                return _applicationId;
            }
        }
        private string _applicationId;

        /// <summary>
        /// 
        /// </summary>
        public bool Dynamic { get; set; }
        private string _dynamic;

        // Application information

        /// <summary>
        /// 
        /// </summary>
        public string TrackId
        {
            get
            {
                if (string.IsNullOrEmpty(_trackId))
                {
                    _configDictionary.TryGetValue(KConstants.s_TrackId.ToLower(), out _trackId);
                }

                return _trackId;
            }
        }
        private string _trackId;

        /// <summary>
        /// 
        /// </summary>
        public string RegionId
        {
            get
            {
                if (string.IsNullOrEmpty(_regionId))
                {
                    _configDictionary.TryGetValue(KConstants.s_RegionId.ToLower(), out _regionId);
                }

                return _regionId;
            }
        }
        private string _regionId;

        /// <summary>
        /// 
        /// </summary>
        public string ClusterId
        {
            get
            {
                if (string.IsNullOrEmpty(_clusterId))
                {
                    _configDictionary.TryGetValue(KConstants.s_ClusterId.ToLower(), out _clusterId);
                }

                return _clusterId;
            }
        }
        private string _clusterId;

        /// <summary>
        /// 
        /// </summary>
        public string DeviceId
        {
            get
            {
                if (string.IsNullOrEmpty(_deviceId))
                {
                    _configDictionary.TryGetValue(KConstants.s_DeviceId.ToLower(), out _deviceId);
                }

                return _deviceId;
            }
        }
        private string _deviceId;

        // Logging types

        /// <summary>
        /// 
        /// </summary>
        public bool Trace { get; private set; }
        private string _trace;

        /// <summary>
        /// 
        /// </summary>
        public bool Info { get; private set; }
        private string _info;

        /// <summary>
        /// 
        /// </summary>
        public bool Warning { get; private set; }
        private string _warning;

        /// <summary>
        /// 
        /// </summary>
        public bool Error { get; private set; }
        private string _error;

        /// <summary>
        /// 
        /// </summary>
        public bool Metric { get; private set; }
        private string _metric;

        // Log writers

        /// <summary>
        /// 
        /// </summary>
        public bool WriteLog { get; private set; }
        private string _writelog;

        /// <summary>
        /// 
        /// </summary>
        public bool WriteVerbose { get; private set; }
        private string _writeverbose;

        /// <summary>
        /// 
        /// </summary>
        public string FullFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_rootFilePath))
                {
                    _configDictionary.TryGetValue(KConstants.s_FilePath.ToLower(), out _rootFilePath);
                }

                return _rootFilePath + KConstants.s_WritingToLog;
            }
        }
        private string _rootFilePath;

        /// <summary>
        /// Utility to convert string to bool.
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        private static bool ConvertValueToBool(string inputValue)
        {
            bool outputValue;

            if (!string.IsNullOrEmpty(inputValue) && inputValue == "1")
            {
                outputValue = true;
            }
            else
            {
                outputValue = false;
            }

            return outputValue;
        }
    }
}
