namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class UpdateKeyTagInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key,
            string tag_1, 
            string tag_2)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.UpdateKeyTag },
                { RequestKeys.Key, key },
                { RequestKeys.Tag, tag_1 },
                { RequestKeys.Aux, tag_2 }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
