namespace Sensor
{
    using System;
    using System.Collections.Generic;

    public class Capsule
    {
        public DateTime Session { get; set; }
        public string Source { get; set; }
        public List<DNSRecord> DNSRecords { get; set; }

        public List<SQLRecord> GenerateSQLRecords()
        {
            List<SQLRecord> sqlRecords = new List<SQLRecord>();

            var session = Session;
            var source = Source;

            foreach (var dnsRecord in DNSRecords)
            {
                var dnsName = dnsRecord.DNSName;
                var dnsStatus = dnsRecord.DNSStatus;

                foreach (var ipRecord in dnsRecord.IPRecords)
                {
                    var ip = ipRecord.IP.ToString();
                    var ipStatus = ipRecord.IPStatus;
                    var datacenter = ipRecord.Datacenter;
                    var datacenterTag = ipRecord.DatacenterTag;

                    //foreach (var tcpRecord in ipRecord.TCPRecords)
                    //{
                    var tcpRecord = ipRecord.TCPRecord;

                    var port = tcpRecord.Port;
                    var latency = tcpRecord.Latency;

                    SQLRecord record = new SQLRecord
                    {
                        Session = session,
                        Source = source,
                        DNSName = dnsName,
                        DNSStatus = dnsStatus,
                        IP = ip,
                        IPStatus = ipStatus,
                        Datacenter = datacenter,
                        DatacenterTag = datacenterTag,
                        Port = port,
                        Latency = latency
                    };

                    sqlRecords.Add(record);
                    //}
                }
            }

            return sqlRecords;
        }

        //public class SQLRecord
        //{
        //    public DateTime Session { get; set; }
        //    public string Source { get; set; }
        //    public string DNSName { get; set; }
        //    public string DNSStatus { get; set; }
        //    public string IP { get; set; }
        //    public string IPStatus { get; set; }
        //    public string Datacenter { get; set; }
        //    public string DatacenterTag { get; set; }
        //    public string Port { get; set; }
        //    public double Latency { get; set; }
        //}
    }
}
