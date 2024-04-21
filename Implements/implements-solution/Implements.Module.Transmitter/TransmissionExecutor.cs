using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Module.Transmitter
{
	public class TransmissionExecutor
	{
		private string _name;
		private int _maxRetry;
		private int _cooldown;
		private int _backoff;
		private int _maxDuration;


		public TransmissionExecutor(TransmitterProfile profile)
		{
			_name = profile.Name;
			_maxRetry = profile.MaxRetry;
			_backoff = profile.Backoff;
			_cooldown = profile.Cooldown;
			_maxDuration = profile.MaxDuration;
		}

		public string Execute()
		{

		}
	}
}
