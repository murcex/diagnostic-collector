namespace Crane.Internal.Engine.Interface
{
	public interface ICraneConsole
	{
		void Starter();
		void Conformation(ICraneLogger logger);
		public void Close();
	}
}