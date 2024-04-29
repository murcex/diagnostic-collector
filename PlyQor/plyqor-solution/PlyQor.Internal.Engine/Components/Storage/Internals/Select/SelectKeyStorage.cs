namespace PlyQor.Engine.Components.Storage.Internals
{
	using Microsoft.Data.SqlClient;
	using PlyQor.Engine.Core;
	using PlyQor.Models;
	using PlyQor.Resources;
	using System;

	public class SelectKeyStorage
	{
		public static string Execute(
			string container,
			string id)
		{
			string data = string.Empty;

			try
			{
				using (var connection = new SqlConnection(Configuration.DatabaseConnection))
				{
					var cmd = new SqlCommand(SqlValues.SelectKeyStorage, connection);

					cmd.CommandType = System.Data.CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue(SqlValues.Container, container);
					cmd.Parameters.AddWithValue(SqlValues.Id, id);

					cmd.CommandTimeout = 0;

					connection.Open();

					var reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						data = (string)reader[SqlValues.Data];
					}

					return data;
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
