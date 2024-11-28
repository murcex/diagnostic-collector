namespace AyrQor
{
	public class AyrQorContainerOptions
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AyrQorContainerOptions"/> class.
		/// </summary>
		public AyrQorContainerOptions()
		{
			this.MaxDataSize = -1;
			this.MaxTotalDataSize = -1;
			this.MaxRecordCount = -1;
			this.MaxAge = -1;
		}

		/// <summary>
		/// Gets or sets the maximum size.
		/// </summary>
		public int MaxDataSize { get; set; }

		public int MaxTotalDataSize { get; set; }

		public int MaxKeySize { get; set; }

		public int MaxTagSize { get; set; }

		public int MaxTagCount { get; set; }

		/// <summary>
		/// Gets or sets the maximum count.
		/// </summary>
		public int MaxRecordCount { get; set; }

		/// <summary>
		/// Gets or sets the maximum age.
		/// </summary>
		public int MaxAge { get; set; }
	}
}
