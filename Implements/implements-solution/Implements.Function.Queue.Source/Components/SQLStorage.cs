using Implements.Function.Queue.Source.Core;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Implements.Function.Queue.Source.Components
{
	public class SQLStorage
	{
		public static bool InsertRecord(string id)
		{
			// insert record of id, sent & updated = 0
			try
			{
				var query = $"INSERT INTO [dbo].[tbl_AxQueue_Tracking] (dt_timestamp, nvc_id, i_sent, i_updated) VALUES('{DateTime.UtcNow}', '{id}', 0, 0)";

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

		public static bool BulkInsertRecords(List<string> samples)
		{
			try
			{
				var timestamp = DateTime.UtcNow;

				// Create a DataTable with columns matching the destination table
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("dt_timestamp", typeof(DateTime));
				dataTable.Columns.Add("nvc_id", typeof(string));
				dataTable.Columns.Add("i_sent", typeof(int));
				dataTable.Columns.Add("i_update", typeof(int));

				// Add rows to the DataTable
				foreach (var sample in samples)
				{
					dataTable.Rows.Add(timestamp, sample, 0, 0);
				}

				// Initialize SqlBulkCopy
				using (SqlConnection connection = new SqlConnection(Configuration.Database))
				{
					connection.Open();
					using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
					{
						bulkCopy.DestinationTableName = "tbl_AxQueue_Tracking"; // Specify your table name
						bulkCopy.WriteToServer(dataTable);
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return false;
			}
		}

		public static bool UpdateRecord(string id)
		{
			// update record sent = 1
			try
			{
				var query = $"UPDATE [dbo].[tbl_AxQueue_Tracking] SET i_sent = 1 WHERE nvc_id = '{id}';";

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

		public static bool Retention()
		{
			// delete older than 7 days
			try
			{
				var query = $"DELETE FROM [tbl_AxQueue_Tracking] WHERE [dt_timestamp] < DATEADD(day,-7,GETDATE())";

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
			catch { return false; }
		}
	}
}
