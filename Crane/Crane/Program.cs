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
			Console.WriteLine("\n\t--- Complete ---");
			Console.WriteLine("\t\t<!> Log: <addlocationhere>");
			Console.ReadKey();

			// Close Application
			Environment.Exit(0);
		}
	}
}
