using KirokuG2.Loader;

namespace KirokuG2.Internal.Loader.Interface
{
	public interface ISQLProvider
	{
		bool Initialized(string sqlConnection);
		bool InsertActivation(DateTime session, string record_id, string source);
		bool InsertBlock(LogBlock logBlock);
		bool InsertCritical(LogError logError);
		bool InsertError(LogError logError);
		bool InsertInstance(LogInstance logInstance);
		bool InsertMetric(LogMetric logMetric);
		bool InsertQuarantine(DateTime session, string record_id);
	}
}