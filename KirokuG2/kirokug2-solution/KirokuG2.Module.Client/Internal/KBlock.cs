namespace KirokuG2.Internal
{
	public class KBlock : IDisposable, IKBlock
	{
		/// <summary>
		/// Get KBLock Tag
		/// </summary>
		public string Tag { get; private set; }

		private bool disposedValue;

		private Action<string, string> _injector;

		public KBlock(string name, Action<string, string> injector)
		{
			this.Tag = Guid.NewGuid().ToString().Split('-')[0].ToUpper();

			_injector = injector;

			var data = $"{this.Tag}${name}";

			_injector("B", data);
		}

		/// <summary>
		/// Stop KBlock tracing
		/// </summary>
		public void Stop()
		{
			Shutdown();
		}

		private void Shutdown()
		{
			_injector("SB", this.Tag);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Shutdown();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
