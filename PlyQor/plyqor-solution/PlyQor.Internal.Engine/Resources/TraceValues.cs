namespace PlyQor.Engine.Resources
{
	class TraceValues
	{
		public static string TraceNoContainer { get; } = "NoContainer";
		public static string TraceNoOperation { get; } = "NoOperation";
		public static string OK { get; } = "OK";
		public static string TraceKeySplit { get; } = ",KEY";
		public static string InsertTrace { get; } = "usp_PlyQor_Trace_InsertTrace";
		public static string Timestamp { get; } = "dt_timestamp";
		public static string Container { get; } = "nvc_container";
		public static string Id { get; } = "nvc_id";
		public static string Operation { get; } = "nvc_operation";
		public static string Code { get; } = "nvc_code";
		public static string Status { get; } = "nvc_status";
		public static string Duration { get; } = "i_duration";
	}
}
