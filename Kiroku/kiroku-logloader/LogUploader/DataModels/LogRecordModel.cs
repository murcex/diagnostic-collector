namespace KLOGLoader
{
    using System;

    public class LogRecordModel
    {
        public Nullable<System.DateTime> EventTime { get; set; }
        public Guid BlockID { get; set; }
        public string BlockName { get; set; }
        public string LogType { get; set; }
        public string LogData { get; set; }
    }
}
