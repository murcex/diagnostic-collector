namespace Sensor
{
    public class TCPRecord
    {
        public string Port { get; set; }
        public double Latency { get; set; }

        /// <summary>
        /// Mark the TCPRecord as Offline.
        /// </summary>
        public void SetOffline()
        {
            this.Port = "443";
            this.Latency = -1;
        }
    }
}
