namespace KirokuG2.Loader
{
	public class LogBlock
	{
		public string Id { get; }

		public string Tag { get; }

		public DateTime Start { get; }

		public DateTime Stop { get; private set; }

		public string Name { get; }

		public int Duration => _duration;

		private int _duration;

		public bool Result => _result;

		private bool _result;

		public LogBlock(DateTime start, string id, string tag, string name)
		{
			Id = id;
			Tag = tag;
			Start = start;
			Name = name;
		}

		public void StopBlock(DateTime stop)
		{
			Stop = stop;

			if (Start != DateTime.MinValue && Stop != DateTime.MinValue)
			{
				_duration = (int)(Stop - Start).TotalMilliseconds;

				_result = true;
			}
			else
			{
				_duration = -1;

				_result = false;
			}
		}
	}
}
