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
        public static readonly string _type = ConfigurationManager.AppSettings["type"].ToString();
        public static readonly string Build = ConfigurationManager.AppSettings["build"].ToString();
		public static readonly string Database = ConfigurationManager.AppSettings["database"].ToString();
		public static readonly string Instance = ConfigurationManager.AppSettings["instance"].ToString();
		public static readonly string Account = ConfigurationManager.AppSettings["account"].ToString();
		public static readonly string Key = ConfigurationManager.AppSettings["key"].ToString();
        public static string SQLConnectionString;
        public static int Type;
        public static string TypeName;

        // Create Azure SQL Server Connection String
        private static readonly String _azureConnectionString = "Server=tcp:" + Global.Instance + ",1433; Initial Catalog=" + Global.Database + "; Persist Security Info=False; User ID=" + Global.Account + "; Password=" + Global.Key + "; MultipleActiveResultSets=False;";

        // Create Local SQL Server Connection String
        private static readonly String _localConnectionString = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=" + Global.Database + "; Data Source=localhost";

        public static void Set()
        {
            if (_type == "1")
            {
                SQLConnectionString = _azureConnectionString;
                Type = 1;
                TypeName = "Azure SQL Server";
            }
            else
            {
                SQLConnectionString = _localConnectionString;
                Type = 0;
                TypeName = "Local SQL Server";
            }
        }

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
