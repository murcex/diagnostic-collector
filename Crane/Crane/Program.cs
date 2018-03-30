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
            bool operate = true;
            string loopCheck;

            while (operate)
            {
                #region Console Display

                //TODO: Move to Class
                Console.WriteLine("\n  Crane : SQL Server Database Deployment Tool\n");
                Console.WriteLine("\t<!> Deployment Settings");
                Console.WriteLine("\n\t\t-> Source: {0}", Global.Build);
                Console.WriteLine("\t\t-> Type: Azure SQL Server");
                Console.WriteLine("\n");
                Console.WriteLine("\t<!> Target Settings");
                Console.WriteLine("\n\t\t-> Instance: {0}", Global.Instance);
                Console.WriteLine("\t\t-> Database: {0}", Global.Database);
                Console.WriteLine("\t\t-> Account: {0}", Global.Account);
                Console.WriteLine("\n");
                Console.Write("\t<!> DEPLOYMENT (Y/N) -> ");

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

                Constructor.Execute();

                #endregion

                // Application Complete
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\t--- Complete ---");
                Console.ResetColor();
                Console.WriteLine("\t\t<!> Log: <addlocationhere>");

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
            }
		}
	}
}
