namespace Crane.Application.Utility
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            FileManager fileManager = new FileManager();
            ScriptExecutor executor = new ScriptExecutor();

            // load Config.ini
            var craneCfg = fileManager.LoadCraneConfig();

            // read script.ini
            var scriptCfg = fileManager.LoadCraneScript(craneCfg);

            // switch type
            executor.Execute(scriptCfg);

            // execute cfg with type
        }
    }
}