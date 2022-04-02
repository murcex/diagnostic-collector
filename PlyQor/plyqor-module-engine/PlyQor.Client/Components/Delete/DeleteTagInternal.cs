namespace PlyQor.Client
{
    class DeleteTagInternal
    {
        public static Dictionary<string, string> Execute(
            string uri, 
            string container, 
            string token, 
            string tag)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "DeleteTag" },
                { "Tag", tag }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
