namespace Sensor
{
    using System;
    using KirokuG2;

    static class UploadCapsule
    {
        /// <summary>
        /// Upload all Sensor Capsule records to Azure SQL Server.
        /// </summary>
        /// <param name="capsule"></param>
        public static void Execute(IKLog klog, Capsule capsule)
        {
            try
            {
                string result = AddRecords.Insert(capsule.GenerateSQLRecords(klog));

                if (!string.IsNullOrEmpty(result) && result.Contains("Result: True"))
                {
                    klog.Trace($"{result}");
                }
                else
                {
                    klog.Error($"Capsule Upload Failure: {result}");
                }
            }
            catch (Exception ex)
            {
                klog.Error($"UploadCapsule::Execute | {ex}");
            }
        }
    }
}
