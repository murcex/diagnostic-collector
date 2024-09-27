namespace Implements.Module.Queue
{
	/// <summary>
	/// Represents a class for parsing and processing queue log data.
	/// </summary>
	public class QueueLoglizer
	{
		/// <summary>
		/// Executes the parsing of a single input string and returns a QueueLogRecord object.
		/// </summary>
		/// <param name="input">The input string to parse.</param>
		/// <returns>A QueueLogRecord object.</returns>
		public static QueueLogRecord Execute(string input)
		{
			return ParseInput(input);
		}

		/// <summary>
		/// Executes the parsing of a list of input strings and returns a list of QueueLogRecord objects.
		/// </summary>
		/// <param name="inputs">The list of input strings to parse.</param>
		/// <returns>A list of QueueLogRecord objects.</returns>
		public static List<QueueLogRecord> Execute(List<string> inputs)
		{
			List<QueueLogRecord> records = new();

			foreach (string input in inputs)
			{
				records.Add(ParseInput(input));
			}

			return records;
		}

		/// <summary>
		/// Parses the input string and returns a QueueLogRecord object.
		/// </summary>
		/// <param name="input">The input string to parse.</param>
		/// <returns>A QueueLogRecord object.</returns>
		private static QueueLogRecord ParseInput(string input)
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

		/// <summary>
		/// Converts the input string to a DateTime object.
		/// </summary>
		/// <param name="input">The input string to convert.</param>
		/// <returns>A DateTime object.</returns>
		private static DateTime AsDateTime(string input)
		{
			DateTime.TryParse(input, out var dt);
			return dt;
		}
	}
}
