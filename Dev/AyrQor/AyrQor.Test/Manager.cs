using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AyrQor.Test
{
	[TestClass]
	public class Manager
	{
		[TestMethod]
		public void Basic()
		{
			AyrQorContainer container_1 = new("Test1");
			AyrQorContainer container_2 = new("Test2");
			AyrQorManager manager = new();

			manager.Add(container_1);
			manager.Add(container_2);

			manager.Insert("Test1", "test123", "abc");
			var data = manager.Select("Test1", "test123");
			Assert.AreEqual("abc", data);

			manager.Update("Test1", "test123", "efg");
			data = manager.Select("Test1", "test123");
			Assert.AreEqual("efg", data);

			manager.Delete("Test1", "test123");
			data = manager.Select("Test1", "test123");
			Assert.AreEqual(null, data);
		}

		// import

		// export
	}
}
