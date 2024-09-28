using Microsoft.Data.SqlClient;
using PlyQor.Engine.Core;
using PlyQor.Internal.Engine.Components.Storage.SQL.Internal.Utilities;
using PlyQor.Models;
using PlyQor.Resources;
using System;
using System.Data;

namespace PlyQor.Internal.Engine.Components.Storage.SQL.Internal.Insert
{
	class InsertTagStorage
	{
		public static int Execute(
			DateTime timestamp,
			string container,
			string id,
			string data)
		{
			try
			{
				using (var connection = new SqlConnection(Configuration.DatabaseConnection))
				{
					var cmd = new SqlCommand(SqlValues.InsertTagStorage, connection);

					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue(SqlValues.TimeStamp, timestamp);
					cmd.Parameters.AddWithValue(SqlValues.Container, container);
					cmd.Parameters.AddWithValue(SqlValues.Id, id);
					cmd.Parameters.AddWithValue(SqlValues.Data, data);

					cmd.CommandTimeout = 0;

					connection.Open();

					var reader = cmd.ExecuteReader();

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
