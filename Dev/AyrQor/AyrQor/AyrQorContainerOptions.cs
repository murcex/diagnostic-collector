namespace AyrQor
{
	public class AyrQorContainerOptions
	{
		public AyrQorContainerOptions()
		{
			this.MaxSize = -1;
			this.MaxCount = -1;
			this.MaxAge = -1;
		}

		public int MaxSize { get; set; }
		public int MaxCount { get; set; }
		public int MaxAge { get; set; }

		public bool MaxSizePolicy()
		{
			return this.MaxSize > 0;
		}

		public bool MaxCountPolicy()
		{
			return this.MaxCount > 0;
		}
	}
}
