namespace Implements.Audit
{
    using System;
    using Implements.Log;
    using Implements.Utility;

    class LogAudit
    {
        public static void Execute(bool execute)
        {
            if (!execute)
            {
                Console.WriteLine($"Audit: {nameof(LogAudit)} has not been marked for execution, skipping audit.");
                Console.WriteLine($"*** PRESS ANY KEY TO CONTINUE *** \r\n");
                Console.ReadKey();

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
