namespace PlyQor.Client
{
    class UpdateTagByKeyInternal
    {
        public static Dictionary<string, string> Execute(
            string uri, 
            string container, 
            string token, 
            string key, 
            string tag_1, 
            string tag_2)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "UpdateTagByKey" },
                { "Key", key },
                { "Tag", tag_1 },
                { "Aux", tag_2 }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
