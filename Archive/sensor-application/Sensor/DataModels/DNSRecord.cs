namespace Sensor
{
    using System.Collections.Generic;
    using System.Net;

    public class DNSRecord
    {
        public string DNSName { get; set; }
        public string DNSConfiguration { get; set; }
        public string DNSStatus { get; set; }
        public List<IPRecord> IPRecords { get; set; }

        /// <summary>
        /// Mark the DNSRecord as Offline.
        /// </summary>
        public void SetOffline()
        {
            IPAddress address = IPAddress.Parse("0.0.0.0");

            IPRecord record = new IPRecord
            {
                IP = address,
                IPStatus = "OFFLINE"
            };

            List<IPRecord> records = new List<IPRecord>
            {
                record
            };

            this.IPRecords = records;
            this.DNSStatus = "OFFLINE";
        }
    }
}
