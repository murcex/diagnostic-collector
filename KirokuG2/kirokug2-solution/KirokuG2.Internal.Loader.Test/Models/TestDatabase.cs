using KirokuG2.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirokuG2.Internal.Loader.Test.Models
{
    public class TestDatabase
    {
        private List<LogMetric> logMetrics = new List<LogMetric>();

        private List<LogInstance> logInstances = new List<LogInstance>();

        private List<LogError> logErrors = new List<LogError>();

        private List<LogBlock> logBlocks = new List<LogBlock>();

        private List<LogError> criticalLogs = new List<LogError>();

        private List<(DateTime session, string record_id)> quarantineLogs = new List<(DateTime session, string record_id)>();

        private List<(DateTime session, string record_id, string source)> activationLogs = new List<(DateTime session, string record_id, string source)>();


    }
}
