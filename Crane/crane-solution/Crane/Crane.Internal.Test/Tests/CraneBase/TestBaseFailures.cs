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
