using Implements.Function.Queue.Target.Core;
using Microsoft.Data.SqlClient;
using System;

namespace Implements.Function.Queue.Target.Components
{
	public class SQLStorage
	{
		public static bool UpdateRecord(string id)
		{
			// update record sent = 1
			try
			{
				var query = $"UPDATE [dbo].[tbl_AxQueue_Tracking] SET i_updated = 1 WHERE nvc_id = '{id}';";

				using (var connection = new SqlConnection(Configuration.Database))
				{
					connection.Open();

					using (var command = new SqlCommand(query, connection))
					{
						command.CommandTimeout = 0;

						var reader = command.ExecuteReader();

						var recordCount = reader.RecordsAffected;

						while (reader.Read())
						{ }
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
