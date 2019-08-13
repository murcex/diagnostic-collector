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
                    //TODO: Detect Activity Method

                    //TODO: Detect Failover Method

                    DataRetention.Execute();
                }
            }

            KManager.Offline();

            #endregion

            #region console.debug

            if (Global.Debug)
            {
                Console.WriteLine("-- Operation Complete --");
                Console.Read();
            }

            #endregion

            // Close application
            Environment.Exit(0);
        }
    }
}
