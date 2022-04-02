namespace PlyQor.Client
{
    using PlyQor.Client.DataExtension.Internal;

    class InsertKeyInternal
    {
        public static Dictionary<string, string> Execute( 
            string uri, 
            string container, 
            string token, 
            string key, 
            string data, 
            List<string> tags)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "InsertKey" },
                { "Key", key },
                { "Data", data },
                { "Tags", tags.UnwrapTags() }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
