using Crane.Internal.Engine.Interface;

namespace Crane.Internal.Test.Mock
{
	public class TestConsole : ICraneConsole
	{
		public void Close()
		{
			//throw new NotImplementedException();
		}

		public void GeneralConformation(ICraneLogger logger)
		{
			//throw new NotImplementedException();
		}

		public void Starter()
		{
			//throw new NotImplementedException();
		}

		public void TaskConfirmation(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> collection)
		{
			//throw new NotImplementedException();
		}
	}
}
