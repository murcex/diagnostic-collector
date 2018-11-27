namespace KLOGLoader
{
    using System;
    using System.Configuration;
    using System.Collections.Specialized;

    // Kiroku
    using Kiroku;

    class Global
    {
        public static readonly String Debug = ConfigurationManager.AppSettings["debug"].ToString();
        public static readonly String AzureContainer = ConfigurationManager.AppSettings["azureContainer"].ToString();
        public static readonly String SqlConnetionString = ConfigurationManager.AppSettings["sql"].ToString();
        static string _retentionDays = ConfigurationManager.AppSettings["retentionDays"].ToString();
        static string _messageLength = ConfigurationManager.AppSettings["messageLength"].ToString();
        public static readonly Double RetentionDays = Convert.ToDouble(_retentionDays);
        public static readonly int MessageLength = Convert.ToInt32(_messageLength);

        static string _instance = ConfigurationManager.AppSettings["instance"].ToString();
        static string _block = ConfigurationManager.AppSettings["block"].ToString();
        static string _trace = ConfigurationManager.AppSettings["trace"].ToString();
        static string _info = ConfigurationManager.AppSettings["info"].ToString();
        static string _warning = ConfigurationManager.AppSettings["warning"].ToString();
        static string _error = ConfigurationManager.AppSettings["error"].ToString();

        public static bool Instance;
        public static bool Block;
        public static bool Trace;
        public static bool Info;
        public static bool Warning;
        public static bool Error;

        public static void CheckDebug()
        {
            if (Debug == "1")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tDEBUG DETECTED, PRESS ANY KEY");
                Console.ReadKey();
            }
        }

        public static void StartLogging()
        {
            KManager.Online((NameValueCollection)ConfigurationManager.GetSection("Kiroku"));
        }

        public static void StopLogging()
        {
            KManager.Offline();
        }

        public static void SetLoadValues()
        {
            Instance = ConvertStringToBool(_instance);
            Block = ConvertStringToBool(_block);
            Trace = ConvertStringToBool(_trace);
            Info = ConvertStringToBool(_info);
            Warning = ConvertStringToBool(_warning);
            Error = ConvertStringToBool(_error);
        }

        private static bool ConvertStringToBool(string input)
        {
            if (input == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
