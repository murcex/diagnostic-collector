using System;

namespace Kiroku
{
    /// <summary>
    /// CLASS: Contains global Kiroku values. To be properly set by the Manager when a instance is started.
    /// </summary>
    static class LogConfiguration
    {        
        // Set Log Write Type
        public static string WriteLog;

        // Set Logging Types
        public static string WriteVerbose;
        public static string Trace;
        public static string Info;
        public static string Warning;
        public static string Error;

        // Set GUIDs
        public static Guid ApplicationID;
        public static Guid TrackID;
        public static Guid InstanceID;

        // Set LogConfiguration Values
        public const string Version = "KLOG_VERSION_20181026"; // Removed KLOG_S<#> Counter
        public static string Datetime;
        public static string RootFilePath;
        public static string FullFilePath;
    }
}

