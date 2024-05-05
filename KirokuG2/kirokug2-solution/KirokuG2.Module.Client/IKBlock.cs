namespace KirokuG2
{
	public interface IKBlock
	{
		string Tag { get; }

		void Dispose();
		void Stop();
	}
}