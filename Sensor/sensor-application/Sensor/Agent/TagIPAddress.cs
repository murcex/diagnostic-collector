namespace Sensor
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public static class TagIPAddress
    {
        public static void Execute()
        {
            List<DNSRecord> sensorTransferList = new List<DNSRecord>();

            foreach (var ipRecord in Capsule.IPRecords)
            {
                DNSRecord sensor = new DNSRecord(ipRecord.HostName);

                sensor.nvc_ip = ipRecord.IP.ToString();

                var dnsConfig = Capsule.Articles.Where(x => x.DNSName == ipRecord.HostName).Select(x => x.DNSConfiguration).FirstOrDefault();

                // Check if configuration data exsits and deserialize
                if (dnsConfig.Contains(Global.IpAddress))
                {
                    var jsonObject = JsonConvert.DeserializeObject<List<Endpoint>>(dnsConfig);

                    // Match IPAddress with Data Center
                    sensor.nvc_datacenter = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenter).FirstOrDefault();
                    sensor.nvc_datacentertag = jsonObject.Where(x => x.IpAddress == sensor.nvc_ip).Select(x => x.DataCenterTag).FirstOrDefault();

                    // Set sensor with Data Center
                    sensor.nvc_status = Global.StatusOnline;
                }
                else
                {
                    sensor.nvc_datacenter = Global.UnknownDataCenter;
                    sensor.nvc_datacentertag = Global.UnknownDataCenterTag;
                    sensor.nvc_status = Global.StatusNoMatch;
                }

                if (sensor.nvc_datacenter == null)
                {
                    sensor.nvc_datacenter = Global.UnknownDataCenter;
                    sensor.nvc_datacentertag = Global.UnknownDataCenterTag;
                }

                sensorTransferList.Add(sensor);
            }

            Capsule.DNSRecords = sensorTransferList;
        }
    }
}
