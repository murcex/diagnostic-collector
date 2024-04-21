using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implements.Module.Transmitter
{
	public class TransmitterProfile
	{
		public string Name { get; set; }
		public int MaxRetry { get; set; }
		public int Cooldown { get; set; }
		public int Backoff { get; set; }
		public int MaxDuration { get; set; }
	}
}
