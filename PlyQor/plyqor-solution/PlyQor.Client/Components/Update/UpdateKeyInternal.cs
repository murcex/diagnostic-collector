namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class UpdateKeyInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key_1,
            string key_2)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.UpdateKey },
                { RequestKeys.Key, key_1 },
                { RequestKeys.Aux, key_2 }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
