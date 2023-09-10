namespace Crane.Internal.Engine.Interface
{
	public interface ICraneTaskType
	{
		(bool result, string message) Execute(ICraneLogger logger, Dictionary<string, Dictionary<string, string>> script, Dictionary<string, object> parameters);
	}
}