namespace KirokuG2.Internal.Loader.Interface
{
	public interface IKLogSeralializer
	{
		Dictionary<string, List<string>> DeseralizalizeLogSet(string rawLog);
	}
}