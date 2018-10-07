using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLOGLoader
{
    public class InstanceModel
    {
        public string Version { get; set; }
        public Nullable<System.DateTime> EventTime { get; set; }
        public Guid ApplicationID { get; set; }
        public Guid InstanceID { get; set; }
        public string InstanceStatus { get; set; }
    }
}
