using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public class DNSRecord
    {
        public string DNSName { get; set; }
        public string DNSConfiguration { get; set; }
        public string DNSStatus { get; set; }
        public List<IPRecord> IPRecords { get; set; }
    }
}
