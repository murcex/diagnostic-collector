namespace Kiroku
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// DATA MODEL: Instance Model used to contain the instance header and footer.
    /// </summary>
    class LogInstance : ILog, IDisposable
    {
        #region Vars

        /// <summary>
        /// 
        /// </summary>
        private bool dispose = false;
        public string Version { get; private set; }
        public Nullable<System.DateTime> EventTime { get; private set; }
        public string ApplicationID { get; private set; }
        public string TrackID { get; private set; }
        public string RegionID { get; private set; }
        public string ClusterID { get; private set; }
        public string DeviceID { get; private set; }
        public Guid InstanceID { get; private set; }
        public string InstanceStatus { get; private set; }
        [JsonIgnore]
        public string FilePath { get; private set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Log instance constructor -- static instance.
        /// </summary>
        /// <param name="instanceStatus"></param>
        /// <param name="instanceId"></param>
        /// <param name="appConfig"></param>
        public LogInstance(string instanceStatus, Guid instanceId, AppConfiguration appConfig)
        {
            Version = appConfig.Version;
            EventTime = DateTime.Now.ToUniversalTime();
            ApplicationID = appConfig.ApplicationId;
            TrackID = appConfig.TrackId;
            RegionID = appConfig.RegionId;
            ClusterID = appConfig.ClusterId;
            DeviceID = appConfig.DeviceId;
            InstanceID = instanceId;
            InstanceStatus = instanceStatus;
            FilePath = appConfig.FullFilePath;
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