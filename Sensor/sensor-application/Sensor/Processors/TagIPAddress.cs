namespace Sensor
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using System;

    public static class TagIPAddress
    {
        public static void Execute()
        {
            List<DNSResolutionRecord> sensorTransferList = new List<DNSResolutionRecord>();

            try
            {
                foreach (var ipRecord in Capsule.IPRecords)
                {
                    DNSResolutionRecord sensor = new DNSResolutionRecord(ipRecord.HostName);

                    sensor.IP = ipRecord.IP.ToString();

                    var dnsConfig = Capsule.DNSRecords.Where(x => x.DNSName == ipRecord.HostName).Select(x => x.DNSConfiguration).FirstOrDefault();

                    // Check if configuration data exsits and deserialize
                    if (dnsConfig.Contains(Global.IpAddress))
                    {
                        var jsonObject = JsonConvert.DeserializeObject<List<EndpointRecord>>(dnsConfig);

                        // Match IPAddress with Data Center
                        sensor.Datacenter = jsonObject.Where(x => x.IpAddress == sensor.IP).Select(x => x.DataCenter).FirstOrDefault();
                        sensor.DatacenterTag = jsonObject.Where(x => x.IpAddress == sensor.IP).Select(x => x.DataCenterTag).FirstOrDefault();

                        // Set sensor with Data Center
                        sensor.Status = Global.StatusOnline;
                    }
                    else
                    {
                        sensor.Datacenter = Global.UnknownDataCenter;
                        sensor.DatacenterTag = Global.UnknownDataCenterTag;
                        sensor.Status = Global.StatusNoMatch;
                    }

                    if (sensor.Datacenter == null)
                    {
                        sensor.Datacenter = Global.UnknownDataCenter;
                        sensor.DatacenterTag = Global.UnknownDataCenterTag;
                    }

                    sensorTransferList.Add(sensor);
                }

                Capsule.DNSResolutionRecords = sensorTransferList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"TagIPAddress - Excepection: {e.ToString()}");
            }
        }
    }
}
