using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AyrQor
{
	public class AyrQorManager
	{
		private readonly ConcurrentDictionary<string, AyrQorContainer> containers;

		public AyrQorManager()
		{
			containers = new();
		}

		public bool Add(AyrQorContainer container)
		{
			if (containers.TryAdd(container.Name, container))
			{
				return true;
			}

			return false;
		}

		public bool Remove(string name)
		{
			if (containers.TryRemove(name, out _))
			{
				return true;
			}

			return false;
		}

		public bool Import(string name, List<(DateTime timestamp, string id, string data)> records)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.Import(records);
			}

			return false;
		}

		public List<(DateTime timestamp, string id, string data)> Export(string name)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.Export();
			}

			return null;
		}

		// ----- CRUD -----

		public bool Insert(string name, string id, string data)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.Insert(id, data);
			}

			return false;
		}

		public string Select(string name, string id, bool remove = false)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.Select(id, remove);
			}

			return null;
		}

		public bool Update(string name, string id, string data)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.Update(id, data);
			}

			return false;
		}

		public bool Delete(string name, string id)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.Delete(id);
			}

			return false;
		}

		// ----- MultiSelect (with OrderBy) -----

		public Dictionary<string, string> MultiSelectAll(string name, bool remove = false)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.MultiSelect(remove: remove);
			}

			return null;
		}

		public Dictionary<string, string> MultiSelectLIFO(string name, int top = 0, bool remove = false)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.MultiSelect(top, OrderBy.DESC, remove);
			}

			return null;
		}

		public Dictionary<string, string> MultiSelectFIFO(string name, int top = 0, bool remove = false)
		{
			if (containers.TryGetValue(name, out var container))
			{
				return container.MultiSelect(top, OrderBy.ASC, remove);
			}

			return null;
		}
	}
}
