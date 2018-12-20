namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    public static class GetIPAddress
    {
        public static void Execute()
        {
            List<IPRecord> ipRecordTransferList = new List<IPRecord>();

            List<DNSDistributionRecord> dnsCountList = new List<DNSDistributionRecord>();

            try
            {
                foreach (var article in Capsule.DNSRecords)
                {
                    try
                    {
                        var count = 0;
                        List<IPRecord> ipRecordQuickList = new List<IPRecord>();

                        while (count < 10)
                        {
                            var ips = Dns.GetHostAddresses(article.DNSName);

                            foreach (var ip in ips)
                            {
                                IPRecord record = new IPRecord();

                                record.IP = ip;
                                record.HostName = article.DNSName;

                                ipRecordQuickList.Add(record);
                            }

                            Thread.Sleep(25);

                            count++;
                        }

                        var ipRecordDistinctList = ipRecordQuickList.GroupBy(ip => ip.IP).Select(y => y.First());

                        foreach (var ipRecord in ipRecordDistinctList)
                        {
                            var hitCount = ipRecordQuickList.Select(x => x.IP == ipRecord.IP).Count();

                            DNSDistributionRecord dnsCount = new DNSDistributionRecord();
                            dnsCount.IP = ipRecord.IP;
                            dnsCount.HostName = ipRecord.HostName;
                            dnsCount.Count = hitCount;

                            dnsCountList.Add(dnsCount);

                            ipRecordTransferList.Add(ipRecord);
                        }

                        Capsule.IPRecords = ipRecordTransferList;

                        Capsule.DNSDistributionRecords = dnsCountList;
                    }
                    catch (Exception ex)
                    {
                        if (ex.ToString().Contains("No such host is known"))
                        {
                            Console.WriteLine("Exception: Unknow Host. ({0})", article.DNSName);
                        }
                        else
                        {
                            Console.WriteLine("Exception: {0}", ex.ToString());
                        }

                        IPAddress[] address = { IPAddress.Parse("0.0.0.0") };
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetIPAddress - Exception: {e.ToString()}");
            }
        }
    }
}
