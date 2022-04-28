namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class UpdateKeyTagInternal
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
                { RequestKeys.Token, token },
                { RequestKeys.Container, container },
                { RequestKeys.Operation, QueryOperation.UpdateKeyTag },
                { RequestKeys.Key, key },
                { RequestKeys.Tag, tag_1 },
                { RequestKeys.Aux, tag_2 }
            };

            return Transmitter.Execute(uri, request);
        }
    }
}
