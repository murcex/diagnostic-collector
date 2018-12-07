namespace Implements
{
    using System;
    using System.IO;

    public static class Log
    {
        /// <summary>
        /// The full log file name, prefix and datetime stamp.
        /// </summary>
        private static string LogFileName;

        /// <summary>
        /// The full path loction with log file name.
        /// </summary>
        private static string FullLogPath;

        /// <summary>
        /// The log file prefix name.
        /// </summary>
        private static string AppLogName;

        /// <summary>
        /// The delimiter used to separate log data columns.
        /// </summary>
        private static string Delimiter;

        /// <summary>
        /// Flag to write data to the console.
        /// </summary>
        private static bool WriteToConsole;

        /// <summary>
        /// Flag to write data to the log.
        /// </summary>
        private static bool WriteToLog;

        /// <summary>
        /// Current initialized state of the log. (internal)
        /// </summary>
        private static bool Initialized = false;

        /// <summary>
        /// Type Info constant. 
        /// </summary>
        private static string TypeInfo = "Info";

        /// <summary>
        /// Type Error constant.
        /// </summary>
        private static string TypeError = "Error";

        /// <summary>
        /// Current initialized state of the log.
        /// </summary>
        public static bool Status { get { return Initialized; } }

        /// <summary>
        /// Initializes the log for function. Creates the log file with a first entry.
        /// </summary>
        /// <param name="cfg"></param>
        public static void Initialize(LogConfiguration cfg = null)
        {
            if (cfg == null)
            {
                cfg = new LogConfiguration();
            }

            NewInstance(cfg);

            try
            {
                using (StreamWriter file = new StreamWriter(FullLogPath, true))
                {
                    file.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{Delimiter}{TypeInfo}{Delimiter}{AppLogName} Initialized");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Log Exception [Log].[Initialize()]: {e.ToString()}");
            }
        }

        /// <summary>
        /// Apply Type Info data to the log.
        /// </summary>
        /// <param name="logData"></param>
        public static void Info(string logData)
        {
            if (Initialized)
            {
                AddLogEntry(TypeInfo, logData);
            }
            else
            {
                throw new Exception($"Log Exception [Log].[Info()]: Logger is not initialized.");
            }
        }

        /// <summary>
        /// Apply Type Error data to the log.
        /// </summary>
        /// <param name="logData"></param>
        public static void Error(string logData)
        {
            if (Initialized)
            {
                AddLogEntry(TypeError, logData);
            }
            else
            {
                throw new Exception($"Log Exception [Log].[Error()]: Logger is not initialized.");
            }
        }

        /// <summary>
        /// Set interal values and creates the full log file path used for the first write.
        /// </summary>
        /// <param name="cfg"></param>
        private static void NewInstance(LogConfiguration cfg)
        {
            try
            {
                LogFileName = string.Empty;
                FullLogPath = string.Empty;
                Delimiter = string.Empty;
                AppLogName = string.Empty;

                Delimiter = cfg.Delimiter;
                AppLogName = cfg.LogName;
                WriteToConsole = cfg.WriteToConsole;
                WriteToLog = cfg.WriteToLog;

                LogFileName = $"{AppLogName}-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.txt";

                if (cfg.Directory == "default")
                {
                    FullLogPath = Directory.GetCurrentDirectory() + @"\" + LogFileName;
                }
                else
                {
                    FullLogPath = cfg.Directory + @"\" + LogFileName;

                    if (!Directory.Exists(FullLogPath))
                    {
                        throw new Exception($"Log Exception [Log].[NewInstance()]: Directory doesn't exist! Directory = {cfg.Directory}");
                    }
                }

                Initialized = true;
            }
            catch (Exception e)
            {
                throw new Exception($"Log Exception [Log].[SetLogFile()]: {e.ToString()}");
            }
        }

        /// <summary>
        /// Writes data to the console and write data to log.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entry"></param>
        private static void AddLogEntry(string type, string entry)
        {
            var transferCase = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}{Delimiter}{type}{Delimiter}{entry}";

            try
            {
                if (WriteToConsole)
                {
                    Console.WriteLine(entry);
                }

                if (WriteToLog)
                {
                    using (StreamWriter file = File.AppendText(FullLogPath))
                    {
                        file.WriteLine(transferCase);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Log Exception [Log].[AddLogEntry()]: {e.ToString()}");
            }
        }
    }
}
