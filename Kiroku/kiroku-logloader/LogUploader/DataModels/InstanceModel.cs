namespace KLOGLoader
{
    using System;

    public class InstanceModel
    {
        public string Version { get; set; }
        public Nullable<System.DateTime> EventTime { get; set; }
        public string ApplicationID { get; set; }
        public string TrackID { get; set; }
        public string RegionID { get; set; }
        public string ClusterID { get; set; }
        public string DeviceID { get; set; }
        public Guid InstanceID { get; set; }
        public string InstanceStatus { get; set; }
    }
}
