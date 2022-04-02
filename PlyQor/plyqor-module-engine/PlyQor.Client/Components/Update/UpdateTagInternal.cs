namespace PlyQor.Client
{
    class UpdateTagInternal
    {
        public static Dictionary<string, string> Execute( 
            string uri, 
            string container, 
            string token, 
            string tag_1, 
            string tag_2)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { "Token", token },
                { "Collection", container },
                { "Operation", "UpdateTag" },
                { "Tag", tag_1 },
                { "Aux", tag_2 }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
