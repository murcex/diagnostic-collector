using Crane.Internal.Engine.SQLDatabaseDeployment.SQLDatabaseDeployment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane.Internal.Engine.SQLDatabaseDeployment
{
    public class SQLDatabaseDeploymentType
    {
        public void Execute(Dictionary<string, Dictionary<string, string>> script)
        {
            // get all the values from cfg
            var buildDir = string.Empty;

            // build connection string
            var connectionString = string.Empty;

            // build sql executor
            SQLAccess sQLAccess = new SQLAccess(connectionString);

            // foreach enum type
            foreach (var sqlObjectType in Enum.GetNames(typeof(SqlObjectType)))
            {
                try
                {
                    string[] fileEntries = Directory.GetFiles(buildDir + @"\" + sqlObjectType);

                    foreach (var fileName in fileEntries)
                    {
                        Console.WriteLine("\t-> '{0}'", Path.GetFileName(fileName));
                        //Log.Info($"\r\nExecuting File: '{Path.GetFileName(fileName)}'");

                        try
                        {
                            var sqlScript = System.IO.File.ReadAllText(fileName);

                            var result = sQLAccess.Execute(sqlScript);
                        }
                        catch (Exception e)
                        {
                            //Log.Error($"Read and Execute Payload Exception: {e.ToString()}");
                        }
                    }
                }
                catch (Exception e)
                {
                    //Log.Error($"Read Directory Exception: {e.ToString()}");
                }
            }
        }
    }
}
