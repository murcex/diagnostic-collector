namespace Sensor
{
    using System;

    using Kiroku;

    public static class UploadCapsule
    {
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
