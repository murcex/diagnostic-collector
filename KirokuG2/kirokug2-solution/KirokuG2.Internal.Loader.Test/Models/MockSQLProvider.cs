using KirokuG2.Internal.Loader.Interface;
using KirokuG2.Loader;

namespace KirokuG2.Internal.Loader.Test.Mocks
{
    public class MockSQLProvider : ISQLProvider
    {
        private Dictionary<string, string> _tracker;

        public MockSQLProvider(Dictionary<string, string> tracker)
        {
            _tracker = tracker;
        }

        public bool Initialized(string sqlConnection)
        {
            return AddValueToTracker("Initialized", $"{sqlConnection}");
        }

        public bool InsertActivation(DateTime session, string record_id, string source)
        {
            return AddValueToTracker("Activation", $"{session},{record_id},{source}");
        }

        public bool InsertBlock(LogBlock logBlock)
        {
            return AddValueToTracker("Block", $"{logBlock.Id},{logBlock.Name},{logBlock.Tag},{logBlock.Duration}");
        }

        public bool InsertCritical(LogError logError)
        {
            return AddValueToTracker("Critical", $"{logError.Timestamp},{logError.Source},{logError.Function},{logError.Id},{logError.Message}");
        }

        public bool InsertError(LogError logError)
        {
            return AddValueToTracker("Error", $"{logError.Timestamp},{logError.Source},{logError.Function},{logError.Id},{logError.Message}");
        }

        public bool InsertInstance(LogInstance logInstance)
        {
            return AddValueToTracker("Instance", $"{logInstance.Id},{logInstance.Source},{logInstance.Function},{logInstance.Errors},{logInstance.Duration},{logInstance}");
        }

        public bool InsertMetric(LogMetric logMetric)
        {
            return AddValueToTracker("Metric", $"{logMetric.Id},{logMetric.Source},{logMetric.Function},{logMetric.Type},{logMetric.Key},{logMetric.Value}");
        }

        public bool InsertQuarantine(DateTime session, string record_id)
        {
            return AddValueToTracker("Quarantine", $"{session},{record_id}");
        }

        private bool AddValueToTracker(string group, string value)
        {
            _tracker.Add($"{group}-{Guid.NewGuid()}", value);

            return true;
        }
    }
}
