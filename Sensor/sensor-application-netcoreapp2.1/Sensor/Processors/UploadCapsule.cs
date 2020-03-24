namespace Sensor
{
    using System;
    using System.Collections.Generic;
    using Kiroku;

    static class UploadCapsule
    {
        /// <summary>
        /// Upload all Sensor Capsule records to Azure SQL Server.
        /// </summary>
        /// <param name="capsule"></param>
        public static void Execute(Capsule capsule)
        {
            using (KLog klog = new KLog("UploadData"))
            {
                try
                {
                    string result = AddRecords.Insert(capsule.GenerateSQLRecords());

                    if (!string.IsNullOrEmpty(result) && result.Contains("Result: True"))
                    {
                        klog.Trace($"{result}");
                        klog.Success("Capsule Uploaded.");
                    }
                    else
                    {
                        klog.Error($"Capsule Upload Failure: {result}");
                        klog.Failure($"Capsule Upload Failure.");
                    }
                }
                catch (Exception ex)
                {
                    klog.Error($"UploadCapsule::Execute | {ex.ToString()}");
                }
            }
        }
    }
}
