using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
    public static class ConsoleDisplay
    {
        public static void DisplayTargetSettings()
        {
            Console.WriteLine("\t<!> Target Settings");

            if (Global.Type == 1)
            {
                Console.WriteLine("\n\t\t-> Instance: {0}", Global.Instance);
                Console.WriteLine("\t\t-> Database: {0}", Global.Database);
                Console.WriteLine("\t\t-> Account: {0}", Global.Account);
            }
            else
            {
                Console.WriteLine("\n\t\t-> Instance: LocalHost");
                Console.WriteLine("\t\t-> Database: {0}", Global.Database);
            }

            Console.WriteLine("\n");
            Console.Write("\t<!> DEPLOYMENT (Y/N) -> ");
        }
    }
}
