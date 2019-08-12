using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public class SQLRecord
    {
        public DateTime Session { get; set; }
        public string Source { get; set; }
        public string DNSName { get; set; }
        public string DNSStatus { get; set; }
        public string IP { get; set; }
        public string IPStatus { get; set; }
        public string Datacenter { get; set; }
        public string DatacenterTag { get; set; }
        public string Port { get; set; }
        public double Latency { get; set; }
    }
}
