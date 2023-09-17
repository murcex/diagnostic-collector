using Crane.Internal.Engine.Components;
using Crane.Internal.Engine.Interface;
using Crane.Internal.Test.Core;
using Crane.Internal.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crane.Internal.Test.Tests.CraneBase
{
    [TestClass]
	public class TestBaseExternal
	{
		private string craneTestDir;

		[TestInitialize]
		public void Execute()
		{
			Setup.Basic();

			craneTestDir = Path.Combine(Setup.CraneTestRoot, "external");

			Directory.SetCurrentDirectory(craneTestDir);

			// config.ini
			CreateTestFile("Config.ini", $"[crane]\r\ntask={Path.Combine(craneTestDir, "tasks")}\r\nlog={Path.Combine(craneTestDir, "logs")}");

			// logs\_
			CreateTestFile("logs\\_", $"");

			// tasks\test-task.ini
			CreateTestFile("tasks\\test-task.ini", $"[task]\r\nid=external-test-task\r\ntype=test");

			// log-test\_
			CreateTestFile("log-test\\_", $"");
			CleanDirectory("log-test");
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

		private static void CleanDirectory(string pathway)
		{
			pathway = Path.Combine(Directory.GetCurrentDirectory(), pathway);

			var dir = new DirectoryInfo(pathway);

			foreach (FileInfo file in dir.GetFiles())
			{
				file.Delete();
			}
			foreach (var nestedDir in dir.GetDirectories())
			{
				nestedDir.Delete(true);
			}
		}

		/// <summary>
		/// Test basic crane execution using user provided / external task directory
		/// (1) Load "test-task.ini" from external directory
		/// (2) Set Logger to external directory
		/// </summary>
		[TestMethod]
		public void TestCraneFileManager_External()
		{
			List<string> logs = new();
			ICraneLogger logger = new TestLogger(logs);

			CraneFileManager fileManager = new();

			var cfg = fileManager.LoadCraneConfig(logger);

			Assert.IsNotNull(cfg);
			Assert.AreEqual(1, cfg.Count);

			var loggerFilePath = fileManager.GetCraneLoggerFilePath(logger, cfg);

			logger.Enable(loggerFilePath);

			Assert.AreEqual(Path.Combine(Directory.GetCurrentDirectory(), "logs"), loggerFilePath);

			var scriptCfg = fileManager.LoadCraneTask(logger, cfg, "test-task.ini");

			var craneCfgHeader = scriptCfg["task"];

			var craneScriptId = craneCfgHeader["id"];

			var craneScriptType = craneCfgHeader["type"];

			Assert.AreEqual("external-test-task", craneScriptId);
			Assert.AreEqual("test", craneScriptType);
		}

		/// <summary>
		/// Test the logger will use current executing directory when enabled/forced without a user provided / extenral directory
		/// * This would occur in the event the logger throw exception before the log path could be aquired from the Config.ini
		/// </summary>
		[TestMethod]
		public void TestLogger_Off_External()
		{
			var testLogPathway = Path.Combine(Directory.GetCurrentDirectory(), "log-test");
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
			var logFile = afterFiles[0].FullName;
			var logContents = File.ReadAllLines(logFile);

			var infoCheck = logContents.Any(x => x.Contains(infoId));
			var errorCheck = logContents.Any(y => y.Contains(errorId));

			Assert.AreEqual(0, beforeFiles.Length);
			Assert.AreEqual(1, afterFiles.Length);
			Assert.IsTrue(status);
			Assert.IsTrue(infoCheck);
			Assert.IsTrue(errorCheck);
		}

		/// <summary>
		/// Test the logger will use the user provided / external directory when provided
		/// * This will only occur if the log=<directory> is provided in the Config.ini
		/// </summary>
		[TestMethod]
		public void TestLogger_On_External()
		{
			var testLogPathway = Path.Combine(Directory.GetCurrentDirectory(), "log-test");
			CleanDirectory(testLogPathway);
			var dir = new DirectoryInfo(testLogPathway);

			ICraneLogger logger = new CraneLogger(testLogPathway);

			var infoId = Guid.NewGuid().ToString();
			var errorId = Guid.NewGuid().ToString();

			logger.Info(infoId);
			logger.Error(errorId);

			var status = logger.Enabled();

			var afterFiles = dir.GetFiles();
			var logFile = afterFiles[0].FullName;
			var logContents = File.ReadAllLines(logFile);

			var infoCheck = logContents.Any(x => x.Contains(infoId));
			var errorCheck = logContents.Any(y => y.Contains(errorId));

			Assert.AreEqual(1, afterFiles.Length);
			Assert.IsTrue(status);
			Assert.IsTrue(infoCheck);
			Assert.IsTrue(errorCheck);
		}
	}
}
