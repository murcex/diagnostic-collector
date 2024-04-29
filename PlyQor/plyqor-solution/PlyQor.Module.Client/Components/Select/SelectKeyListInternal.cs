namespace PlyQor.Client
{
	using PlyQor.Client.Resources;

	class SelectKeyListInternal
	{
		public static Dictionary<string, string> Execute(
			string uri,
			string container,
			string token,
			string tag,
			int count)
		{
			Dictionary<string, string> request = new Dictionary<string, string>
			{
				{ RequestKeys.Token, token },
				{ RequestKeys.Container, container },
				{ RequestKeys.Operation, QueryOperation.SelectKeyList },
				{ RequestKeys.Tag, tag },
				{ RequestKeys.Aux, count.ToString() }
			};

			return Transmitter.Execute(uri, request);
		}
	}
}
