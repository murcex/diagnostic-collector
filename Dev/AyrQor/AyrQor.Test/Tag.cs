using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AyrQor.Test
{
	[TestClass]
	public class Tag
	{
		static readonly string containerName = "TestContainer";
		static readonly string id = Guid.NewGuid().ToString();
		static readonly string value = "AbCd134#";
		static readonly string value_2 = "EfGh567%";
		static readonly string tag = "TestTag";

		[TestMethod]
		[DataRow(0, false)]
		[DataRow(3, false)]
		[DataRow(0, true)]
		[DataRow(3, true)]
		public void Select(int top, bool update)
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
		public void Update()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);
			var newTag = "NewTag";

			var countStart = container.Count();
			var sizeStart = container.Size;

			container.Insert("A", "1", tag);

			var selectStart = container.MultiSelect(tag);

			var updateResult = container.Update(tag, newTag, tag: true);

			var selectEnd = container.MultiSelect(tag);
			var selectNew = container.MultiSelect(newTag);

			Assert.AreEqual(selectStart.Count, 1);
			Assert.IsTrue(updateResult);
			Assert.AreEqual(selectEnd.Count, 0);
			Assert.AreEqual(selectNew.Count, 1);
		}

		[TestMethod]
		public void Delete()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var countStart = container.Count();
			var sizeStart = container.Size;

			container.Insert("A", "1", tag);

			var selectStart = container.MultiSelect(tag);

			var deleteResult = container.Delete(tag, tag: true);

			var selectEnd = container.MultiSelect();
			var selectTag = container.MultiSelect(tag);

			Assert.AreEqual(selectStart.Count, 1);
			Assert.IsTrue(deleteResult);
			Assert.AreEqual(selectEnd.Count, 1);
			Assert.AreEqual(selectTag.Count, 0);
		}
	}
}
