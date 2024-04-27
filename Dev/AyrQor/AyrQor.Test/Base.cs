using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AyrQor.Test
{
	[TestClass]
	public class Base
	{
		[TestMethod]
		public void CreateContainer()
		{
			var containerName = "TestContainer";

			AyrQorContainer container = new AyrQorContainer(containerName);

			Assert.AreEqual(container.Name, containerName);
		}
	}
}
