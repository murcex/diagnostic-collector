namespace Sensor
{
    using System.Collections.Generic;

    public class DNSRecord
    {
        public string DNSName { get; set; }
        public string DNSConfiguration { get; set; }
        public string DNSStatus { get; set; }
        public List<IPRecord> IPRecords { get; set; }
    }
}
