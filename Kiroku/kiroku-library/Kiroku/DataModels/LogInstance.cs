namespace Kiroku
{
    using System;

    /// <summary>
    /// DATA MODEL: Instance Model used to contain the instance header and footer.
    /// </summary>
    class LogInstance : IDisposable
    {
        #region Vars

        /// <summary>
        /// 
        /// </summary>
        bool dispose = false;
        public string Version { get; set; }
        public Nullable<System.DateTime> EventTime { get; set; }
        public string ApplicationID { get; set; }
        public string TrackID { get; set; }
        public string RegionID { get; set; }
        public string ClusterID { get; set; }
        public string DeviceID { get; set; }
        public Guid InstanceID { get; set; }
        public string InstanceStatus { get; set; }

        #endregion

        #region Constru

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceStatus"></param>
        public LogInstance(string instanceStatus)
        {
            Version = LogConfiguration.Version;
            EventTime = DateTime.Now.ToUniversalTime();
            ApplicationID = LogConfiguration.ApplicationID;
            TrackID = LogConfiguration.TrackID;
            RegionID = LogConfiguration.RegionID;
            ClusterID = LogConfiguration.ClusterID;
            DeviceID = LogConfiguration.DeviceID;
            InstanceID = LogConfiguration.InstanceID;
            InstanceStatus = instanceStatus;
        }

        #endregion

        #region Disposal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!dispose)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            dispose = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
