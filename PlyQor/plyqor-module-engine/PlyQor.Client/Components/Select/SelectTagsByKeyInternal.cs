namespace PlyQor.Client
{
    class SelectTagsByKeyInternal
    {
        public static Dictionary<string, string> Execute(
            string uri, 
            string container, 
            string token, 
            string key)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "SelectTagsByKey" },
                { "Key", key }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
