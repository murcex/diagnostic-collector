namespace AryQor
{
	using System;
	using System.Linq;
	using System.Text;

	class Writer
	{
		/// <summary>
		/// Create document to write to storage.
		/// </summary>
		/// <returns></returns>
		public static string CreateDocument(int lineCount)
		{
			var random = new Random();
			var stop = lineCount;
			var executionCounter = 0;
			var sb = new StringBuilder();

			do
			{
				sb.AppendLine(RandomStringGenerator(random, lineCount));

				executionCounter++;
			}
			while (stop > executionCounter);

			var document = sb.ToString();

			var fileHash = document.GetHashCode().ToString();
			var fileSize = document.Length * sizeof(Char);

			return sb.ToString();
		}


		/// <summary>
		/// Random string generator.
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		private static string RandomStringGenerator(Random random, int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(
			  Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)])
			  .ToArray()
			  );
		}
	}
}
