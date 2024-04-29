namespace PlyQor.Client
{
	using PlyQor.Client.Resources;

	class UpdateDataInternal
	{
		public static Dictionary<string, string> Execute(
			string uri,
			string container,
			string token,
			string key,
			string data)
		{
			Dictionary<string, string> request = new Dictionary<string, string>
			{
				{ RequestKeys.Token, token },
				{ RequestKeys.Container, container },
				{ RequestKeys.Operation, QueryOperation.UpdateData },
				{ RequestKeys.Key, key },
				{ RequestKeys.Aux, data }
			};

			return Transmitter.Execute(uri, request);
		}
	}
}
