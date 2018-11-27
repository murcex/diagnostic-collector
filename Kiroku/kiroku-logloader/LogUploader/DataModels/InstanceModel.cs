namespace KLOGLoader
{
    using System;

    public class InstanceModel
    {
        public string Version { get; set; }
        public Nullable<System.DateTime> EventTime { get; set; }
        public Guid ApplicationID { get; set; }
        public Guid InstanceID { get; set; }
        public string InstanceStatus { get; set; }
    }
}
