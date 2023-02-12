namespace PlyQor.Client
{
    using PlyQor.Client.Resources;

    class UpdateDataInternal
    {
        public static Dictionary<string, string> Execute(
            PlyClientConfiguration configuration,
            string key,
            string data)
        {
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                { RequestKeys.Token, configuration.Token },
                { RequestKeys.Container, configuration.Container },
                { RequestKeys.Operation, QueryOperation.UpdateData },
                { RequestKeys.Key, key },
                { RequestKeys.Aux, data }
            };

            return Transmitter.Execute(configuration, request);
        }
    }
}
