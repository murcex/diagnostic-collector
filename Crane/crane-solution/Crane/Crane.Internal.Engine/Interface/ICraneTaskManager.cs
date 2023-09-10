namespace Crane.Internal.Engine.Interface
{
	public interface ICraneTaskManager
	{
		(bool result, string message) Execute(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> craneTask);
	}
}