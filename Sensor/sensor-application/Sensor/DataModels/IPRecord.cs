using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Sensor
{
    public class IPRecord
    {
        public IPAddress IP { get; set; }

        public string IPStatus { get; set; }

        public string Datacenter { get; set; }

        public string DatacenterTag { get; set; }

        public TCPRecord TCPRecord { get; set; }
    }
}
