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
            using (StreamWriter file = new StreamWriter(Global.FullLogPath, true))
            {
                file.WriteLine("");
            }
        }

        public static void AppendLogEntry(String entry)
        {
            using (StreamWriter file = File.AppendText(Global.FullLogPath))
            {
                file.WriteLine(entry);
            }
        }
    }
}
