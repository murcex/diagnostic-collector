namespace KirokuG2
{
	using KirokuG2.Internal;

	public interface IKLog
	{
		void Dispose();
		void Error(string data);
		void Info(string data);
		void Metric(string key, bool value);
		void Metric(string key, double value);
		void Metric(string key, float value);
		void Metric(string key, int value);
		void Metric(string key, string value);
		KBlock NewBlock(string name);
		void Trace(string data);
	}
}