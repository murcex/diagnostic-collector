namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class SelectKeyListInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key,
            int count)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.SelectKeyList },
                { RequestKeys.Tag, key },
                { RequestKeys.Aux, count.ToString() }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
