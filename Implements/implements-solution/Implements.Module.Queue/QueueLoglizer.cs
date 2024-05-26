namespace Implements.Module.Queue
{
	public class QueueLoglizer
	{
		public QueueLogRecord Execute(string input)
		{
			return ParseRawLogInput(input);
		}

		public List<QueueLogRecord> Execute(List<string> inputs)
		{
			List<QueueLogRecord> records = new();

			foreach (string input in inputs)
			{
				records.Add(ParseRawLogInput(input));
			}

			return records;
		}

		private QueueLogRecord ParseRawLogInput(string input)
		{
			var inputs = input.Split(',');

			var timestamp = DateTime.MinValue;
			var key = string.Empty;
			var value = string.Empty;
			var id = string.Empty;

			foreach (var record in inputs)
			{
				var lookup = record.Split("=");
				var type = lookup[0];
				var data = lookup[1];

				switch (type)
				{
					case "t":
						timestamp = AsDateTime(data);
						break;
					case "k":
						key = data;
						break;
					case "v":
						value = data;
						break;
					case "i":
						id = data;
						break;
				}
			}

			if (string.IsNullOrEmpty(value))
			{
				return new QueueLogRecord(timestamp, key);
			}
			else if (string.IsNullOrEmpty(id))
			{
				return new QueueLogRecord(timestamp, key, value);
			}
			else
			{
				return new QueueLogRecord(timestamp, key, value, id);
			}
		}

		private DateTime AsDateTime(string input)
		{
			DateTime.TryParse(input, out var dt);
			return dt;
		}
	}
}
