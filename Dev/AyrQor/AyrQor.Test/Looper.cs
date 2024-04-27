namespace AyrQor.Test
{
	public class Looper
	{
		private int _current;

		private readonly int _limit;

		public int Current => _current;

		public Looper(int limit, int start = 0)
		{
			this._current = start;
			this._limit = limit;
		}

		public bool IsActive()
		{
			return _current < _limit;
		}

		public void Next()
		{
			_current++;
		}

		public void Stop()
		{
			_current = _limit;
		}
	}
}
