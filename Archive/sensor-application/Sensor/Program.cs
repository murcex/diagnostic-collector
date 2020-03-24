namespace Sensor
{
    using System;

    using Kiroku;

    class Program
    {
        static void Main(string[] args)
        {
            #region agent mode

            KManager.Online(Global.KirokuTagList);

            Capsule capsule = new Capsule
            {
                Session = Global.Session,
                Source = Global.Source
            };

            if (Global.Agent)
            {
                using (KLog klog = new KLog("AgentExecutionStack"))
                {
                    capsule.DNSRecords = GetArticles.Execute();

                    GetIPAddress.Execute(ref capsule);

                    TagIPAddress.Execute(ref capsule);

                    GetTCPLatency.Execute(ref capsule);

                    UploadCapsule.Execute(capsule);
                }
            }

            #endregion

            #region worker mode

            if (Global.Worker)
            {
                using (KLog klog = new KLog("WorkerExecutionStack"))
                {
                    DataRetention.Execute();
                }
            }

            KManager.Offline();

            #endregion

            // Close application
            Global.CheckDebug();
            Environment.Exit(0);
        }
    }
}
