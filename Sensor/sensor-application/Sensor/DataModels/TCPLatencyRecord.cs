using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public class TCPLatencyRecord
    {
        public string HostName { get; set; }

        public IPAddress IP { get; set; }

        public double Latency { get; set; }
    }
}
