namespace Implements.Audit
{
    using Implements.Log;
    using Implements.Utility;

    class LogAudit
    {
        public static void Execute(bool execute)
        {
            if (!execute)
            {
                return;
            }

            var curImplVer = Conversion.GetAssemblyVersion("Implements");

            ///
            /// Logging
            ///

            var cfg = Log.GenerateConfig();
            cfg.LogName = "ExampleLog";

            Log.Initialize();

            Log.Info($"Current Implement.dll Version: {curImplVer}");
            Log.Info("");
        }
    }
}
