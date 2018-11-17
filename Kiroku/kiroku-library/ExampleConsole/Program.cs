namespace KFlow
{
    using System;
    using System.Configuration;
    using System.Collections.Specialized;

    // Kiroku Logging Library
    using Kiroku;

    class Program
    {
        static void Main(string[] args)
        {
            Global.SetValues();

            for (int i = 1; i <= Global.InstanceLoop; i++)
            {
                KManager.Online((NameValueCollection)ConfigurationManager.GetSection("Kiroku"));

                for (int y = 1; y <= Global.BlockLoop; y++)
                {
                    using (KLog klog = new KLog($"Block-{i}-{y}"))
                    {
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
                            }
                            catch (Exception e)
                            {
                                klog.Error($"KFlow Exception: {e.ToString()}");
                            }
                        }
                    }
                }

                KManager.Offline();
            }
        }
    }
}