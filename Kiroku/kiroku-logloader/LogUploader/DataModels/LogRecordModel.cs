using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLOGLoader
{
    public class LogRecordModel
    {
        public Nullable<System.DateTime> EventTime { get; set; }
        public Guid BlockID { get; set; }
        public string BlockName { get; set; }
        public string LogType { get; set; }
        public string LogData { get; set; }
    }
}
