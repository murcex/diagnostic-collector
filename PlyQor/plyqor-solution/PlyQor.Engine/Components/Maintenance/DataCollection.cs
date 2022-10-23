using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlyQor.Engine.Components.Maintenance
{
    public class DataCollection
    {
        public static void Execute()
        {
            // foreach container

            // get record count (data) [select into]
            // done

            // get record count by tag (tag) [select into]
            // done

            // get total operation count by operations over the last 24 hours (last check point) (as operation)
            // between (get last known timestamp as day + 1) and (current date - 1)

            // get total operation error count (error)
            // total / false count

            // get 50th (avg), 95th, 99th for operation latency
            // avg done
            // 95th done

            // dt_timestamp, nvc_container, i_type, nvc_key, i_value
            // i_type = data, retention, tag, error, operation, avg, p95
        }
    }
}
