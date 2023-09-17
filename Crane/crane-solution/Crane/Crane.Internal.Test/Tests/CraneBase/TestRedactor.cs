using Crane.Internal.Engine.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crane.Internal.Test.Tests.CraneBase
{
	[TestClass]
	public class TestRedactor
	{
		[TestMethod]
		public void TestRedactor_Hit()
		{
			Dictionary<string, string> taskCfg = new();
			taskCfg.Add("crane_redact", "testa1,testb2");

			Dictionary<string, string> testa = new();
			testa.Add("testa1", "a1key");
			testa.Add("testa2", "a2");
			Dictionary<string, string> testb = new();
			testb.Add("testb1", "b1");
			testb.Add("testb2", "b2key");
			Dictionary<string, Dictionary<string, string>> taskParameters = new();
			taskParameters.Add("testa", testa);
			taskParameters.Add("testb", testb);

			CraneRedactor redactor = new();

			var consoleCollection = redactor.Execute(taskCfg, taskParameters);

			consoleCollection.TryGetValue("testa", out var testaColsone);
			Assert.AreEqual(2, testaColsone.Count);
			var a1 = testaColsone["testa1"];
			var a2 = testaColsone["testa2"];
			Assert.AreEqual("*redacted*", a1);
			Assert.AreEqual("a2", a2);

			consoleCollection.TryGetValue("testb", out var testbColsone);
			Assert.AreEqual(2, testbColsone.Count);
			var b1 = testbColsone["testb1"];
			var b2 = testbColsone["testb2"];
			Assert.AreEqual("b1", b1);
			Assert.AreEqual("*redacted*", b2);
		}

		[TestMethod]
		public void TestRedactor_Miss_EmptyKey()
		{
			Dictionary<string, string> taskCfg = new();

			Dictionary<string, string> testa = new();
			testa.Add("testa1", "a1key");
			testa.Add("testa2", "a2");
			Dictionary<string, string> testb = new();
			testb.Add("testb1", "b1");
			testb.Add("testb2", "b2key");
			Dictionary<string, Dictionary<string, string>> taskParameters = new();
			taskParameters.Add("testa", testa);
			taskParameters.Add("testb", testb);

			CraneRedactor redactor = new();

			var consoleCollection = redactor.Execute(taskCfg, taskParameters);

			consoleCollection.TryGetValue("testa", out var testaColsone);
			Assert.AreEqual(2, testaColsone.Count);
			var a1 = testaColsone["testa1"];
			var a2 = testaColsone["testa2"];
			Assert.AreEqual("a1key", a1);
			Assert.AreEqual("a2", a2);

			consoleCollection.TryGetValue("testb", out var testbColsone);
			Assert.AreEqual(2, testbColsone.Count);
			var b1 = testbColsone["testb1"];
			var b2 = testbColsone["testb2"];
			Assert.AreEqual("b1", b1);
			Assert.AreEqual("b2key", b2);
		}

		[TestMethod]
		public void TestRedactor_Miss_EmptyValue()
		{
			Dictionary<string, string> taskCfg = new();
			taskCfg.Add("crane_redact", string.Empty);

			Dictionary<string, string> testa = new();
			testa.Add("testa1", "a1key");
			testa.Add("testa2", "a2");
			Dictionary<string, string> testb = new();
			testb.Add("testb1", "b1");
			testb.Add("testb2", "b2key");
			Dictionary<string, Dictionary<string, string>> taskParameters = new();
			taskParameters.Add("testa", testa);
			taskParameters.Add("testb", testb);

			CraneRedactor redactor = new();

			var consoleCollection = redactor.Execute(taskCfg, taskParameters);

			consoleCollection.TryGetValue("testa", out var testaColsone);
			Assert.AreEqual(2, testaColsone.Count);
			var a1 = testaColsone["testa1"];
			var a2 = testaColsone["testa2"];
			Assert.AreEqual("a1key", a1);
			Assert.AreEqual("a2", a2);

			consoleCollection.TryGetValue("testb", out var testbColsone);
			Assert.AreEqual(2, testbColsone.Count);
			var b1 = testbColsone["testb1"];
			var b2 = testbColsone["testb2"];
			Assert.AreEqual("b1", b1);
			Assert.AreEqual("b2key", b2);
		}
	}
}
