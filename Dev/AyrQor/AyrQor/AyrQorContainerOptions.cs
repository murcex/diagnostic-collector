namespace AyrQor
{
	public class AyrQorContainerOptions
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AyrQorContainerOptions"/> class.
		/// </summary>
		public AyrQorContainerOptions()
		{
			this.MaxSize = -1;
			this.MaxCount = -1;
			this.MaxAge = -1;
		}

		/// <summary>
		/// Gets or sets the maximum size.
		/// </summary>
		public int MaxSize { get; set; }

		/// <summary>
		/// Gets or sets the maximum count.
		/// </summary>
		public int MaxCount { get; set; }

		/// <summary>
		/// Gets or sets the maximum age.
		/// </summary>
		public int MaxAge { get; set; }

		/// <summary>
		/// Determines if the maximum size policy is enabled.
		/// </summary>
		/// <returns><c>true</c> if the maximum size policy is enabled; otherwise, <c>false</c>.</returns>
		public bool MaxSizePolicy()
		{
			return this.MaxSize > 0;
		}

		/// <summary>
		/// Determines if the maximum count policy is enabled.
		/// </summary>
		/// <returns><c>true</c> if the maximum count policy is enabled; otherwise, <c>false</c>.</returns>
		public bool MaxCountPolicy()
		{
			return this.MaxCount > 0;
		}

		// max tags policy

		// max tag size policy

		// max key size policy
	}
}
