namespace Crane.Internal.Loggie
{
	public interface ICraneLogger
	{
		public void Info(string input);

		public void Error(string input);

		public void Enable(string rootPath);

		public bool Enabled();
	}
}