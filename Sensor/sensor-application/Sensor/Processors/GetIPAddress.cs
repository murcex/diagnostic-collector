namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using Kiroku;

    public static class GetIPAddress
    {
        /// <summary>
        /// Preform DNS Query to gather IPV4 IP's.
        /// </summary>
        /// <param name="capsule"></param>
        public static void Execute(ref Capsule capsule)
        {
            using (KLog klog = new KLog("GetIPAddress"))
            {
                try
                {
                    // for each DNS Record
                    foreach (var article in capsule.DNSRecords)
                    {
                        try
                        {
                            var count = 0;
                            List<IPRecord> ipRecordQuickList = new List<IPRecord>();
                            List<IPRecord> ipRecordTransferList = new List<IPRecord>();

                            // DNS query multiple times
                            while (count < 3)
                            {
                                var ips = Dns.GetHostAddresses(article.DNSName);

                                klog.Metric($"ipcount-{article.DNSName}", ips.Length);

                                // DNS query may return more than one ip
                                foreach (var ip in ips)
                                {
                                    IPRecord record = new IPRecord();

                                    record.IP = ip;
                                    record.IPStatus = "ONLINE";

                                    ipRecordQuickList.Add(record);
                                }

                                Thread.Sleep(5);

                                count++;
                            }

                            var ipRecordDistinctList = ipRecordQuickList.GroupBy(ip => ip.IP).Select(y => y.First());

                            foreach (var ipRecord in ipRecordDistinctList)
                            {
                                var hitCount = ipRecordQuickList.Select(x => x.IP == ipRecord.IP).Count();

                                ipRecordTransferList.Add(ipRecord);
                            }

                            article.IPRecords = ipRecordTransferList;
                            article.DNSStatus = "ONLINE";
                        }
                        catch (Exception ex)
                        {
                            if (ex.ToString().Contains("No such host is known"))
                            {
                                klog.Error($"Exception: Unknow Host. {article.DNSName}");
                            }
                            else
                            {
                                klog.Error($"Exception: {ex.ToString()}");
                            }

                            article.SetOffline();
                        }
                    }
                }
                catch (Exception e)
                {
                    klog.Error($"GetIPAddress - Exception: {e.ToString()}");
                }
            }
        }
    }
}
