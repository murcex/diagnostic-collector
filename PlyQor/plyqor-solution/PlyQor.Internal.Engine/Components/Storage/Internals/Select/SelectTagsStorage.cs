namespace PlyQor.Engine.Components.Storage.Internals
{
	using Microsoft.Data.SqlClient;
	using PlyQor.Engine.Core;
	using PlyQor.Models;
	using PlyQor.Resources;
	using System;
	using System.Collections.Generic;

	public class SelectTagsStorage
	{
		public static List<string> Execute(string container)
		{
			List<string> indexes = new List<string>();

			try
			{
				using (var connection = new SqlConnection(Configuration.DatabaseConnection))
				{
					var cmd = new SqlCommand(SqlValues.SelectTagsStorage, connection);

					cmd.CommandType = System.Data.CommandType.StoredProcedure;

					cmd.Parameters.AddWithValue(SqlValues.Container, container);

					cmd.CommandTimeout = 0;

					connection.Open();

					var reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						var index = (string)reader[SqlValues.Data];

						indexes.Add(index);
					}

					return indexes;
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
