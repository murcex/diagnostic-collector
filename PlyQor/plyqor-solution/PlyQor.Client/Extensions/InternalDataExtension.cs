namespace PlyQor.Client.DataExtension.Internal
{
    using Newtonsoft.Json;

    public static class InternalDataExtension
    {
        public static string UnwrapTags(this List<string> tags)
        {
            return JsonConvert.SerializeObject(tags);
        }
    }
}
