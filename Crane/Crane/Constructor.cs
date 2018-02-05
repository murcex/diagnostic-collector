using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Crane
{
	class Constructor
	{
		public static void Execute()
		{
			foreach (KeyValuePair<int, string> objectType in Global.ObjectType)
			{
				//TODO: Add Try/Catch
				string[] fileEntries = Directory.GetFiles(Global.Build + @"\" + objectType.Value);

				foreach (string fileName in fileEntries)
				{
					Console.WriteLine("\t-> '{0}'", Path.GetFileName(fileName));

					//TODO: Add Try/Catch
					string payLoad = System.IO.File.ReadAllText(fileName);

					Injector.Execute(payLoad);
				}
			}
		}
	}
}
