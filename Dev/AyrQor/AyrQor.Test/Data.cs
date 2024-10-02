using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AyrQor.Test
{
	[TestClass]
	public class Data
	{
		[TestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public void Import(bool tagged)
		{
			List<(DateTime timestamp, string id, string data, List<string> tags)> records = new();

			Iterator iterator = new(10);
			Dictionary<string, string> data_set = new();
			List<string> tags = tagged ? new List<string>() { "Test".ToUpper() } : new List<string>();

			while (iterator.IsActive())
			{
				var test_id = Guid.NewGuid().ToString();
				var test_data = Guid.NewGuid().ToString();

				records.Add((DateTime.Now, test_id, test_data, tags));

				data_set.Add(test_id, test_data);

				iterator.Next();
			}

			records.Add((DateTime.Now, "test", "abc", tags));

			AyrQorContainer container = new("Test1");
			container.Import(records);

			var data = container.Select("test");

			Assert.AreEqual("abc", data);

			foreach (var test_record in data_set)
			{
				var check_data = container.Select(test_record.Key);

				Assert.AreEqual(test_record.Value, check_data);
			}

			if (tagged)
			{
				var tagged_data = container.MultiSelect("Test");

				Assert.AreEqual(11, tagged_data.Count);
			}
		}

		[TestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public void Export(bool tagged)
		{
			Iterator interator = new(10);
			Dictionary<string, string> data_set = new();
			AyrQorContainer container = new("Test1");
			var tag = tagged ? "Test".ToUpper() : null;

			while (interator.IsActive())
			{
				var test_id = Guid.NewGuid().ToString();
				var test_data = Guid.NewGuid().ToString();
				container.Insert(test_id, test_data, tag);
				data_set.Add(test_id, test_data);

				interator.Next();
			}

			var records = container.Export();

			foreach (var test_record in records)
			{
				data_set.TryGetValue(test_record.id.ToLower(), out var data);

				Assert.AreEqual(test_record.data, data);

				if (tagged)
				{
					Assert.IsTrue(test_record.tags.Contains(tag));
				}
			}
		}
	}
}
