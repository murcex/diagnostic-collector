using Crane.Internal.Engine.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crane.Internal.Test.Mock
{
	public class TestConsole : ICraneConsole
	{
		public void Close()
		{
			//throw new NotImplementedException();
		}

		public void Conformation(ICraneLogger logger)
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
