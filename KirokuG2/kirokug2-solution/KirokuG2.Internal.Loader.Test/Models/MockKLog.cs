namespace KirokuG2.Internal.Loader.Test.Models
{
	public class MockKLog : IKLog
	{
		private List<string> _logs;

		public MockKLog(List<string> logs)
		{
			_logs = logs;
		}

		public void Dispose()
		{
			_logs.Add("Disposed");
		}

		public void Error(string data)
		{
			_logs.Add($"Error: {data}");
		}

		public void Info(string data)
		{
			_logs.Add($"Info: {data}");
		}

		public void Metric(string key, bool value)
		{
			_logs.Add($"Metric: {key},{value}");
		}

		public void Metric(string key, double value)
		{
			_logs.Add($"Metric: {key},{value}");
		}

		public void Metric(string key, float value)
		{
			_logs.Add($"Metric: {key},{value}");
		}

		public void Metric(string key, int value)
		{
			_logs.Add($"Metric: {key},{value}");
		}

		public void Metric(string key, string value)
		{
			_logs.Add($"Metric: {key},{value}");
		}

		public KBlock NewBlock(string name)
		{
			_logs.Add($"NewBlock: {name}");

			throw new("");
		}

		public void Trace(string data)
		{
			_logs.Add($"Trace: {data}");
		}
	}
}
