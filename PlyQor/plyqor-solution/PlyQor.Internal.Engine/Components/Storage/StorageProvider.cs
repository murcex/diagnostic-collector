namespace PlyQor.Engine.Components.Storage
{
	using PlyQor.Engine.Components.Storage.Internals;
	using System;
	using System.Collections.Generic;

	public class StorageProvider
	{
		/// <summary>
		/// 
		/// </summary>
		public static int InsertKey(
			string container,
			string id,
			string data,
			List<string> indexes)
		{
			var timestamp = DateTime.UtcNow;

			var count = InsertKeyStorage.Execute(
				timestamp,
				container.ToUpper(),
				id.ToUpper(),
				data);

			if (indexes != null && indexes.Count > 0)
			{
				foreach (var indexId in indexes)
				{
					count = +InsertTagStorage.Execute(
						timestamp,
						container.ToUpper(),
						id.ToUpper(),
						indexId.ToUpper());
				}
			}

			return count;
		}

		/// <summary>
		/// 
		/// </summary>
		public static string SelectKey(
			string conatiner,
			string id)
		{
			return SelectKeyStorage.Execute(
				conatiner.ToUpper(),
				id.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static List<string> SelectTags(string container)
		{
			return SelectTagsStorage.Execute(container.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static int SelectTagCount(
			string container,
			string index)
		{
			return SelectTagCountStorage.Execute(
				container.ToUpper(),
				index.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static List<string> SelectKeyList(
			string container,
			string tag,
			int count)
		{
			return SelectKeyListStorage.Execute(
				container.ToUpper(),
				tag.ToUpper(),
				count);
		}

		/// <summary>
		/// 
		/// </summary>
		public static List<string> SelectKeyTags(
			string container,
			string id)
		{
			return SelectKeyTagsStorage.Execute(
				container.ToUpper(),
				id.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static int UpdateKey(
			string container,
			string oldid,
			string newid)
		{
			var count = UpdateKeyStorage.Execute(
				container,
				oldid,
				newid);

			count = +UpdateKeyTagsStorage.Execute(
				 container.ToUpper(),
				 oldid.ToUpper(),
				 newid.ToUpper());

			return count;
		}

		/// <summary>
		/// 
		/// </summary>
		public static int UpdateData(
			string container,
			string id,
			string newdata)
		{
			return UpdateDataStorage.Execute(
				container.ToUpper(),
				id.ToUpper(),
				newdata);
		}

		/// <summary>
		/// 
		/// </summary>
		public static int UpdateKeyTag(
			string container,
			string id,
			string oldindex,
			string newindex)
		{
			return UpdateKeyTagStorage.Execute(
				container.ToUpper(),
				id.ToUpper(),
				oldindex.ToUpper(),
				newindex.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static int UpdateTag(
			string container,
			string oldIndex,
			string newIndex)
		{
			return UpdateTagStorage.Execute(
				container.ToUpper(),
				oldIndex.ToUpper(),
				newIndex.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static int DeleteKey(
			string container,
			string id)
		{
			// delete key
			var count = DeleteKeyStorage.Execute(
				container,
				id);

			// delete all tags for a key
			count += DeleteKeyTagsStorage.Execute(
				container.ToUpper(),
				id.ToUpper());

			return count;
		}

		/// <summary>
		/// 
		/// </summary>
		public static int DeleteTag(
			string container,
			string index)
		{
			return DeleteTagStorage.Execute(
				container.ToUpper(),
				index.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static int DeleteKeyTags(
			string container,
			string id)
		{
			var count = DeleteKeyTagsStorage.Execute(
				container.ToUpper(),
				id.ToUpper());

			return count;
		}

		/// <summary>
		/// 
		/// </summary>
		public static int DeleteKeyTag(
			string container,
			string id,
			string index)
		{
			return DeleteKeyTagStorage.Execute(
				container.ToUpper(),
				id.ToUpper(),
				index.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static int InsertTag(
			string container,
			string id,
			string index)
		{
			var timestamp = DateTime.UtcNow;

			return InsertTagStorage.Execute(
				timestamp,
				container.ToUpper(),
				id.ToUpper(),
				index.ToUpper());
		}

		/// <summary>
		/// 
		/// </summary>
		public static List<string> SelectRetentionKeys(
			string container,
			int days)
		{
			return SelectRetentionKeysStorage.Execute(
				container.ToUpper(),
				days);
		}

		/// <summary>
		/// 
		/// </summary>
		public static int TraceRetention(int days)
		{
			return TraceRetentionStorage.Execute(
				days);
		}
	}
}
