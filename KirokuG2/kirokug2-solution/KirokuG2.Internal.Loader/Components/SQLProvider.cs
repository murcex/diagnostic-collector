namespace KirokuG2.Loader.Components
{
	using KirokuG2.Internal.Loader.Interface;
	using KirokuG2.Loader.Components.Internal;

	public class SQLProvider : ISQLProvider
	{
		private static string _sqlConnection;

		public bool Initialized(string sqlConnection)
		{
			_sqlConnection = sqlConnection;

			return true;
		}

		public bool InsertInstance(LogInstance logInstance)
		{
			InsertInstanceOperation.Execute(logInstance, _sqlConnection);

			return true;
		}

		public bool InsertBlock(LogBlock logBlock)
		{
			InsertBlockOperation.Execute(logBlock, _sqlConnection);

			return true;
		}

		public bool InsertError(LogError logError)
		{
			InsertErrorOperation.Execute(logError, _sqlConnection);

			return true;
		}

		public bool InsertMetric(LogMetric logMetric)
		{
			InsertMetricOperation.Execute(logMetric, _sqlConnection);

			return true;
		}

		public bool InsertActivation(DateTime session, string record_id, string source)
		{
			InsertActivationOperation.Execute(session, record_id, source, _sqlConnection);

			return true;
		}

		public bool InsertCritical(LogError logError)
		{
			InsertCriticalOperation.Execute(logError, _sqlConnection);

			return true;
		}

		public bool InsertQuarantine(DateTime session, string record_id)
		{
			InsertQuarantineOperation.Execute(session, record_id, _sqlConnection);

			return true;
		}
	}
}
