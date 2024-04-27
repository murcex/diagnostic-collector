namespace AyrQor
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;

	public enum OrderBy
	{
		ASC,   // ASC
		DESC    // DESC  
	}

	public class AyrQorContainer
	{
		/// <summary>
		/// Internal Id + System Data
		/// </summary>
		private ConcurrentDictionary<string, string> System { get; set; }

		/// <summary>
		/// Id + TimeStamp
		/// </summary>
		private ConcurrentDictionary<string, DateTime> TimeStamp { get; set; }

		/// <summary>
		/// Id + Data
		/// </summary>
		private ConcurrentDictionary<string, string> Storage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		private int size;

		/// <summary>
		/// 
		/// </summary>
		private AyrQorContainerOptions options;

		/// <summary>
		/// Container Name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public int Size => size;

		/// <summary>
		/// 
		/// </summary>
		public int Count()
		{
			return Storage.Count;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		public AyrQorContainer(string name, AyrQorContainerOptions options = null)
		{
			this.Name = name;

			this.options = options ?? new AyrQorContainerOptions();

			System = new ConcurrentDictionary<string, string>();
			TimeStamp = new ConcurrentDictionary<string, DateTime>();
			Storage = new ConcurrentDictionary<string, string>();

			System.TryAdd("Watermark", DateTime.UtcNow.ToString());

			size = 0;
		}

		/// <summary>
		/// Insert key value pair record.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Insert(string key, string value)
		{
			PolicyCheck(value);

			key = key.ToUpper();

			if (TimeStamp.TryAdd(key, DateTime.UtcNow))
			{
				Storage.TryAdd(key, value);
			}
			else
			{
				return false;
			}

			size += value.Length;

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="remove"></param>
		/// <returns></returns>
		public string Select(string key, bool remove = false)
		{
			key = key.ToUpper();

			if (Storage.TryGetValue(key, out string cargo))
			{
				if (remove)
				{
					Delete(key);
				}

				return cargo;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Delete(string key)
		{
			key = key.ToUpper();

			if (Storage.TryGetValue(key, out string value))
			{
				TimeStamp.TryRemove(key, out _);
				Storage.TryRemove(key, out _);

				size -= value.Length;

				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool Update(string key, string value)
		{
			key = key.ToUpper();

			if (Storage.TryGetValue(key, out string oldValue))
			{
				TimeStamp[key] = DateTime.UtcNow;
				Storage[key] = value;

				size -= oldValue.Length;
				size += value.Length;

				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="order"></param>
		/// <param name="top"></param>
		/// <param name="remove"></param>
		/// <returns></returns>
		public Dictionary<string, string> MultiSelect(int top = 0, OrderBy order = OrderBy.DESC, bool remove = false)
		{
			Dictionary<string, string> data = new();

			if (top == 0)
			{
				foreach (var record in Storage)
				{
					data.Add(record.Key, record.Value);
				}

				if (remove)
				{
					Storage.Clear();
					TimeStamp.Clear();

					size = 0;
				}

				return data;
			}

			if (order == OrderBy.DESC)
			{
				var datetimesDescending = TimeStamp.OrderByDescending(x => x.Value)
					.Take(top)
					.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

				foreach (var record in datetimesDescending)
				{
					data.Add(record.Key, Select(record.Key, remove: remove));
				}

				return data;
			}

			if (order == OrderBy.ASC)
			{
				var datetimesAscending = TimeStamp.OrderBy(x => x.Value)
					.Take(top)
					.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

				foreach (var record in datetimesAscending)
				{
					data.Add(record.Key, Select(record.Key, remove: remove));
				}

				return data;
			}

			return data;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="records"></param>
		/// <returns></returns>
		public bool Import(List<(DateTime timestamp, string id, string data)> records)
		{
			foreach (var record in records)
			{
				PolicyCheck(record.data);

				var id = record.id.ToUpper();

				if (TimeStamp.TryAdd(id, DateTime.UtcNow))
				{
					Storage.TryAdd(id, record.data);
				}
				else
				{
					return false;
				}

				size += record.data.Length;
			}

			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<(DateTime timestamp, string id, string data)> Export()
		{
			List<(DateTime timestamp, string id, string data)> records = new();

			foreach (var record in TimeStamp)
			{
				if (Storage.TryGetValue(record.Key, out var data))
				{
					records.Add((record.Value, record.Key, data));
				}
			}

			return records;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string Ping()
		{
			System.TryGetValue("Watermark", out var watermark);

			return watermark;
		}

		private bool PolicyCheck(string data)
		{
			if (data.Length > options.MaxSize
				&& options.MaxSize > 0)
			{
				return false;
			}

			if (Storage.Count > options.MaxCount
				&& options.MaxCount > 0)
			{
				return false;
			}

			return true;
		}
	}
}
