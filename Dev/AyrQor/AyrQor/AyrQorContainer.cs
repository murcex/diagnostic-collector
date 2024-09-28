namespace AyrQor
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// 
	/// </summary>
	public enum OrderBy
	{
		ASC,   // Ascending order
		DESC    // Descending order
	}

	public class AyrQorContainer
	{
		/// <summary>
		/// The system dictionary for the container
		/// </summary>
		private ConcurrentDictionary<string, string> System { get; set; }

		/// <summary>
		/// The timestamp dictionary for the container
		/// </summary>
		private ConcurrentDictionary<string, DateTime> TimeStamp { get; set; }

		/// <summary>
		/// The storage dictionary for the container
		/// </summary>
		private ConcurrentDictionary<string, string> Storage { get; set; }


		private ConcurrentDictionary<string, ConcurrentDictionary<string, DateTime>> Tags { get; set; }

		/// <summary>
		/// Total size of the container
		/// </summary>
		private int size;

		/// <summary>
		/// Options for the container
		/// </summary>
		private AyrQorContainerOptions options;

		/// <summary>
		/// Container Name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Size of the container
		/// </summary>
		public int Size => size;

		/// <summary>
		/// Get the count of records in the container
		/// </summary>
		/// <returns>The count of records</returns>
		public int Count()
		{
			return Storage.Count;
		}

		/// <summary>
		/// Initializes a new instance of the AyrQorContainer class with the specified name and options.
		/// </summary>
		/// <param name="name">The name of the container</param>
		/// <param name="options">The options for the container</param>
		public AyrQorContainer(string name, AyrQorContainerOptions options = null)
		{
			this.Name = name;

			this.options = options ?? new AyrQorContainerOptions();

			System = new ConcurrentDictionary<string, string>();
			TimeStamp = new ConcurrentDictionary<string, DateTime>();
			Storage = new ConcurrentDictionary<string, string>();
			Tags = new ConcurrentDictionary<string, ConcurrentDictionary<string, DateTime>>();

			System.TryAdd("Watermark", DateTime.UtcNow.ToString());

			size = 0;
		}

		/// <summary>
		/// Insert a key-value pair record into the container.
		/// </summary>
		/// <param name="key">The key of the record</param>
		/// <param name="value">The value of the record</param>
		/// <returns>True if the record is inserted successfully, otherwise false</returns>
		public bool Insert(string key, string value, string tag = null)
		{
			PolicyCheck(value);

			key = key.ToUpper();

			var tagged = false;
			if (!string.IsNullOrEmpty(tag))
			{
				tagged = true;
				tag = tag?.ToUpper();
			}

			var timeStamp = DateTime.UtcNow;

			if (TimeStamp.TryAdd(key, timeStamp))
			{
				Storage.TryAdd(key, value);

				if (tagged)
				{
					if (Tags.TryGetValue(tag, out var tagDictionary))
					{
						tagDictionary.TryAdd(key, timeStamp);
					}
					else
					{
						tagDictionary = new ConcurrentDictionary<string, DateTime>();
						tagDictionary.TryAdd(tag, timeStamp);

						Tags.TryAdd(tag, new ConcurrentDictionary<string, DateTime>());
					}
				}
			}
			else
			{
				return false;
			}

			size += value.Length;

			return true;
		}

		/// <summary>
		/// Select a record from the container by key.
		/// </summary>
		/// <param name="key">The key of the record</param>
		/// <param name="remove">True to remove the record after selecting, otherwise false</param>
		/// <returns>The value of the selected record, or null if the record is not found</returns>
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
		/// Delete a record from the container by key.
		/// </summary>
		/// <param name="key">The key of the record</param>
		/// <returns>True if the record is deleted successfully, otherwise false</returns>
		public bool Delete(string key)
		{
			key = key.ToUpper();

			if (Storage.TryGetValue(key, out string value))
			{
				TimeStamp.TryRemove(key, out _);
				Storage.TryRemove(key, out _);

				foreach (var tagDictionary in Tags.Values)
				{
					tagDictionary.TryRemove(key, out _);
				}

				size -= value.Length;

				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Update a record in the container by key.
		/// </summary>
		/// <param name="key">The key of the record</param>
		/// <param name="value">The new value of the record</param>
		/// <returns>True if the record is updated successfully, otherwise false</returns>
		public bool Update(string key, string value)
		{
			key = key.ToUpper();

			if (Storage.TryGetValue(key, out string oldValue))
			{				
				TimeStamp[key] = DateTime.UtcNow;
				Storage[key] = value;

				foreach (var tagDictionary in Tags.Values)
				{
					if (tagDictionary.ContainsKey(key))
					{
						tagDictionary[key] = DateTime.UtcNow;
					}
				}

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
		/// Select multiple records from the container.
		/// </summary>
		/// <param name="top">The maximum number of records to select</param>
		/// <param name="order">The order in which to select the records</param>
		/// <param name="remove">True to remove the selected records after selecting, otherwise false</param>
		/// <returns>A dictionary containing the selected records</returns>
		public Dictionary<string, string> MultiSelect(string tag = null, int top = 0, OrderBy order = OrderBy.DESC, bool remove = false)
		{
			Dictionary<string, string> data = new();

			var tagged = false;
			if (!string.IsNullOrEmpty(tag))
			{
				tagged = true;
				tag = tag?.ToUpper();
			}

			if (top == 0)
			{
				if (tagged)
				{
					//...
				}
				else
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
				}

				return data;
			}

			if (order == OrderBy.DESC)
			{
				if (tagged)
				{
					if (Tags.ContainsKey(tag))
					{
						var datetimesDescending = Tags[tag].OrderByDescending(x => x.Value)
						.Take(top)
						.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

						foreach (var record in datetimesDescending)
						{
							data.Add(record.Key, Select(record.Key, remove: remove));
						}
					}
				}
				else
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
			}

			if (order == OrderBy.ASC)
			{
				if (tagged)
				{
					if (Tags.ContainsKey(tag))
					{
						var datetimesDescending = Tags[tag].OrderBy(x => x.Value)
						.Take(top)
						.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

						foreach (var record in datetimesDescending)
						{
							data.Add(record.Key, Select(record.Key, remove: remove));
						}
					}
				}
				else
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
			}

			return data;
		}

		/// <summary>
		/// Import records into the container.
		/// </summary>
		/// <param name="records">The list of records to import</param>
		/// <returns>True if the records are imported successfully, otherwise false</returns>
		public bool Import(List<(DateTime timestamp, string id, string data, List<string> tags)> records)
		{
			foreach (var record in records)
			{
				PolicyCheck(record.data);

				var id = record.id.ToUpper();

				bool tagged = false;
				List<string> tags = null;
				if (record.tags.Any())
				{
					tagged = true;
					tags = record.tags.Where(tag => !string.IsNullOrEmpty(tag)).Select(tag => tag.ToUpper()).ToList();
				}

				if (TimeStamp.TryAdd(id, DateTime.UtcNow))
				{
					Storage.TryAdd(id, record.data);

					if (tagged)
					{
						foreach (var tag in tags)
						{
							if (Tags.TryGetValue(tag, out var tagDictionary))
							{
								tagDictionary.TryAdd(id, DateTime.UtcNow);
							}
							else
							{
								tagDictionary = new ConcurrentDictionary<string, DateTime>();
								tagDictionary.TryAdd(id, DateTime.UtcNow);

								Tags.TryAdd(tag, tagDictionary);
							}
						}
					}
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
		/// Export all records from the container.
		/// </summary>
		/// <returns>A list of exported records</returns>
		public List<(DateTime timestamp, string id, string data, List<string> tags)> Export()
		{
			List<(DateTime timestamp, string id, string data, List<string> tags)> records = new();

			foreach (var record in TimeStamp)
			{
				if (Storage.TryGetValue(record.Key, out var data))
				{
					List<string> tags = new();
					foreach (var tagDictionary in Tags)
					{
						if (tagDictionary.Value.ContainsKey(record.Key))
						{
							tags.Add(tagDictionary.Key.ToString().ToUpper());
						}
					}

					records.Add((record.Value, record.Key, data, tags));
				}
			}

			return records;
		}

		/// <summary>
		/// Get the watermark of the container.
		/// </summary>
		/// <returns>The watermark value</returns>
		public string Ping()
		{
			System.TryGetValue("Watermark", out var watermark);

			return watermark;
		}

		/// <summary>
		/// Check the policy for the given data.
		/// </summary>
		/// <param name="data">The data to check</param>
		/// <returns>True if the data satisfies the policy, otherwise false</returns>
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
