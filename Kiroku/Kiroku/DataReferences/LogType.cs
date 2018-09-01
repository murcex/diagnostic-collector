// No using

namespace Kiroku
{
    /// <summary>
    /// CLASS: Contains global constant Kiroku values.
    /// </summary>
    class LogType
    {
        // Logging Block Headers
        public const string Start = "Start";
        public const string StartTag = "KLOG_BLOCK_START";
        public const string Stop = "Stop";
        public const string StopTag = "KLOG_BLOCK_STOP";

        // Logging Event Types
        public const string Trace = "Trace";
        public const string Info = "Info";
        public const string Warning = "Warning";
        public const string Error = "Error";

        // Set Instance Log Tags
        public const string InstanceHeaderTag = "#KLOG_INSTANCE_HEADER#";
        public const string InstanceStatusTag = "#KLOG_INSTANCE_STATUS#";
        public const string InstanceStart = "Start";
        public const string InstanceStop = "Stop";

        // Log Header Status Types
        public const string WritingToLog = "KLOG_W_";
        public const string ReadyToSend = "KLOG_S_";
    }
}
