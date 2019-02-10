using System;
using System.IO;

namespace Kiroku
{
    //static class Log
    //{
    //    private static string LogFileName;
    //    private static string FullLogPath;
    //    private static bool EventCounter = false;

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetLogFileName()
    //    {
    //        return LogFileName;
    //    }

    //    private static void SetLogFile()
    //    {
    //        LogFileName = string.Empty;
    //        FullLogPath = string.Empty;

    //        LogFileName = "KLOG-CriticalLog-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
    //        FullLogPath = Directory.GetCurrentDirectory() + @"\" + LogFileName;
    //    }

    //    public static void CriticalError(string logData)
    //    {
    //        if (EventCounter)
    //        {
    //            AppendLogEntry(logData);
    //        }
    //        else
    //        {
    //            CreateLog(logData);
    //        }
    //    }

    //    private static void CreateLog(string logData)
    //    {
    //        SetLogFile();

    //        try
    //        {
    //            using (StreamWriter file = new StreamWriter(FullLogPath, true))
    //            {
    //                file.WriteLine(logData);
    //            }

    //            EventCounter = true;
    //        }
    //        catch (Exception ex)
    //        {
    //            // swallow exception
    //        }
    //    }

    //    private static void AppendLogEntry(string logData)
    //    {
    //        try
    //        {
    //            using (StreamWriter file = File.AppendText(FullLogPath))
    //            {
    //                file.WriteLine(logData);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // swallow exception
    //        }
    //    }
    //}
}
