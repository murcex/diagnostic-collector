using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
	public static class Capsule
	{
        public static List<Article> Articles { get; set; }

        public static List<IPRecord> IPRecords { get; set; }

        public static List<DNSResolution> DNSResolutionRecords { get; set; }

        public static List<DNSDistribution> DNSDistributionRecords { get; set; }

        public static List<TCPLatency> TCPLatencyRecords { get; set; }
    }
}
