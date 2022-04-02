namespace PlyQor.Client
{
    class DeleteKeyInternal
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
                { "Operation", "DeleteKey" },
                { "Key", key }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
