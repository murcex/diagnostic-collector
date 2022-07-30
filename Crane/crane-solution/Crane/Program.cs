using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane
{
	class Program
	{
		static void Main(string[] args)
		{
            Global.Set();

            bool operate = true;
            string loopCheck;

            while (operate)
            {
                #region Console Display

                //TODO: Move to Class
                Console.WriteLine("\n  Crane : SQL Server Database Deployment Tool\n");
                Console.WriteLine("\t<!> Deployment Settings");
                Console.WriteLine("\n\t\t-> Source: {0}", Global.Build);
                Console.WriteLine("\t\t-> Type: {0}", Global.TypeName);
                Console.WriteLine("\n");

                ConsoleDisplay.DisplayTargetSettings();

                // Check Y/N Deployment
                string deployCheck = Console.ReadLine();

                deployCheck = deployCheck.ToUpper();

                if (deployCheck != "Y")
                {
                    Console.Clear();
                    Console.WriteLine("\n\t--- Closing ---");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }

                Console.Clear();
                Console.WriteLine("\n\t--- Deploying ---\n");

                #endregion

                #region Constructor

                Log.CreateLog();
                Constructor.Execute();

                #endregion

                #region Closing / Reloop

                // Application Complete
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\t--- Complete ---");
                Console.ResetColor();
                Console.WriteLine("\n\t\t<!> Log: {0}", Log.GetLogFileName());

                // Loop Check
                Console.Write("\n\t<!> LOOP? (Y/N) -> ");

                // Check Y/N Loop
                loopCheck = Console.ReadLine();
                loopCheck = loopCheck.ToUpper();

                if (loopCheck != "Y")
                {
                    Console.WriteLine("\n\t--- Closing ---");
                    System.Threading.Thread.Sleep(2000);
                    Console.Clear();
                    Environment.Exit(0);
                }

                else
                {
                    Console.WriteLine("\n\t--- Looping ---");
                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
                
                #endregion
            }
        }
	}
}
