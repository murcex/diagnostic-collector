namespace KLOGCopy
{
    // Kiroku Logging Library
    using Kiroku;

    class Program
    {
        static void Main(string[] args)
        {
            // Start instance level logging
            Global.StartLogging();

            // Log global properties
            using (KLog logConfig = new KLog("ClassProgram-LogicConfig"))
            {
                logConfig.Info($"Config Local Directory: {Global.LocalDirectory}");
                logConfig.Info($"Config Azure Container: {Global.AzureContainer}");
                logConfig.Info($"Config Retention: {Global.RetentionDays}");
                logConfig.Info($"Config Cleanse: {Global.CleanseHours}");
            }

            // Extract all files names
            Capsule.AddLogFiles(CollectLogs.Execute(Global.LocalDirectory));

            // Process each IEnum filter group in their appropriate action method
            DeleteLogs.Execute();

            SendLogs.Execute();

            CleanseLogs.Execute();

            // End instance level logging
            Global.StopLogging();

            Global.CheckDebug();
        }
    }
}
