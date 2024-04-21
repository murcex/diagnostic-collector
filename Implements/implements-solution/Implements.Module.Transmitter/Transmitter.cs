using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Module.Transmitter
{
	public class Transmitter
	{
		private static HttpClient _httpClient;

		private static ConcurrentDictionary<string, TransmissionExecutor> _profiles;

		private static string _defaultProfile;

		public Transmitter(HttpClient httpClient, TransmitterProfile profile)
		{
			_httpClient = httpClient;
			_profiles = new ConcurrentDictionary<string, TransmissionExecutor>();

			AddProfile(profile);
		}

		public void AddProfile(TransmitterProfile profile)
		{
			if (string.IsNullOrEmpty(profile.Name))
			{
				throw new ArgumentNullException("Transmitter Profile Name is NullOrEmpty");
			}

			_profiles.TryAdd(profile.Name, new TransmissionExecutor());
		}

		public static string Execute()
		{
			return Execute(_defaultProfile);
		}

		public static string Execute(string profileName)
		{
			if (_profiles.TryGetValue(profileName, out TransmissionExecutor executor))
			{
				if (executor == null)
				{
					throw new Exception($"Executor for Profile {profileName} is Null");
				}
				else
				{
					return executor.Execute();
				}
			}

			throw new ArgumentException($"Profile Name {profileName} doesn't exist");
		}
	}
}
