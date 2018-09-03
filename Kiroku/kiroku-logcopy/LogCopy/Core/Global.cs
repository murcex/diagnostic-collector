using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace KLOGCopy
{
    public static class Global
    {
        public static readonly String Debug = ConfigurationManager.AppSettings["debug"].ToString();
        public static readonly String LocalDirectory = ConfigurationManager.AppSettings["localDir"].ToString();
        public static readonly String AzureContainer = ConfigurationManager.AppSettings["azureContainer"].ToString();
        static string _retentionDays = ConfigurationManager.AppSettings["retentionDays"].ToString();
        static string _cleanseHours = ConfigurationManager.AppSettings["cleanseHours"].ToString();
        public static readonly Double RetentionDays = Convert.ToDouble(_retentionDays);
        public static readonly Double CleanseHours = Convert.ToDouble(_cleanseHours);

        public static void CheckDebug()
        {
            if (Debug == "1")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\tDEBUG DETECTED, PRESS ANY KEY");
                Console.ReadKey();
            }
        }
    }
}
