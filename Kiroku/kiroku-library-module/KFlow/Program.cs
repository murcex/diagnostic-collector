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
            Console.WriteLine("Testing Static Kiroku Logger");

            KManager.Configure(Global.KirokuTagList);

            KManager.Open();

            using (KLog klog = new KLog("testing block one"))
            {
                klog.Trace("testing trace one");

                klog.Info("testing info one");

                klog.Warning("testing warning one");

                klog.Error("testing error one");

                klog.Metric("test one", 1);

                klog.Success("test one success");
            }

            KManager.Close();

            KManager.Configure(Global.KirokuTagList, dynamic: true);

            Console.WriteLine("\n\rTrying to use Static Logger while Dynamic=true Kiroku Logger");
            try
            {
                KManager.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Open Exception: {ex.ToString()}");
            }
            
            try
            {
                KManager.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Close Exception: {ex.ToString()}");
            }

            Console.WriteLine("\n\rTesting Non-Nested Dynamic Kiroku Logger");
            using (KLog klog = new KLog("testing block two"))
            {
                klog.Trace("testing trace two");

                klog.Info("testing info two");

                klog.Warning("testing warning two");

                klog.Error("testing error two");

                klog.Metric("test two", 1);

                klog.Success("test two success");
            }

            Console.WriteLine("\n\rTesting Nested Dynamic Kiroku Logger");
            using (KLog klog = new KLog("testing block three"))
            {
                klog.Trace("testing trace three");

                klog.Info("testing info three");

                klog.Warning("testing warning three");

                klog.Error("testing error three");

                klog.Metric("test three", 1);

                klog.Success("test three success");

                using (KLog klog2 = new KLog("nested testing block three", klog))
                {
                    klog2.Trace("nested testing trace three");

                    klog2.Info("nested testing info three");

                    klog2.Warning("nested testing warning three");

                    klog2.Error("nested testing error three");

                    klog2.Metric("nested test three", 1);

                    klog2.Success("nested test three success");
                }
            }

            Console.WriteLine("\n\rTesting Task<Nested Dynamic> Kiroku Logger");

            //#region Dynamic-False

            //for (int instanceIteration = 1; instanceIteration <= Global.InstanceLoop; instanceIteration++)
            //{
            //    KManager.Online(Global.KirokuTagList);

            //    for (int blockIteration = 1; blockIteration <= Global.BlockLoop; blockIteration++)
            //    {
            //        using (KLog klog = new KLog($"Block-{instanceIteration}-{blockIteration}"))
            //        {
            //            klog.Metric("Test Metric One", blockIteration);
            //            klog.Metric("Test Metric Two", true);
            //            klog.Metric("Test Metric Three", 2.33);

            //            if (Global.TraceOn)
            //            {
            //                try
            //                {
            //                    // Trace
            //                    for (int traceMeter = 1; traceMeter <= Global.TraceLoopCount; traceMeter++)
            //                    {
            //                        klog.Trace(Generator.Execute(Global.TraceCharCount));
            //                    }

            //                    // Info
            //                    if (Global.InfoOn)
            //                    {
            //                        for (int infoMeter = 1; infoMeter <= Global.InfoLoopCount; infoMeter++)
            //                        {
            //                            klog.Info(Generator.Execute(Global.InfoCharCount));
            //                        }
            //                    }

            //                    // Warning
            //                    if (Global.WarningOn)
            //                    {
            //                        for (int warningMeter = 1; warningMeter <= Global.WarningLoopCount; warningMeter++)
            //                        {
            //                            klog.Warning(Generator.Execute(Global.WarningCharCount));
            //                        }
            //                    }

            //                    // Error
            //                    if (Global.ErrorOn)
            //                    {
            //                        for (int errorMeter = 1; errorMeter <= Global.ErrorLoopCount; errorMeter++)
            //                        {
            //                            klog.Error(Generator.Execute(Global.ErrorCharCount));
            //                        }
            //                    }

            //                    klog.Success();
            //                }
            //                catch (Exception e)
            //                {
            //                    klog.Error($"KFlow Exception: {e.ToString()}");
            //                    klog.Failure();
            //                }
            //            }
            //        }
            //    }

            //    KManager.Offline();
            //}

            //#endregion

            var items = new string[] { "Task1", "Task2", "Task3", "Task4" };
            var tasks = new Task[items.Length];
            var taskCounter = 0;

            foreach (var item in items)
            {
                tasks[taskCounter] = new Task(() => DynamicLog(item));

                taskCounter++;
            }

            Parallel.ForEach(tasks, (t) => { t.Start(); });
            Task.WaitAll(tasks);

            Console.ReadKey();
        }

        /// <summary>
        /// Test method for dynamic KLOG creation.
        /// </summary>
        /// <param name="instanceIteration"></param>
        public static void DynamicLog(string instanceIteration)
        {
            using (KLog klog = new KLog($"Dynamic KFlow -- Primary Node -- {instanceIteration}"))
            {
                klog.Info("Testing primary block");

                using (KLog nestedKLog = new KLog($"Dynamic KFlow -- Nested Node -- {instanceIteration}", klog))
                {
                    nestedKLog.Info("Testing nested block");
                }
            }
        }
    }
}