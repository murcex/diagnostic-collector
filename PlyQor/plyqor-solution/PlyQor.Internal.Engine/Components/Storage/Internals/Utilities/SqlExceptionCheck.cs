namespace PlyQor.Engine.Components.Storage.Internals
{
	using PlyQor.Models;
	using PlyQor.Resources;
	using System;

	class SqlExceptionCheck
	{
		public static void Execute(Exception ex)
		{
			if (ex.Message.Contains(SqlValues.RequestLimit))
			{
				throw new PlyQorException(StatusCode.ERR012, ex);
			}

			if (ex.Message.Contains(SqlValues.PrimaryKeyConstraint))
			{
				throw new PlyQorException(StatusCode.ERR013, ex);
			}

			throw new PlyQorException(StatusCode.ERR014, ex);
		}
	}
}
