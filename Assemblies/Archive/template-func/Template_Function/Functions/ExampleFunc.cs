namespace ExampleApp
{
    using System;
    using Microsoft.Extensions.Logging;
    using ExampleModule;

    class ExampleFunc
    {

        // Main stub -- required to build template
        public static void Main()
        {

        }

        //[FunctionName("ExampleFunc")]
        //public static void Execute([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        public static void Execute(ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation($"Config Status: {Configuration.ConfigStatus("KCopyApp")}");

            try
            {
                ExampleAppManager.Execute();
            }
            catch (Exception ex)
            {
                log.LogInformation(ex.ToString());
            }
        }
    }
}