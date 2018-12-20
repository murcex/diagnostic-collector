using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensor
{
    public class DNSResolutionRecord
    {
        // Constructor
        public DNSResolutionRecord(string source)
        {
            Session = Global.SessionDatetime;
            Source = Global.SensorLocation;
            DNS = source;
        }

        // Properties
        public Nullable<System.DateTime> Session { get; set; }
        public string Source { get; set; }
        public string Datacenter { get; set; }
        public string DatacenterTag { get; set; }
        public string DNS { get; set; }
        public string IP { get; set; }
        public string Status { get; set; }
    }
}
