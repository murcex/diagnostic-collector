namespace Sensor
{
    using KirokuG2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;

    static class TagIPAddress
    {
        /// <summary>
        /// Evaluate IP address for match on a known provided region.
        /// </summary>
        /// <param name="capsule"></param>
        public static void Execute(IKLog klog, ref Capsule capsule)
        {
            foreach (var article in capsule.DNSRecords)
            {
                var dnsConfig = article.DNSConfiguration;
                var ips = article.IPRecords;

                try
                {
                    foreach (var ipRecord in ips)
                    {
                        var ipString = ipRecord.IP.ToString();

                        if (ipRecord.IPStatus == "OFFLINE")
                        {
                            ipRecord.Datacenter = "ERROR";
                            ipRecord.DatacenterTag = "ERR";

                            break;
                        }

                        // Check if configuration data exsits and deserialize
                        if (dnsConfig.Contains(Configuration.IpAddress))
                        {
                            var jsonObject = JsonSerializer.Deserialize<List<EndpointRecord>>(dnsConfig);

                            // Match IPAddress with Data Center
                            ipRecord.Datacenter = jsonObject.Where(x => x.IpAddress == ipString).Select(x => x.DataCenter).FirstOrDefault();
                            ipRecord.DatacenterTag = jsonObject.Where(x => x.IpAddress == ipString).Select(x => x.DataCenterTag).FirstOrDefault();

                            // Set sensor with Data Center
                            ipRecord.IPStatus = Configuration.StatusOnline;
                        }
                        else
                        {
                            if (dnsConfig.Contains("empty"))
                            {
                                klog.Error($"DNS Config is missing");
                            }
                            else
                            {
                                klog.Error($"DNS Config is malformed");
                            }

                            ipRecord.Datacenter = Configuration.UnknownDataCenter;
                            ipRecord.DatacenterTag = Configuration.UnknownDataCenterTag;
                        }

                        if (ipRecord.Datacenter == null)
                        {
                            ipRecord.Datacenter = Configuration.UnknownDataCenter;
                            ipRecord.DatacenterTag = Configuration.UnknownDataCenterTag;
                        }

                        klog.Trace($"DNS: {article.DNSName} IP: {ipRecord.IP} Datacenter: {ipRecord.Datacenter} Tag: {ipRecord.DatacenterTag}");
                    }
                }
                catch (Exception e)
                {
                    klog.Error($"TagIPAddress - Excepection: {e}");
                }
            }
        }
    }
}
