using System;
using System.Collections.Generic;
using System.IO;

namespace Crane
{
    /// <summary>
    /// 
    /// </summary>
    class Constructor
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Execute()
        {
            foreach (KeyValuePair<int, string> objectType in Global.ObjectType)
            {
                try
                {
                    string[] fileEntries = Directory.GetFiles(Global.Build + @"\" + objectType.Value);

                    foreach (string fileName in fileEntries)
                    {
                        Console.WriteLine("\t-> '{0}'", Path.GetFileName(fileName));
                        Log.Info($"\r\nExecuting File: '{Path.GetFileName(fileName)}'");

                        try
                        {
                            var payLoad = System.IO.File.ReadAllText(fileName);

                            var result = Injector.Execute(payLoad);

                            //if (result.Contains("Success"))
                            //{
                            //    Log.Info($"File Name => {result}");
                            //}
                            //else if (result.Contains("Failure (SQL Exception):"))
                            //{
                            //    Log.Error($"File Name => {result}");
                            //}
                        }
                        catch (Exception e)
                        {
                            Log.Error($"Read and Execute Payload Exception: {e.ToString()}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Read Directory Exception: {e.ToString()}");
                }
            }
        }
    }
}
