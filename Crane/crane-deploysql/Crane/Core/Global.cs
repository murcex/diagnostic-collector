using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Crane
{
    /// <summary>
    /// 
    /// </summary>
	class Global
	{
        // Set Deployment Variables
        static readonly string _session = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        public static readonly string Build = ConfigurationManager.AppSettings["build"].ToString();
		public static readonly string Database = ConfigurationManager.AppSettings["database"].ToString();
		public static readonly string Instance = ConfigurationManager.AppSettings["instance"].ToString();
		public static readonly string Account = ConfigurationManager.AppSettings["account"].ToString();
		public static readonly string Key = ConfigurationManager.AppSettings["key"].ToString();

        // Create SQL Connection String
        public static readonly String SQLConnectionString = "Server=tcp:" + Global.Instance + ",1433; Initial Catalog=" + Global.Database + "; Persist Security Info=False; User ID=" + Global.Account + "; Password=" + Global.Key + "; MultipleActiveResultSets=False;";

        // Create Full Log Path
        public static readonly string FullLogPath = Directory.GetCurrentDirectory() + @"\CraneLog-" + _session + ".txt";

        // Create Object Type Dictionary
        public static readonly Dictionary<int, string> ObjectType = new Dictionary<int, string>
		{
			{1, "table"},
			{2, "proc"},
			{3, "view"},
			{4, "security"}
		};
	}
}
