namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using KirokuG2;

    static class GetIPAddress
    {
        /// <summary>
        /// Preform DNS Query to gather IPV4 IP's.
        /// </summary>
        /// <param name="capsule"></param>
        public static void Execute(IKLog klog, ref Capsule capsule)
        {
            try
            {
                // for each DNS Record
                foreach (var article in capsule.DNSRecords)
                {
                    try
                    {
                        var action = true;
                        var count = 0;
                        List<IPRecord> ipRecordQuickList = new List<IPRecord>();
                        List<IPRecord> ipRecordTransferList = new List<IPRecord>();

                        // DNS query multiple times
                        while (action && count < 4)
                        {
                            try
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

                                action = false;
                            }
                            catch (Exception ex)
                            {
                                if (ex.ToString().Contains("No such host is known"))
                                {
                                    Thread.Sleep(5);

                                    count++;
                                }
                                else
                                {
                                    throw;
                                }
                            }
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
                            klog.Error($"Exception: {ex}");
                        }

                        article.SetOffline();
                    }
                }
            }
            catch (Exception e)
            {
                klog.Error($"GetIPAddress - Exception: {e}");
            }
        }
    }
}
