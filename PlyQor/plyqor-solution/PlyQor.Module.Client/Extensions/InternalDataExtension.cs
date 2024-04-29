using System.Text.Json;

namespace PlyQor.Client.DataExtension.Internal
{
	public static class InternalDataExtension
	{
		public static string UnwrapTags(this List<string> tags)
		{
			return JsonSerializer.Serialize(tags);
		}
	}
}
