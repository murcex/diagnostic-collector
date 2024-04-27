using AyrQor;
using System;

namespace AryQor.Audit
{
	class Index
	{
		static void Main(string[] args)
		{
			Console.WriteLine("-- AyrQor Audit --");

			// ---

			AyrQorContainer container = new AyrQorContainer("Test");

			// ---

			container.Insert("1", "AAA");
			container.Insert("2", "BBB");
			container.Insert("3", "CCC");
			container.Insert("4", "DDD");
			container.Insert("5", "EEE");
			container.Insert("6", "FFF");
			var selectAll = container.MultiSelect();
			Console.WriteLine("\r");
			foreach (var item in selectAll)
			{
				Console.WriteLine($"Item: {item.Key},{item.Value}");
			}
			Console.WriteLine($"\r\nContainer Count: {container.Count()}");
			Console.WriteLine($"Container Size: {container.Size}");

			// ---

			var deleteIdFive = container.Delete("5");
			var selectIdTwo = container.Select("2");
			Console.WriteLine($"Delete ID 5: {deleteIdFive}");
			Console.WriteLine($"\r\nID 2: {selectIdTwo}");
			Console.WriteLine($"Container Count: {container.Count()}");
			Console.WriteLine($"Containerg Size: {container.Size}");

			// ---

			var updateIdTwo = container.Update("2", "BBBBBB");
			var selectIdTwoAgain = container.Select("2");
			var selectIdOneWithRemove = container.Select("1", remove: true);
			Console.WriteLine($"\r\nID 2 Update: {selectIdOneWithRemove} ");
			Console.WriteLine($"ID 1: {selectIdTwoAgain}");
			Console.WriteLine($"Container Count: {container.Count()}");
			Console.WriteLine($"Container Size: {container.Size}");

			// ---

			var allItems = container.MultiSelect(remove: true);
			Console.WriteLine("\r");
			foreach (var item in allItems)
			{
				Console.WriteLine($"Item: {item.Key},{item.Value}");
			}

			// ---

			var watermark = container.Ping();
			Console.WriteLine($"\r\nWatermark: {watermark}");

			// ---

			var document = Writer.CreateDocument(100);
			var insertIdSevenDocument = container.Insert("7", document);
			Console.WriteLine($"Add Id 7: {insertIdSevenDocument}");
			Console.WriteLine($"Container Count: {container.Count()}");
			Console.WriteLine($"Container Size: {container.Size}");
			var selectDocument = container.Select("7");
			Console.WriteLine($"\r\nDocument Check: {Equals(document, selectDocument)}");
			Console.WriteLine($"Document Data:");
			Console.WriteLine($"{selectDocument}");

			// ---

			Console.WriteLine("\r\nEnd");
			Console.ReadKey();
		}
	}
}
