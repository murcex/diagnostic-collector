namespace PlyQor.Client
{
    class UpdateKeyInternal
    {
        public static Dictionary<string, string> Execute(
            string uri, 
            string container, 
            string token, 
            string key_1, 
            string key_2)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "UpdateKey" },
                { "Key", key_1 },
                { "Aux", key_2 }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
