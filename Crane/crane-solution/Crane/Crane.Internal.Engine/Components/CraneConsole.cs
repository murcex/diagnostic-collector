using Crane.Internal.Engine.Interface;
using Crane.Internal.Engine.Model;

namespace Crane.Internal.Engine.Components
{
	public class CraneConsole : ICraneConsole
	{
		public void Starter()
		{
			// clear console
			Console.Clear();

			// crane version
			Console.WriteLine("Starting Crane\r\n");
		}

		public void Conformation(ICraneLogger logger)
		{
			Console.WriteLine($"Continue? Y/N");
			var deployCheck = Console.ReadLine();

			if (!string.Equals("Y", deployCheck, StringComparison.OrdinalIgnoreCase))
			{
				logger.Info($"conformation_check=fail");
				throw new CraneException();
			}

			logger.Info($"conformation_check=pass");
		}

		public void Close()
		{
			Environment.Exit(0);
		}
	}
}
