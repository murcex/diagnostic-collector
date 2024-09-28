using PlyQor.Internal.Engine.Components.Storage.Memeory;
using PlyQor.Internal.Engine.Components.Storage.SQL;

namespace PlyQor.Internal.Engine.Components.Storage.Adapter
{
	public class StorageAdapterProvider
	{
		public static IStorageAdapter GetStorageAdapter(string storageType) // should take dictionary<string,string> settings -- connection string, etc.
		{
			switch (storageType.ToUpper())
			{
				case "LOCAL":
					return new MemoryAdapter();
				case "SQL":
					return new SQLAdapter("");
				default:
					return null;
			}
		}
	}
}
