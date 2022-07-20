namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using KirokuG2;

    public class SensorManager
    {
        /// <summary>
        /// Initialize Sensor application
        /// </summary>
        public static bool Initialize(List<KeyValuePair<string, string>> sensorConfig)
        {
            if (sensorConfig == null)
            {
                throw new ArgumentNullException(nameof(sensorConfig));
            }

            return Configuration.SetConfigs(sensorConfig);
        }

        /// <summary>
        /// Execute Sensor application
        /// </summary>
        public static bool Execute(IKLog klog)
        {
            Capsule capsule = new Capsule
            {
                Session = DateTime.Now.ToUniversalTime(),
                Source = Configuration.Source
            };

            capsule.DNSRecords = GetArticles.Execute(klog);

            GetIPAddress.Execute(klog, ref capsule);

            TagIPAddress.Execute(klog, ref capsule);

            GetTCPLatency.Execute(klog, ref capsule);

            UploadCapsule.Execute(klog, capsule);

            return true;
        }

        /// <summary>
        /// Execute Sensor data retention
        /// </summary>
        public static bool Retention(IKLog klog)
        {
            if (Configuration.Worker)
            {
                klog.Trace($"Worker Detected.");

                DataRetention.Execute();
            }

            return true;
        }
    }
}
