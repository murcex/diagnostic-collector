using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public class DNSRecord
    {
        // Constructor
        public DNSRecord(string source)
        {
            dt_session = Global.SessionDatetime;
            nvc_source = Global.SensorLocation;
            nvc_dns = source;
        }

        // Properties
        public Nullable<System.DateTime> dt_session { get; set; }
        public string nvc_source { get; set; }
        public string nvc_datacenter { get; set; }
        public string nvc_datacentertag { get; set; }
        public string nvc_dns { get; set; }
        public string nvc_ip { get; set; }
        public string nvc_status { get; set; }
    }
}
