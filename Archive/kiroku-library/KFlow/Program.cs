namespace KFlow
{
    using System;
    using System.Threading.Tasks;

    // Kiroku Logging Library
    using Kiroku;

    class Program
    {
        static void Main(string[] args)
        {
            #region Dynamic-False

            for (int instanceIteration = 1; instanceIteration <= Global.InstanceLoop; instanceIteration++)
            {
                KManager.Online(Global.KirokuTagList);

                for (int blockIteration = 1; blockIteration <= Global.BlockLoop; blockIteration++)
                {
                    using (KLog klog = new KLog($"Block-{instanceIteration}-{blockIteration}"))
                    {
                        klog.Metric("Test Metric One", blockIteration);
                        klog.Metric("Test Metric Two", true);
                        klog.Metric("Test Metric Three", 2.33);

                        if (Global.TraceOn)
                        {
                            try
                            {
                                // Trace
                                for (int traceMeter = 1; traceMeter <= Global.TraceLoopCount; traceMeter++)
                                {
                                    klog.Trace(Generator.Execute(Global.TraceCharCount));
                                }

                                // Info
                                if (Global.InfoOn)
                                {
                                    for (int infoMeter = 1; infoMeter <= Global.InfoLoopCount; infoMeter++)
                                    {
                                        klog.Info(Generator.Execute(Global.InfoCharCount));
                                    }
                                }

                                // Warning
                                if (Global.WarningOn)
                                {
                                    for (int warningMeter = 1; warningMeter <= Global.WarningLoopCount; warningMeter++)
                                    {
                                        klog.Warning(Generator.Execute(Global.WarningCharCount));
                                    }
                                }

                                // Error
                                if (Global.ErrorOn)
                                {
                                    for (int errorMeter = 1; errorMeter <= Global.ErrorLoopCount; errorMeter++)
                                    {
                                        klog.Error(Generator.Execute(Global.ErrorCharCount));
                                    }
                                }

                                klog.Success();
                            }
                            catch (Exception e)
                            {
                                klog.Error($"KFlow Exception: {e.ToString()}");
                                klog.Failure();
                            }
                        }
                    }
                }

                KManager.Offline();
            }

            #endregion

            #region Dynamic-True

            KManager.Configure(Global.KirokuTagList, dynamic: true);

            var items = new string[] { "red", "green", "blue", "yellow" };
            var tasks = new Task[items.Length];
            var taskCounter = 0;

            foreach (var item in items)
            {
                tasks[taskCounter] = new Task(() => DynamicTest(item));

                taskCounter++;
            }

            Parallel.ForEach(tasks, (t) => { t.Start(); });
            Task.WaitAll(tasks);

            #endregion
        }

        /// <summary>
        /// Test method for dynamic KLOG creation.
        /// </summary>
        /// <param name="instanceIteration"></param>
        public static void DynamicTest(string instanceIteration)
        {
            using (KLog klog = new KLog($"Dynamic KFlow -- Primary Node -- {instanceIteration}"))
            {
                klog.Info("Testing primary block");

                using (KLog nestedKLog = new KLog($"Dynamic KFlow -- Nested Node -- {instanceIteration}", klog.instanceId))
                {
                    nestedKLog.Info("Testing nested block");
                }
            }
        }
    }
}