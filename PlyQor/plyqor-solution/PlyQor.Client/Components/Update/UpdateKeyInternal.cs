namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

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
                { RequestKeys.Token, token },
                { RequestKeys.Container, container },
                { RequestKeys.Operation, QueryOperation.UpdateKey },
                { RequestKeys.Key, key_1 },
                { RequestKeys.Aux, key_2 }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
