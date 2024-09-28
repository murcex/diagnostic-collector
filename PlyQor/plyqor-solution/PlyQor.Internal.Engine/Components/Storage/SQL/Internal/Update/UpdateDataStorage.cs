using Microsoft.Data.SqlClient;
using PlyQor.Engine.Core;
using PlyQor.Internal.Engine.Components.Storage.SQL.Internal.Utilities;
using PlyQor.Models;
using PlyQor.Resources;
using System;
using System.Data;

namespace PlyQor.Internal.Engine.Components.Storage.SQL.Internal.Update
{
	class UpdateDataStorage
	{
		public static int Execute(
			string container,
			string id,
			string data)
		{
			try
			{
				using (var connection = new SqlConnection(Configuration.DatabaseConnection))
				{
					var cmd = new SqlCommand(SqlValues.UpdateDataStorage, connection);

					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue(SqlValues.Container, container);
					cmd.Parameters.AddWithValue(SqlValues.Id, id);
					cmd.Parameters.AddWithValue(SqlValues.Data, data);

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
