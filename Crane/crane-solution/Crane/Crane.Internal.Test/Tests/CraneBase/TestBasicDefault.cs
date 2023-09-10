using Crane.Internal.Engine.Components;
using Crane.Internal.Engine.Interface;
using Crane.Internal.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crane.Internal.Test.Tests.CraneBase
{
	[TestClass]
	public class TestBasicDefault
	{
		string craneTestDir = @"C:\Data\CraneTest\local";

		[TestInitialize]
		public void Execute()
		{
			Directory.SetCurrentDirectory(craneTestDir);

			// local\task.ini
			CreateTestFile("Config.ini", "");

			// tasks\task.ini
			CreateTestFile("test-task.ini", $"[task]\r\nid=default-test-task\r\ntype=test");

			CleanDirectory();
		}

		private static void CreateTestFile(string file, string contents)
		{
			var root = Directory.GetCurrentDirectory();

			var compoents = file.Split("\\").ToList();

			if (compoents.Count > 1)
			{
				compoents.Remove(compoents[compoents.Count - 1]);

				var testFilepath = string.Empty;
				foreach (var compoent in compoents)
				{
					if (string.IsNullOrEmpty(testFilepath))
					{
						testFilepath = compoent;
					}
					else
					{
						testFilepath = Path.Combine(testFilepath, compoent);
					}

					var testFilePath_1 = Path.Combine(root, testFilepath);

					if (!Directory.Exists(testFilePath_1))
					{
						Directory.CreateDirectory(testFilePath_1);
					}
				}
			}

			file = Path.Combine(root, file);

			File.WriteAllText(file, contents);
		}

		private static void CleanDirectory()
		{
			var pathway = Directory.GetCurrentDirectory();

			var files = new DirectoryInfo(pathway).GetFiles();

			foreach (var file in files)
			{
				if (!string.Equals("Config.ini", file.Name, StringComparison.OrdinalIgnoreCase) && !string.Equals("test-task.ini", file.Name, StringComparison.OrdinalIgnoreCase))
				{
					file.Delete();
				}
			}
		}

		[TestMethod]
		public void TestCraneFileManager_Default()
		{
			List<string> logs = new();
			ICraneLogger logger = new TestLogger(logs);

			CraneFileManager fileManager = new();

			var cfg = fileManager.LoadCraneConfig(logger);

			Assert.IsNotNull(cfg);
			Assert.AreEqual(0, cfg.Count);

			var loggerFilePath = fileManager.GetCraneLoggerFilePath(logger, cfg);

			logger.Enable(loggerFilePath);

			Assert.AreEqual(Directory.GetCurrentDirectory(), loggerFilePath);

			var scriptCfg = fileManager.LoadCraneTask(logger, cfg, "test-task.ini");

			var craneCfgHeader = scriptCfg["task"];

			var craneScriptId = craneCfgHeader["id"];

			var craneScriptType = craneCfgHeader["type"];

			Assert.AreEqual("default-test-task", craneScriptId);
			Assert.AreEqual("test", craneScriptType);
		}

		[TestMethod]
		public void TestLogger_Off_Default()
		{
			var testLogPathway = Directory.GetCurrentDirectory();
			CleanDirectory();
			var dir = new DirectoryInfo(testLogPathway);

			ICraneLogger logger = new CraneLogger();

			var infoId = Guid.NewGuid().ToString();
			var errorId = Guid.NewGuid().ToString();

			logger.Info(infoId);
			logger.Error(errorId);

			var beforeFiles = dir.GetFiles();

			logger.Enable(testLogPathway);
			var status = logger.Enabled();

			var afterFiles = dir.GetFiles();
			var logFile = string.Empty;
			foreach (var file in afterFiles)
			{
				if (!string.Equals("Config.ini", file.Name, StringComparison.OrdinalIgnoreCase) && !string.Equals("test-task.ini", file.Name, StringComparison.OrdinalIgnoreCase))
				{
					logFile = file.Name;
					break;
				}
			}

			var logContents = File.ReadAllLines(logFile);

			foreach (var entry in logContents)
			{
				Console.WriteLine($"entry: {entry}");
			}

			var infoCheck = logContents.Any(x => x.Contains(infoId));
			var errorCheck = logContents.Any(y => y.Contains(errorId));

			Assert.AreEqual(2, beforeFiles.Length);
			Assert.AreEqual(3, afterFiles.Length);
			Assert.IsTrue(status, "logger status should be true (enabled)");
			Assert.IsTrue(infoCheck, $"log contents should contain info id {infoId}");
			Assert.IsTrue(errorCheck, $"log contents should container error id {errorId}");
		}

		[TestMethod]
		public void TestLogger_On_Default()
		{
			var testLogPathway = Directory.GetCurrentDirectory();
			CleanDirectory();
			var dir = new DirectoryInfo(testLogPathway);

			ICraneLogger logger = new CraneLogger(testLogPathway);

			var infoId = Guid.NewGuid().ToString();
			var errorId = Guid.NewGuid().ToString();

			logger.Info(infoId);
			logger.Error(errorId);

			var status = logger.Enabled();

			var afterFiles = dir.GetFiles();
			var logFile = string.Empty;
			foreach (var file in afterFiles)
			{
				if (!string.Equals("Config.ini", file.Name, StringComparison.OrdinalIgnoreCase) && !string.Equals("test-task.ini", file.Name, StringComparison.OrdinalIgnoreCase))
				{
					logFile = file.Name;
					break;
				}
			}

			var logContents = File.ReadAllLines(logFile);

			foreach (var entry in logContents)
			{
				Console.WriteLine($"entry: {entry}");
			}

			var infoCheck = logContents.Any(x => x.Contains(infoId));
			var errorCheck = logContents.Any(y => y.Contains(errorId));

			Assert.IsTrue(status, "logger status should be true (enabled)");
			Assert.IsTrue(infoCheck, $"log contents should contain info id {infoId}");
			Assert.IsTrue(errorCheck, $"log contents should container error id {errorId}");
		}
	}
}
