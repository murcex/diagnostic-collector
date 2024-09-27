namespace Implements.Module.Queue
{
	public class QueueLogRecord
	{
		/// <summary>
		/// Gets or sets the timestamp of the log record.
		/// </summary>
		public DateTime Timestamp { get; protected set; }

		/// <summary>
		/// Gets or sets the key of the log record.
		/// </summary>
		public string Key { get; protected set; }

		/// <summary>
		/// Gets or sets the value of the log record.
		/// </summary>
		public string Value { get; protected set; }

		/// <summary>
		/// Gets or sets the instance of the log record.
		/// </summary>
		public string Instance { get; protected set; }

		/// <summary>
		/// Gets or sets the type of the log record.
		/// </summary>
		public int Type { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the QueueLogRecord class with the specified timestamp and key.
		/// </summary>
		/// <param name="timestamp">The timestamp of the log record.</param>
		/// <param name="key">The key of the log record.</param>
		public QueueLogRecord(DateTime timestamp, string key)
		{
			this.Timestamp = timestamp;
			this.Key = key;
			this.Value = string.Empty;
			this.Instance = string.Empty;
			this.Type = 1;
		}

		/// <summary>
		/// Initializes a new instance of the QueueLogRecord class with the specified timestamp, key, and value.
		/// </summary>
		/// <param name="timestamp">The timestamp of the log record.</param>
		/// <param name="key">The key of the log record.</param>
		/// <param name="value">The value of the log record.</param>
		public QueueLogRecord(DateTime timestamp, string key, string value)
		{
			this.Timestamp = timestamp;
			this.Key = key;
			this.Value = value;
			this.Instance = string.Empty;
			this.Type = 2;
		}

		/// <summary>
		/// Initializes a new instance of the QueueLogRecord class with the specified timestamp, key, value, and instance.
		/// </summary>
		/// <param name="timestamp">The timestamp of the log record.</param>
		/// <param name="key">The key of the log record.</param>
		/// <param name="value">The value of the log record.</param>
		/// <param name="id">The instance of the log record.</param>
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
