namespace Sensor
{
    using System.Net;

    public class IPRecord
    {
        public IPAddress IP { get; set; }

        public string IPStatus { get; set; }

        public string Datacenter { get; set; }

        public string DatacenterTag { get; set; }

        public TCPRecord TCPRecord { get; set; }
    }
}
