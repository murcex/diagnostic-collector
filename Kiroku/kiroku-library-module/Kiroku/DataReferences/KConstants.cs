namespace Kiroku
{
    /// <summary>
    /// CLASS: Contains global constant Kiroku values.
    /// </summary>
    class KConstants
    {
        // Logging Block Headers
        public const string s_Start = "Start";
        public const string s_StartTag = "KLOG_BLOCK_START";
        public const string s_Stop = "Stop";
        public const string s_StopTag = "KLOG_BLOCK_STOP";

        // Logging Event Types
        public const string s_TraceEvent = "Trace";
        public const string s_InfoEvent = "Info";
        public const string s_WarningEvent = "Warning";
        public const string s_ErrorEvent = "Error";
        public const string s_MetricEvent = "Metric";
        public const string s_ResultEvent = "Result";

        // Config Values
        public const string s_Version = "Version";
        public const string s_ApplicationId = "ApplicationId";
        public const string s_TrackId = "TrackId";
        public const string s_RegionId = "RegionId";
        public const string s_ClusterId = "ClusterId";
        public const string s_DeviceId = "DeviceId";
        public const string s_FilePath = "Filepath";
        public const string s_Write = "Write";
        public const string s_Verbose = "Verbose";
        public const string s_Dynamic = "Dynamic";

        // Set Instance Log Tags
        public const string s_InstanceHeaderTag = "#KLOG_INSTANCE_HEADER#";
        public const string s_InstanceStatusTag = "#KLOG_INSTANCE_STATUS#";
        public const string s_InstanceStart = "Start";
        public const string s_InstanceStop = "Stop";

        // Log Header Status Types
        public const string s_WritingToLog = "KLOG_W_";
        public const string s_ReadyToSend = "KLOG_S_";
        public const string s_FileExt = ".txt";
    }
}