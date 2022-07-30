using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Crane
{
    /// <summary>
    /// 
    /// </summary>
    static class Log
    {
        private static string LogFileName;
        private static string FullLogPath;

        public static string GetLogFileName()
        {
            return LogFileName;
        }

        private static void SetLogFile()
        {
            LogFileName = string.Empty;
            FullLogPath = string.Empty;

            LogFileName = "CraneLog-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
            FullLogPath = Directory.GetCurrentDirectory() + @"\" + LogFileName;
        }

        public static void Info(string logData)
        {
            // Add formatting
            AppendLogEntry(logData);
        }

        public static void Error(string logData)
        {
            // Add formatting
            AppendLogEntry(logData);
        }

        public static void CreateLog()
        {
            SetLogFile();

            using (StreamWriter file = new StreamWriter(FullLogPath, true))
            {
                file.WriteLine("");
            }
        }

        public static void AppendLogEntry(String entry)
        {
            using (StreamWriter file = File.AppendText(FullLogPath))
            {
                file.WriteLine(entry);
            }
        }
    }
}
