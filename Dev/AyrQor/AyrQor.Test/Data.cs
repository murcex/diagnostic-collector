using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AyrQor.Test
{
	[TestClass]
	public class Data
	{
		[TestMethod]
		public void Import()
		{
			List<(DateTime timestamp, string id, string data)> records = new();

			Looper looper = new(10);
			Dictionary<string, string> data_set = new();

			while (looper.IsActive())
			{
				var test_id = Guid.NewGuid().ToString();
				var test_data = Guid.NewGuid().ToString();

				records.Add((DateTime.Now, test_id, test_data));
				data_set.Add(test_id, test_data);

				looper.Next();
			}

			records.Add((DateTime.Now, "test", "abc"));

			AyrQorContainer container = new("Test1");
			container.Import(records);

			var data = container.Select("test");

			Assert.AreEqual("abc", data);

			foreach (var test_record in data_set)
			{
				var check_data = container.Select(test_record.Key);

				Assert.AreEqual(test_record.Value, check_data);
			}
		}

		[TestMethod]
		public void Export()
		{
			Looper looper = new(10);
			Dictionary<string, string> data_set = new();
			AyrQorContainer container = new("Test1");

			while (looper.IsActive())
			{
				var test_id = Guid.NewGuid().ToString();
				var test_data = Guid.NewGuid().ToString();
				container.Insert(test_id, test_data);
				data_set.Add(test_id, test_data);

				looper.Next();
			}

			var records = container.Export();

			foreach (var test_record in records)
			{
				data_set.TryGetValue(test_record.id.ToLower(), out var data);

				Assert.AreEqual(test_record.data, data);
			}
		}
	}
}
