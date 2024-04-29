namespace PlyQor.Audit.TestCases.PlyManager
{
	using PlyQor.Engine;
	using System;

	internal class DataRetention
	{
		public static void Execute()
		{
			Console.WriteLine("-----");
			Console.WriteLine(" \n\r// DataRetention");
			var result = PlyQorManager.Retention();

			Console.WriteLine(result);
		}
	}
}
