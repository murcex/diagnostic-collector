namespace Implements
{
    public class LogConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public string LogName { get; set; }

        /// <summary>
        /// The log file prefix name.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// The delimiter used to separate log data columns.
        /// </summary>
        public string Delimiter { get; set; }

        /// <summary>
        /// Flag to write data to the console.
        /// </summary>
        public bool WriteToConsole { get; set; }

        /// <summary>
        /// Flag to write data to the log.
        /// </summary>
        public bool WriteToLog { get; set; }

        /// <summary>
        /// Log Constructor - sets default values.
        /// </summary>
        public LogConfiguration()
        {
            LogName = "AppLog";
            Directory = "default";
            Delimiter = ",";
            WriteToConsole = true;
            WriteToLog = true;
        }
    }
}
