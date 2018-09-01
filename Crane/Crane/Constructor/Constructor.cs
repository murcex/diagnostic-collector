using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// Kiroku Logging
using Kiroku;

namespace Crane
{
	class Constructor
	{
		public static void Execute()
		{
            using (KLog log = new KLog("ClassConstructor-MethodExecute"))
            {
                foreach (KeyValuePair<int, string> objectType in Global.ObjectType)
                {
                    //TODO: Add Try/Catch
                    try
                    {
                        string[] fileEntries = Directory.GetFiles(Global.Build + @"\" + objectType.Value);

                        foreach (string fileName in fileEntries)
                        {
                            Console.WriteLine("\t-> '{0}'", Path.GetFileName(fileName));
                            log.Info($"File Name: '{Path.GetFileName(fileName)}'");

                            //TODO: Add Try/Catch
                            try
                            {
                                var payLoad = System.IO.File.ReadAllText(fileName);

                                var result = Injector.Execute(payLoad);

                                if (result.Contains("Success"))
                                {
                                    log.Info($"File Name => {result}");
                                }
                                else if (result.Contains("Failure (SQL Exception):"))
                                {
                                    log.Error($"File Name => {result}");
                                }
                            }
                            catch (Exception e)
                            {
                                log.Error(e.ToString());
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error(e.ToString());
                    }
                }
            }
		}
	}
}
