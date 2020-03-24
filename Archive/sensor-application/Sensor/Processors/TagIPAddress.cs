namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    using Kiroku;

    public static class TagIPAddress
    {
        /// <summary>
        /// Evaluate IP address for match on a known provided region.
        /// </summary>
        /// <param name="capsule"></param>
        public static void Execute(ref Capsule capsule)
        {
            using (KLog klog = new KLog("TagIPAddress"))
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
                            if (dnsConfig.Contains(Global.IpAddress))
                            {
                                var jsonObject = JsonConvert.DeserializeObject<List<EndpointRecord>>(dnsConfig);

                                // Match IPAddress with Data Center
                                ipRecord.Datacenter = jsonObject.Where(x => x.IpAddress == ipString).Select(x => x.DataCenter).FirstOrDefault();
                                ipRecord.DatacenterTag = jsonObject.Where(x => x.IpAddress == ipString).Select(x => x.DataCenterTag).FirstOrDefault();

                                // Set sensor with Data Center
                                ipRecord.IPStatus = Global.StatusOnline;
                            }
                            else
                            {
                                if (dnsConfig.Contains("empty"))
                                {
                                    klog.Warning($"DNS Config is missing");
                                }
                                else
                                {
                                    klog.Error($"DNS Config is malformed");
                                }

                                ipRecord.Datacenter = Global.UnknownDataCenter;
                                ipRecord.DatacenterTag = Global.UnknownDataCenterTag;
                            }

                            if (ipRecord.Datacenter == null)
                            {
                                ipRecord.Datacenter = Global.UnknownDataCenter;
                                ipRecord.DatacenterTag = Global.UnknownDataCenterTag;
                            }

                            klog.Trace($"DNS: {article.DNSName} IP: {ipRecord.IP} Datacenter: {ipRecord.Datacenter} Tag: {ipRecord.DatacenterTag}");
                        }
                    }
                    catch (Exception e)
                    {
                        klog.Error($"TagIPAddress - Excepection: {e.ToString()}");
                    }
                }
            }
        }
    }
}
