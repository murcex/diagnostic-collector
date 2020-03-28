namespace Sensor
{
    using System;

    using Kiroku;

    public static class UploadCapsule
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
                    AddRecords.Insert(capsule.GenerateSQLRecords());
                }
                catch (Exception ex)
                {
                    klog.Error($"UploadCapsule::Execute | {ex.ToString()}");
                }
            }
        }
    }
}
