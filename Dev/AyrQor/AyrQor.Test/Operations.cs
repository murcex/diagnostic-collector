using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
		public void Insert()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var countStart = container.Count();
			var sizeStart = container.Size;

			var insertResult = container.Insert(id, value);

			Assert.AreEqual(countStart, 0);
			Assert.AreEqual(sizeStart, 0);
			Assert.IsTrue(insertResult);
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
		public void Update()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			container.Insert(id, value);

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
		public void Delete()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			container.Insert(id, value);

			var delete = container.Delete(id);

			var select_1 = container.Select(id);
			var count_1 = container.Count();
			var size_1 = container.Size;

			Assert.IsTrue(delete);
			Assert.AreEqual(select_1, null);
			Assert.AreEqual(count_1, 0);
			Assert.AreEqual(size_1, 0);
		}
	}
}
