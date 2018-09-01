using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Crane
{
	class Global
	{
		// Set Deployment Variables
		public static readonly DateTime Session = DateTime.Now.ToUniversalTime();
		public static readonly String Build = ConfigurationManager.AppSettings["build"].ToString();
		public static readonly String Database = ConfigurationManager.AppSettings["database"].ToString();
		public static readonly String Instance = ConfigurationManager.AppSettings["instance"].ToString();
		public static readonly String Account = ConfigurationManager.AppSettings["account"].ToString();
		public static readonly String Key = ConfigurationManager.AppSettings["key"].ToString();

		// Create SQL Connection String
		public static readonly String SQLConnectionString = "Server=tcp:" + Global.Instance + ",1433; Initial Catalog=" + Global.Database + "; Persist Security Info=False; User ID=" + Global.Account + "; Password=" + Global.Key + "; MultipleActiveResultSets=False;";

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
