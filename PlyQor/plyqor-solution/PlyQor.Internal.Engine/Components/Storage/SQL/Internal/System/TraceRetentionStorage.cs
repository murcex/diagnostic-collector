using Microsoft.Data.SqlClient;
using PlyQor.Engine.Core;
using PlyQor.Internal.Engine.Components.Storage.SQL.Internal.Utilities;
using PlyQor.Models;
using PlyQor.Resources;
using System;
using System.Data;

namespace PlyQor.Internal.Engine.Components.Storage.SQL.Internal.System
{
	public class TraceRetentionStorage
	{
		public static int Execute(int days)
		{
			try
			{
				using (var connection = new SqlConnection(Configuration.DatabaseConnection))
				{
					var cmd = new SqlCommand(SqlValues.TraceRetentionStorage, connection);

					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue(SqlValues.Days, days);

					cmd.CommandTimeout = 0;

					connection.Open();

					var reader = cmd.ExecuteReader();
					while (reader.Read())
					{ }

					var recordCount = reader.RecordsAffected;

					return recordCount;
				}
			}
			catch (Exception ex)
			{
				if (ex is SqlException)
				{
					SqlExceptionCheck.Execute(ex);
				}

				throw new PlyQorException(StatusCode.ERR010, ex);
			}
		}
	}
}
