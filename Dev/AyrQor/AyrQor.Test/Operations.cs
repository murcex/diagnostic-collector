﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AyrQor.Test
{
	[TestClass]
	public class Operations
	{
		static readonly string containerName = "TestContainer";
		static readonly string id = Guid.NewGuid().ToString();
		static readonly string value = "AbCd134#";
		static readonly string value_2 = "EfGh567%";

		[TestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public void Insert(bool tagged)
		{
			AyrQorContainer container = new AyrQorContainer(containerName);
			var tag = tagged ? "Test".ToUpper() : null;

			var countStart = container.Count();
			var sizeStart = container.Size;

			var insertResult = container.Insert(id, value, tag);

			var countEnd = container.Count();
			var sizeEnd = container.Size;

			Assert.AreEqual(countStart, 0);
			Assert.AreEqual(sizeStart, 0);
			Assert.IsTrue(insertResult);
			Assert.AreEqual(countEnd, 1);
			Assert.AreEqual(sizeEnd, value.Length);

			if (tagged)
			{
				var tagged_data = container.MultiSelect(tag);

				Assert.AreEqual(1, tagged_data.Count);
			}
		}

		[TestMethod]
		public void Select()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			container.Insert(id, value);

			var select_1 = container.Select(id);
			var count_1 = container.Count();
			var size_1 = container.Size;

			var select_2 = container.Select(id, remove: true);
			var count_2 = container.Count();
			var size_2 = container.Size;

			var select_3 = container.Select(id);

			Assert.AreEqual(select_1, value);
			Assert.AreEqual(count_1, 1);
			Assert.AreEqual(size_1, value.Length);

			Assert.AreEqual(select_2, value);
			Assert.AreEqual(count_2, 0);
			Assert.AreEqual(size_2, 0);

			Assert.AreEqual(select_3, null);
		}

		[TestMethod]
		[DataRow(0, false, null)]
		[DataRow(3, false, null)]
		[DataRow(0, true, null)]
		[DataRow(3, true, null)]
		[DataRow(0, false, "Test")]
		[DataRow(3, false, "Test")]
		[DataRow(0, true, "Test")]
		[DataRow(3, true, "Test")]
		public void MultiSelect(int top, bool update, string tag)
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var countStart = container.Count();
			var sizeStart = container.Size;

			var a = "A";
			var b = "B";
			var c = "C";

			container.Insert(a, "1", tag);
			container.Insert(b, "2", tag);
			container.Insert(c, "3", tag);

			if (update)
			{
				container.Update(b, "4");
			}

			var selectDescResult = container.MultiSelect(tag, top: top);
			var selectDescArray = selectDescResult.ToArray();

			var selectAscResult = container.MultiSelect(tag, top: top, order: OrderBy.ASC);
			var selectAscArray = selectAscResult.ToArray();

			if (update)
			{
				Assert.AreEqual(selectDescArray[0].Key, b);
				Assert.AreEqual(selectDescArray[1].Key, c);
				Assert.AreEqual(selectDescArray[2].Key, a);
				Assert.AreEqual(selectAscArray[0].Key, a);
				Assert.AreEqual(selectAscArray[1].Key, c);
				Assert.AreEqual(selectAscArray[2].Key, b);
			}
			else
			{
				Assert.AreEqual(selectDescArray[0].Key, c);
				Assert.AreEqual(selectDescArray[1].Key, b);
				Assert.AreEqual(selectDescArray[2].Key, a);
				Assert.AreEqual(selectAscArray[0].Key, a);
				Assert.AreEqual(selectAscArray[1].Key, b);
				Assert.AreEqual(selectAscArray[2].Key, c);
			}
		}

		[TestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public void Update(bool tagged)
		{
			AyrQorContainer container = new AyrQorContainer(containerName);
			var tag = tagged ? "Test".ToUpper() : null;

			container.Insert(id, value, tag);

			var update_1 = container.Update(id, value_2);
			var update_2 = container.Update("test", value_2);

			var select_1 = container.Select(id);
			var count_1 = container.Count();
			var size_1 = container.Size;

			Assert.IsTrue(update_1);
			Assert.IsFalse(update_2);
			Assert.AreEqual(select_1, value_2);
			Assert.AreEqual(count_1, 1);
			Assert.AreEqual(size_1, value_2.Length);
		}

		[TestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public void Delete(bool tagged)
		{
			AyrQorContainer container = new AyrQorContainer(containerName);
			var tag = tagged ? "Test".ToUpper() : null;

			container.Insert(id, value, tag);

			var delete = container.Delete(id);

			var select_1 = container.Select(id);
			var multi_select = tagged ? container.MultiSelect(tag) : null;
			var count_1 = container.Count();
			var size_1 = container.Size;

			Assert.IsTrue(delete);
			if (tagged)
			{
				Assert.AreEqual(multi_select.Count, 0);
			}
			Assert.AreEqual(select_1, null);
			Assert.AreEqual(count_1, 0);
			Assert.AreEqual(size_1, 0);
		}

		[TestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public void Count(bool tagged)
		{
			AyrQorContainer container = new AyrQorContainer(containerName);
			var tag = tagged ? "Test".ToUpper() : null;

			container.Insert(id, value, tag);

			var count_1 = container.Count(tag);

			container.Insert(Guid.NewGuid().ToString(), value_2, tag);

			var count_2 = container.Count(tag);

			container.Delete(id);

			var count_3 = container.Count(tag);

			Assert.AreEqual(count_1, 1);
			Assert.AreEqual(count_2, 2);
			Assert.AreEqual(count_3, 1);
		}
	}
}
