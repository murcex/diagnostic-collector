using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
	public static class Capsule
	{
        public static List<DNSRecord> DNSRecords { get; set; }

        public static List<IPRecord> IPRecords { get; set; }

        public static List<DNSResolutionRecord> DNSResolutionRecords { get; set; }

        public static List<DNSDistributionRecord> DNSDistributionRecords { get; set; }

        public static List<TCPLatencyRecord> TCPLatencyRecords { get; set; }
    }
}
