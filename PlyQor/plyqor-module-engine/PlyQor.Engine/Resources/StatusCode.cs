namespace PlyQor.Resources
{
    public class StatusCode
    {
        /// <summary>
        /// OK
        /// </summary>
        public static string OK { get; } = "OK";

        /// <summary>
        /// Internal: dictionary is null
        /// </summary>
        public static string ERR001 { get; } = "ERR001";

        /// <summary>
        /// Internal: dictionary is empty
        /// </summary>
        public static string ERR002 { get; } = "ERR002";

        /// <summary>
        /// External: data value is empty
        /// </summary>
        public static string ERR003 { get; } = "ERR003";

        /// <summary>
        /// External: data key is missing
        /// </summary>
        public static string ERR004 { get; } = "ERR004";

        /// <summary>
        /// External: data value is less than 1
        /// </summary>
        public static string ERR005 { get; } = "ERR005";

        /// <summary>
        /// External: can't parse int data value
        /// </summary>
        public static string ERR006 { get; } = "ERR006";

        /// <summary>
        /// External: failed to deserialize tags
        /// </summary>
        public static string ERR008 { get; } = "ERR008";

        /// <summary>
        /// Value is Null
        /// </summary>
        public static string ERR009 { get; } = "ERR009";

        /// <summary>
        /// Internal: sql storage error
        /// </summary>
        public static string ERR010 { get; } = "ERR010";

        /// <summary>
        /// Internal: failed to merge dictionary entry
        /// </summary>
        public static string ERR011 { get; } = "ERR011";

        /// <summary>
        /// Internal: Sql Server request limit
        /// </summary>
        public static string ERR012 { get; } = "ERR012";

        /// <summary>
        /// External: Sql Server primary key constraint
        /// </summary>
        public static string ERR013 { get; } = "ERR013";

        /// <summary>
        /// Internal: Sql Server exception
        /// </summary>
        public static string ERR014 { get; } = "ERR014";

        /// <summary>
        /// External: malformed request message
        /// </summary>
        public static string ERRMALFORM { get; } = "ERRMALFORM";

        /// <summary>
        /// External: access token failed
        /// </summary>
        public static string ERRBLOCK { get; } = "ERRBLOCK";

        /// <summary>
        /// External: operation type missing
        /// </summary>
        public static string ERR033 { get; } = "ERR033";

        /// <summary>
        /// 
        /// </summary>
        public static string ERR400 { get; } = "400";

        /// <summary>
        /// 
        /// </summary>
        public static string ERR401 { get; } = "401";
    }
}
