namespace KFlow
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Collections.Generic;

    // Implements Utility Library
    using Implements;


    public static class Global
    {

        public static List<KeyValuePair<string, string>> GetConfigValues(string _tag)
        {
            List<KeyValuePair<string, string>> _tagList;

            using (Deserializer deserilaizer = new Deserializer())
            {
                var _path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "Config.ini");

                Log.Initialize();

                deserilaizer.Execute(@"E:\02_CLOUD\GitHub\PlatformDiagnosticCollector\Kiroku\kiroku-library\ExampleConsole\Config.ini", true, true);

                var test = deserilaizer.GetCollection();

                _tagList = deserilaizer.GetTag(_tag);
            }

            return _tagList;
        }

        // Private
        // Main
        public static readonly string _instanceloop = ConfigurationManager.AppSettings["instanceloop"].ToString();
        public static readonly string _blockloop = ConfigurationManager.AppSettings["blockloop"].ToString();

        // Trace
        public static readonly string _trace = ConfigurationManager.AppSettings["trace"].ToString();
        public static readonly string _traceloop = ConfigurationManager.AppSettings["traceloop"].ToString();
        public static readonly string _tracechar = ConfigurationManager.AppSettings["tracechar"].ToString();

        // Info
        public static readonly string _info = ConfigurationManager.AppSettings["info"].ToString();
        public static readonly string _infoloop = ConfigurationManager.AppSettings["infoloop"].ToString();
        public static readonly string _infochar = ConfigurationManager.AppSettings["infochar"].ToString();

        // Warning
        public static readonly string _warning = ConfigurationManager.AppSettings["warning"].ToString();
        public static readonly string _warningloop = ConfigurationManager.AppSettings["warningloop"].ToString();
        public static readonly string _warningchar = ConfigurationManager.AppSettings["warningchar"].ToString();

        // Error
        public static readonly string _error = ConfigurationManager.AppSettings["error"].ToString();
        public static readonly string _errorloop = ConfigurationManager.AppSettings["errorloop"].ToString();
        public static readonly string _errorchar = ConfigurationManager.AppSettings["errorchar"].ToString();

        // Public
        // Main
        public static int InstanceLoop = ConvertValueToInt(_instanceloop);
        public static int BlockLoop = ConvertValueToInt(_blockloop);

        // Trace
        public static bool TraceOn;
        public static int TraceLoopCount;
        public static int TraceCharCount;

        // Info
        public static bool InfoOn;
        public static int InfoLoopCount;
        public static int InfoCharCount;

        // Warning
        public static bool WarningOn;
        public static int WarningLoopCount;
        public static int WarningCharCount;

        // Error
        public static bool ErrorOn;
        public static int ErrorLoopCount;
        public static int ErrorCharCount;

        public static void SetValues()
        {
            InstanceLoop = ConvertValueToInt(_instanceloop);
            BlockLoop = ConvertValueToInt(_blockloop);

            TraceOn = ConvertValueToBool(_trace);
            TraceLoopCount = ConvertValueToInt(_traceloop);
            TraceCharCount = ConvertValueToInt(_tracechar);

            InfoOn = ConvertValueToBool(_info);
            InfoLoopCount = ConvertValueToInt(_infoloop);
            InfoCharCount = ConvertValueToInt(_infochar);

            WarningOn = ConvertValueToBool(_warning);
            WarningLoopCount = ConvertValueToInt(_warningloop);
            WarningCharCount = ConvertValueToInt(_warningchar);

            ErrorOn = ConvertValueToBool(_error);
            ErrorLoopCount = ConvertValueToInt(_errorloop);
            ErrorCharCount = ConvertValueToInt(_errorchar);
        }

        private static int ConvertValueToInt(string inputValue)
        {
            int outputValue;

            try
            {
                outputValue = Int32.Parse(inputValue);
            }
            catch
            {
                outputValue = 0;
            }

            return outputValue;
        }

        private static bool ConvertValueToBool(string inputValue)
        {
            bool outputValue;

            if (inputValue == "1")
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
