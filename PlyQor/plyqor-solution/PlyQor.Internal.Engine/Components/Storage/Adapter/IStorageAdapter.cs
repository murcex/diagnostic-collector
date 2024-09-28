using System;
using System.Collections.Generic;

namespace PlyQor.Internal.Engine.Components.Storage.Adapter
{
	public interface IStorageAdapter
	{
		public int InsertKey(
			string container,
			string id,
			string data,
			List<string> indexes);

		public int InsertTagStorage(
			DateTime timestamp,
			string container,
			string id,
			string index);

		public string SelectKey(
			string conatiner,
			string id);

		public List<string> SelectTags(
			string container);

		public int SelectTagCount(
			string container,
			string index);

		public List<string> SelectKeyList(
			string container,
			string tag,
			int count);

		public List<string> SelectKeyTags(
			string container,
			string id);

		public int UpdateKey(
			string container,
			string oldid,
			string newid);

		public int UpdateKeyTags(
			string container,
			string oldid,
			string newid);

		public int UpdateData(
			string container,
			string id,
			string newdata);

		public int UpdateKeyTag(
			string container,
			string id,
			string oldindex,
			string newindex);

		public int UpdateTag(
			string container,
			string oldIndex,
			string newIndex);

		public int DeleteKey(
			string container,
			string id);

		public int DeleteTag(
			string container,
			string index);

		public int DeleteKeyTags(
			string container,
			string id);

		public int DeleteKeyTag(
			string container,
			string id,
			string index);

		public int InsertTag(
			string container,
			string id,
			string index);

		public List<string> SelectRetentionKeys(
			string container,
			int days);

		public int TraceRetention(
			int days);

	}
}
