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

        public static List<DNSRecord> DNSRecords { get; set; }

        public static List<DNSCount> DNSCounts { get; set; }

        //public static int ArticleCount()
        //{
        //    var count = Articles.Count();

        //    return count;
        //}
    }
}
