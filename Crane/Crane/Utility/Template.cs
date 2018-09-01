using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Crane
{
    class Template
    {
        public static void Build()
        {
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

                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(pathObject);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(pathObject));
                }
            }
        }
    }
}
