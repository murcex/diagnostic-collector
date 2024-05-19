using Implements.Function.Queue.Source.Core;
using System;
using System.Net.Http;

namespace Implements.Function.Queue.Source.Components
{
	public class HttpRequest
	{
		private static HttpClient _httpClient = new();

		public static bool SendRequest(string id)
		{
			try
			{
				var url = $"{Configuration.Endpoint}?token={Configuration.Token}&id={id}";

				HttpResponseMessage response = _httpClient.GetAsync(url).Result;

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
