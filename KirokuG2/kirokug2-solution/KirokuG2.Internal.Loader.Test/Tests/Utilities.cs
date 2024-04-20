using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirokuG2.Internal.Loader.Test.Tests
{
	public class KResults
	{
		public List<string> Initialized { get; set; } = new();

		public List<string> Activations { get; set; } = new();

		public List<string> Blocks { get; set; } = new();

		public List<string> Criticals { get; set; } = new();

		public List<string> Errors { get; set; } = new();

		public List<string> Instances { get; set; } = new();

		public List<string> Metrics { get; set; } = new();

		public List<string> Quarantined { get; set; } = new();
	}

	public class Utilities
	{
		public static KResults GetResults(Dictionary<string, string> tracker)
		{
			var results = new KResults();

			foreach (var item in tracker)
			{
				var type = item.Key.Split("-")[1];

				switch (type.ToUpperInvariant())
				{
					case "INITIALIZED":
						results.Initialized.Add(item.Value);
						break;
					case "ACTIVATION":
						results.Activations.Add(item.Value);
						break;
					case "BLOCK":
						results.Blocks.Add(item.Value);
						break;
					case "CRITICAL":
						results.Criticals.Add(item.Value);
						break;
					case "ERROR":
						results.Errors.Add(item.Value);
						break;
					case "INSTANCE":
						results.Instances.Add(item.Value);
						break;
					case "METRIC":
						results.Metrics.Add(item.Value);
						break;
					case "QUARANTINE":
						results.Quarantined.Add(item.Value);
						break;
					default:
						throw new Exception($"Uknown KLog Type {type}");
				}
			}

			return results;
		}
	}
}
