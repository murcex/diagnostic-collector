namespace KirokuG2.Processor.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using KirokuG2.Processor.Core;

    public class MaintenanceFunc
    {
        private static List<string> tables = new List<string>()
        {
            "Activation",
            "Block",
            "Critical",
            "Error",
            "Instance",
            "Metric",
            "Quarantine"
        };

        [FunctionName("Kiroku-Maintenance")]
        public void Run([TimerTrigger("0 0 12 * * *")] TimerInfo myTimer, ILogger log, ExecutionContext executionContext)
        {
            using (var klog = KManager.NewInstance(executionContext.FunctionName))
            {
                try
                {
                    //foreach list execute sql query
                    foreach (var table in tables)
                    {
                        // sql
                        var query = $"DELETE FROM [tbl_KirokuG2_{table}] WHERE [dt_session] < DATEADD(day,-7,GETDATE())";

                        using (var connection = new SqlConnection(Configuration.Database))
                        {
                            connection.Open();
                            using (var command = new SqlCommand(query, connection))
                            {
                                var r = command.ExecuteReader();
                                while (r.Read())
                                { }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    klog.Error(ex.ToString());
                }
            }
        }
    }
}
