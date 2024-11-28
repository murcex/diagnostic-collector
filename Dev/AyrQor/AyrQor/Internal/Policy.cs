using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyrQor.Internal
{
	public class Policy
	{
		private readonly AyrQorContainerOptions _options;

		public Policy(AyrQorContainerOptions options)
		{
			_options = options;
		}

		// max data size
		public bool MaxDataSizePolicy(string data)
		{
			return _options.MaxDataSize < data.Length;
		}

		// max total data size
		public bool MaxTotalDataSizePolicy(int size)
		{
			return _options.MaxTotalDataSize < size;
		}

		// max record count
		public bool MaxRecordCountPolicy(int count)
		{
			return _options.MaxRecordCount < count;
		}

		// max key size
		public bool MaxKeySizePolicy(string key)
		{
			return _options.MaxKeySize < key.Length;
		}

		// max tag size
		public bool MaxTagSizePolicy(string tag)
		{
			return _options.MaxTagSize < tag.Length;
		}

		// max tag count
		public bool MaxTagCountPolicy(int tagCount)
		{
			return _options.MaxTagCount < tagCount;
		}

		// data retention

		// data retention by tag
	}
}
