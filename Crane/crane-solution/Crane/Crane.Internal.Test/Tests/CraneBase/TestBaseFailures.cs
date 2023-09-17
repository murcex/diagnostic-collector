using Crane.Internal.Engine.Components;
using Crane.Internal.Engine.Interface;
using Crane.Internal.Test.Core;
using Crane.Internal.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crane.Internal.Test.Tests.CraneBase
{
	[TestClass]
	public class TestBaseFailures
	{
		string craneTestDir = Path.Combine(Setup.CraneTestRoot, "failure");

		[TestInitialize]
		public void Execute()
		{
			Setup.Basic();

			//Directory.SetCurrentDirectory(craneTestDir);

			// local\task.ini
			//CreateTestFile("Config.ini", "");

			// tasks\task.ini
			//CreateTestFile("test-task.ini", $"[crane]\r\nid=default-test-task\r\ntype=test");

			//CleanDirectory();
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

		[TestMethod]
		public void TestOne()
		{
			List<string> logs = new();
			ICraneLogger logger = new TestLogger(logs);

			CraneFileManager fileManager = new();

			try
			{
				// 1 error - confign.ini doen't exist
				fileManager.LoadCraneConfig(logger);
			}
			catch (Exception ex)
			{

			}

			// both should have 1 error (cfg check is null)
			try
			{
				fileManager.GetCraneLoggerFilePath(logger, null);
			}
			catch (Exception ex)
			{
				var test_1 = ex.ToString();
			}

			try
			{
				fileManager.CheckForConformation(logger, null);
			}
			catch (Exception ex)
			{
				var test_2 = ex.ToString();
			}

			try
			{
				// 3 errors (should be +1)
				// 1. crane task dir doesn't exist
				// 2. crane task type is empty
				fileManager.LoadCraneTask(logger, null, string.Empty);
			}
			catch (Exception ex)
			{

			}
		}

		[TestMethod]
		public void TestTwo()
		{
			List<string> logs = new();
			ICraneLogger logger = new TestLogger(logs);

			CraneTaskManager taskManager = new();

			try
			{
				// 3 errors
				taskManager.Execute(logger, null, null);
			}
			catch (Exception ex)
			{

			}
		}
	}
}
