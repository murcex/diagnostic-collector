namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class DeleteTagsByKeyInternal
    {
        public static Dictionary<string, string> Execute(
            string uri, 
            string container, 
            string token, 
            string key)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, token },
                { RequestKeys.Container, container },
                { RequestKeys.Operation, QueryOperation.DeleteTagsByKey },
                { RequestKeys.Key, key }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
