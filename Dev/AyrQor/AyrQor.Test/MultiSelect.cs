using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AyrQor.Test
{
	[TestClass]
	public class MultiSelect
	{
		static readonly string containerName = "TestContainer";
		static readonly string id = Guid.NewGuid().ToString();
		static readonly string value = "AbCd134#";
		static readonly string value_2 = "EfGh567%";

		private Dictionary<string, string> CreateDataSet(int count, int index = 1)
		{
			if (index > 1)
			{
				count += index;
				index++;
			}

			Dictionary<string, string> dataSet = new Dictionary<string, string>();

			for (int i = index; i <= count; i++)
			{
				dataSet.Add($"Entry_{i}", Guid.NewGuid().ToString().Replace("-", ""));
			}

			return dataSet;
		}

		[TestMethod]
		public void MultiSelect_All()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var dataSet = CreateDataSet(10);

			foreach (var kvp in dataSet)
			{
				container.Insert(kvp.Key, kvp.Value);
			}

			var package = container.MultiSelect();
			var count_1 = container.Count();

			var match = 0;
			foreach (var msg in package)
			{
				if (dataSet.ContainsValue(msg.Value))
				{
					match++;
				}
			}

			var package_2 = container.MultiSelect(remove: true);
			var count_2 = container.Count();

			var match_2 = 0;
			foreach (var msg in package_2)
			{
				if (dataSet.ContainsValue(msg.Value))
				{
					match_2++;
				}
			}

			var package_3 = container.MultiSelect();

			Assert.AreEqual(package.Count, dataSet.Count);
			Assert.AreEqual(count_1, dataSet.Count);
			Assert.AreEqual(match, dataSet.Count);
			Assert.AreEqual(package_2.Count, dataSet.Count);
			Assert.AreEqual(count_2, 0);
			Assert.AreEqual(match_2, dataSet.Count);
			Assert.AreEqual(package_3.Count, 0);
		}

		[TestMethod]
		public void MultiSelect_ASC()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var dataSet_1 = CreateDataSet(5);
			var dataSet_2 = CreateDataSet(5, 5);

			foreach (var kvp in dataSet_1)
			{
				container.Insert(kvp.Key, kvp.Value);

				Thread.Sleep(1000);
			}

			foreach (var kvp in dataSet_2)
			{
				container.Insert(kvp.Key, kvp.Value);

				Thread.Sleep(1000);
			}

			// Select only
			var package_1 = container.MultiSelect(top: 5, order: OrderBy.ASC);
			var count_1 = container.Count();

			var match_1 = 0;
			foreach (var msg in package_1)
			{
				if (dataSet_1.ContainsValue(msg.Value))
				{
					match_1++;
				}
			}

			// Select with removal
			var package_2 = container.MultiSelect(top: 5, order: OrderBy.ASC, remove: true);
			var count_2 = container.Count();

			var match_2 = 0;
			foreach (var msg in package_2)
			{
				if (dataSet_1.ContainsValue(msg.Value))
				{
					match_2++;
				}
			}

			//var package_3 = Container.Select(Scope.All);

			Assert.AreEqual(package_1.Count, dataSet_1.Count);
			Assert.AreEqual(count_1, dataSet_1.Count + dataSet_2.Count);
			Assert.AreEqual(match_1, dataSet_2.Count);
			//Assert.AreEqual(package_2.Count, dataSet.Count);
			//Assert.AreEqual(count_2, dataSet.Count);
			//Assert.AreEqual(match_2, dataSet.Count);
			//Assert.AreEqual(package_3.Count, 0);
		}

		[TestMethod]
		public void MultiSelect_DESC()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var dataSet = CreateDataSet(10);

			foreach (var kvp in dataSet)
			{
				container.Insert(kvp.Key, kvp.Value);
			}

			var package = container.MultiSelect();
			var count_1 = container.Count();

			var match = 0;
			foreach (var msg in package)
			{
				if (dataSet.ContainsValue(msg.Value))
				{
					match++;
				}
			}

			var package_2 = container.MultiSelect(remove: true);
			var count_2 = container.Count();

			var match_2 = 0;
			foreach (var msg in package_2)
			{
				if (dataSet.ContainsValue(msg.Value))
				{
					match_2++;
				}
			}

			var package_3 = container.MultiSelect();

			Assert.AreEqual(package.Count, dataSet.Count);
			Assert.AreEqual(count_1, dataSet.Count);
			Assert.AreEqual(match, dataSet.Count);
			Assert.AreEqual(package_2.Count, dataSet.Count);
			Assert.AreEqual(count_2, 0);
			Assert.AreEqual(match_2, dataSet.Count);
			Assert.AreEqual(package_3.Count, 0);
		}
	}
}
