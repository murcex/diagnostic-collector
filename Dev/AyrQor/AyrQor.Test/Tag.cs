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
		public void Tag_Select()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var countStart = container.Count();
			var sizeStart = container.Size;

			container.Insert("A", "1", tag);
			container.Insert("B", "2", tag);
			container.Insert("C", "3", tag);

			var selectDescResult = container.MultiSelect(tag, top: 3);
			var selectDescArray = selectDescResult.ToArray();

			var selectAscResult = container.MultiSelect(tag, top: 3, order: OrderBy.ASC);
			var selectAscArray = selectAscResult.ToArray();

			var desc1 = selectDescArray[0];
			var desc2 = selectDescArray[1];
			var desc3 = selectDescArray[2];

			var asc1 = selectAscArray[0];
			var asc2 = selectAscArray[1];
			var asc3 = selectAscArray[2];

			var test = 0;
		}

		[TestMethod]
		public void Tag_Update()
		{
			AyrQorContainer container = new AyrQorContainer(containerName);

			var countStart = container.Count();
			var sizeStart = container.Size;

			container.Insert("A", "1", tag);
			container.Insert("B", "2", tag);
			container.Insert("C", "3", tag);

			container.Update("A", "4");

			var selectDescResult = container.MultiSelect(tag, top: 3);
			var selectDescArray = selectDescResult.ToArray();

			var selectAscResult = container.MultiSelect(tag, top: 3, order: OrderBy.ASC);
			var selectAscArray = selectAscResult.ToArray();

			var desc1 = selectDescArray[0];
			var desc2 = selectDescArray[1];
			var desc3 = selectDescArray[2];

			var asc1 = selectAscArray[0];
			var asc2 = selectAscArray[1];
			var asc3 = selectAscArray[2];

			var test = 0;
		}
	}
}
