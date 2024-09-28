using PlyQor.Internal.Engine.Components.Storage.Adapter;
using System;
using System.Collections.Generic;

namespace PlyQor.Internal.Engine.Components.Storage.SQL
{
	public class SQLAdapter : IStorageAdapter
	{
		private static string _connectionString;

		public SQLAdapter(string connectionString)
		{
			_connectionString = connectionString;
		}

		public int InsertKey(string container, string id, string data, List<string> indexes)
		{
			throw new NotImplementedException();
		}

		public int InsertTagStorage(DateTime timestamp, string container, string id, string index)
		{
			throw new NotImplementedException();
		}

		public string SelectKey(string container, string id)
		{
			throw new NotImplementedException();
		}

		public List<string> SelectTags(string container)
		{
			throw new NotImplementedException();
		}

		public int SelectTagCount(string container, string index)
		{
			throw new NotImplementedException();
		}

		public List<string> SelectKeyList(string container, string tag, int count)
		{
			throw new NotImplementedException();
		}

		public List<string> SelectKeyTags(string container, string id)
		{
			throw new NotImplementedException();
		}

		public int UpdateKey(string container, string oldid, string newid)
		{
			throw new NotImplementedException();
		}

		public int UpdateData(string container, string id, string newdata)
		{
			throw new NotImplementedException();
		}

		public int UpdateKeyTags(string container, string oldid, string newid)
		{
			throw new NotImplementedException();
		}

		public int UpdateKeyTag(string container, string id, string oldindex, string newindex)
		{
			throw new NotImplementedException();
		}

		public int UpdateTag(string container, string oldIndex, string newIndex)
		{
			throw new NotImplementedException();
		}

		public int DeleteKey(string container, string id)
		{
			throw new NotImplementedException();
		}

		public int DeleteTag(string container, string index)
		{
			throw new NotImplementedException();
		}

		public int DeleteKeyTags(string container, string id)
		{
			throw new NotImplementedException();
		}

		public int DeleteKeyTag(string container, string id, string index)
		{
			throw new NotImplementedException();
		}

		public int InsertTag(string container, string id, string index)
		{
			throw new NotImplementedException();
		}

		public List<string> SelectRetentionKeys(string container, int days)
		{
			throw new NotImplementedException();
		}

		public int TraceRetention(int days)
		{
			throw new NotImplementedException();
		}
	}
}
