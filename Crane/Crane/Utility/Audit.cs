using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Crane
{
    class Audit
    {
        public static void Verify()
        {
            // TODO: check source folders are available
            string path = Directory.GetCurrentDirectory();

            foreach (KeyValuePair<int, string> objectType in Global.ObjectType)
            {
                string pathObject = path + @"\" + objectType.Value;

                // create folder struct
                // Determine whether the directory exists.
                if (Directory.Exists(pathObject))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }
            }

            // TODO: check sql instance connection + user prem
        }
    }
}
