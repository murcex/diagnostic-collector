namespace Sensor
{
    using Kiroku;
    using System;
    using System.Collections.Generic;

    class Capsule
    {
        public DateTime Session { get; set; }
        public string Source { get; set; }
        public List<DNSRecord> DNSRecords { get; set; }

        /// <summary>
        /// Convert Sensor Capusle records into single row SQL records for uploading.
        /// </summary>
        /// <returns></returns>
        public List<SQLRecord> GenerateSQLRecords()
        {
            List<SQLRecord> sqlRecords = new List<SQLRecord>();

            var session = Session;
            var source = Source;

            using (KLog klog = new KLog("GenerateSQLRecords"))
            {
                foreach (var dnsRecord in DNSRecords)
                {
                    try
                    {
                        var dnsName = dnsRecord.DNSName;
                        var dnsStatus = dnsRecord.DNSStatus;

                        foreach (var ipRecord in dnsRecord.IPRecords)
                        {
                            var ip = ipRecord.IP.ToString();
                            var ipStatus = ipRecord.IPStatus;
                            var datacenter = ipRecord.Datacenter;
                            var datacenterTag = ipRecord.DatacenterTag;
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
                        }
                    }
                    catch (Exception ex)
                    {
                        klog.Error($"Capsule::GenerateSQLRecords - EXCEPTION: {ex}");
                    }
                }

                return sqlRecords;
            }
        }
    }
}
