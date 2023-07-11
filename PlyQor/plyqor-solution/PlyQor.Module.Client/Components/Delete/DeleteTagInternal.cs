namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

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
                { RequestKeys.Token, token },
                { RequestKeys.Container, container },
                { RequestKeys.Operation, QueryOperation.DeleteTag },
                { RequestKeys.Tag, tag }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
