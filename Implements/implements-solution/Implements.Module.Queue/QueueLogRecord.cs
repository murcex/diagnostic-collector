namespace Implements.Module.Queue
{
	public class QueueLogRecord
	{
		public DateTime Timestamp { get; protected set; }

		public string Key { get; protected set; }

		public string Value { get; protected set; }

		public string Instance { get; protected set; }

		public int Type { get; protected set; }

		public QueueLogRecord(DateTime timestamp, string key)
		{
			this.Timestamp = timestamp;
			this.Key = key;
			this.Value = string.Empty;
			this.Instance = string.Empty;
			this.Type = 1;
		}

		public QueueLogRecord(DateTime timestamp, string key, string value)
		{
			this.Timestamp = timestamp;
			this.Key = key;
			this.Value = value;
			this.Instance = string.Empty;
			this.Type = 2;
		}

		public QueueLogRecord(DateTime timestamp, string key, string value, string id)
		{
			this.Timestamp = timestamp;
			this.Key = key;
			this.Value = value;
			this.Instance = id;
			this.Type = 3;
		}
	}
}
