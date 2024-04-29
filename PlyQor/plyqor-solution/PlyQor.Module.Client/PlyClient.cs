namespace PlyQor.Client
{
	public class PlyClient
	{
		private string _uri;

		private string _token;

		private string _container;

		public PlyClient(string uri, string container, string token)
		{
			if (string.IsNullOrEmpty(uri))
			{
				throw new ArgumentNullException(nameof(uri));
			}

			if (string.IsNullOrEmpty(container))
			{
				throw new ArgumentNullException(nameof(container));
			}

			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentNullException(nameof(token));
			}

			_uri = uri;
			_container = container;
			_token = token;
		}

		/// <summary>
		/// Insert an Item with a Tag.
		/// </summary>
		public Dictionary<string, string> Insert(string key, string data, string tag)
		{
			List<string> tags = new List<string>();
			tags.Add(tag);

			return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
		}

		/// <summary>
		/// Insert an Item with two Tags.
		/// </summary>
		public Dictionary<string, string> Insert(string key, string data, string tag_1, string tag_2)
		{
			List<string> tags = new List<string>();
			tags.Add(tag_1);
			tags.Add(tag_2);

			return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
		}

		/// <summary>
		/// Insert an Item with three Tags.
		/// </summary>
		public Dictionary<string, string> Insert(string key, string data, string tag_1, string tag_2, string tag_3)
		{
			List<string> tags = new List<string>();
			tags.Add(tag_1);
			tags.Add(tag_2);
			tags.Add(tag_3);

			return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
		}

		/// <summary>
		/// Insert an Item with a list of Tags.
		/// </summary>
		public Dictionary<string, string> Insert(string key, string data, List<string> tags)
		{
			return InsertKeyInternal.Execute(_uri, _container, _token, key, data, tags);
		}

		/// <summary>
		/// Append a Tag to an existing Item.
		/// </summary>
		public Dictionary<string, string> InsertTag(string key, string tag)
		{
			return InsertTagInternal.Execute(_uri, _container, _token, key, tag);
		}

		/// <summary>
		/// Select an Item by Key.
		/// </summary>
		public Dictionary<string, string> Select(string key)
		{
			return SelectKeyInternal.Execute(_uri, _container, _token, key);
		}

		/// <summary>
		/// Select a list of Item Keys by Tag.
		/// </summary>
		public Dictionary<string, string> Select(string key, int count)
		{
			return SelectKeyListInternal.Execute(_uri, _container, _token, key, count);
		}

		/// <summary>
		/// Select the count of Items by Tag.
		/// </summary>
		public Dictionary<string, string> SelectCount(string tag)
		{
			return SelectTagCountInternal.Execute(_uri, _container, _token, tag);
		}

		/// <summary>
		/// Select all the Tags appended to an Item.
		/// </summary>
		public Dictionary<string, string> SelectTags(string key)
		{
			return SelectKeyTagsInternal.Execute(_uri, _container, _token, key);
		}

		/// <summary>
		/// Select all Tags in the Container.
		/// </summary>
		public Dictionary<string, string> SelectTags()
		{
			return SelectTagsInternal.Execute(_uri, _container, _token);
		}

		/// <summary>
		/// Update the Item's Value.
		/// </summary>
		public Dictionary<string, string> UpdateData(string key, string data)
		{
			return UpdateDataInternal.Execute(_uri, _container, _token, key, data);
		}

		/// <summary>
		/// Update the Item's Key.
		/// </summary>
		public Dictionary<string, string> Update(string key, string newKey)
		{
			return UpdateKeyInternal.Execute(_uri, _container, _token, key, newKey);
		}

		/// <summary>
		/// Update a Tag appended to an Item.
		/// </summary>
		public Dictionary<string, string> UpdateTag(string key, string tag, string newTag)
		{
			return UpdateKeyTagInternal.Execute(_uri, _container, _token, key, tag, newTag);
		}

		/// <summary>
		/// Update the value of a Tag set.
		/// </summary>
		public Dictionary<string, string> UpdateTag(string tag, string newTag)
		{
			return UpdateTagInternal.Execute(_uri, _container, _token, tag, newTag);
		}

		/// <summary>
		/// Delete an Item.
		/// </summary>
		public Dictionary<string, string> Delete(string key)
		{
			return DeleteKeyInternal.Execute(_uri, _container, _token, key);
		}

		/// <summary>
		/// Delete a Tag from an Item.
		/// </summary>
		public Dictionary<string, string> DeleteTag(string key, string tag)
		{
			return DeleteKeyTagInternal.Execute(_uri, _container, _token, key, tag);
		}

		/// <summary>
		/// Delete a Tag set.
		/// </summary>
		public Dictionary<string, string> DeleteTag(string tag)
		{
			return DeleteTagInternal.Execute(_uri, _container, _token, tag);
		}

		/// <summary>
		/// Delete all Tags from an Item.
		/// </summary>
		public Dictionary<string, string> DeleteTags(string key)
		{
			return DeleteKeyTagsInternal.Execute(_uri, _container, _token, key);
		}
	}
}
