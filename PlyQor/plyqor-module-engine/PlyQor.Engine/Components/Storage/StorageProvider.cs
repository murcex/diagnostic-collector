namespace PlyQor.Engine.Components.Storage
{
    using System;
    using System.Collections.Generic;
    using PlyQor.Engine.Components.Storage.Internals;

    public class StorageProvider
    {
        /// <summary>
        /// Insert storage data for a single storage id, with storage indexes.
        /// </summary>
        public static int InsertKey(string collection, string id, string data, List<string> indexes)
        {
            var timestamp = DateTime.UtcNow;

            var count = InsertKeyStroage.Execute(
                timestamp,
                collection.ToUpper(),
                id.ToUpper(),
                data);

            if (indexes != null && indexes.Count > 0)
            {
                foreach (var indexId in indexes)
                {
                    count =+ InsertTagStroage.Execute(
                        timestamp,
                        collection.ToUpper(),
                        id.ToUpper(),
                        indexId.ToUpper());
                }
            }

            return count;
        }

        /// <summary>
        /// Select storage data for a single storage id.
        /// </summary>
        public static string SelectKey(string collection, string id)
        {
            return SelectKeyStroage.Execute(
                collection.ToUpper(),
                id.ToUpper());
        }

        /// <summary>
        /// List all storage indexes of a collection.
        /// </summary>
        public static List<string> SelectTags(string collection)
        {
            return SelectTagsStroage.Execute(collection.ToUpper());
        }

        /// <summary>
        /// Count all records of an storage index, of a collection.
        /// </summary>
        public static int SelectTagCount(string collection, string index)
        {
            return SelectTagCountStroage.Execute(
                collection.ToUpper(),
                index.ToUpper());
        }

        /// <summary>
        /// List a set number of storage ids of an storage index, of a collection.
        /// </summary>
        public static List<string> SelectKeyList(
            string collection,
            string tag,
            int count)
        {
            return SelectKeyListStroage.Execute(
                collection.ToUpper(),
                tag.ToUpper(),
                count);
        }

        /// <summary>
        /// Select all the storage indexes that belong to a storage id.
        /// </summary>
        public static List<string> SelectTagsByKey(string collection, string id)
        {
            return SelectTagsByKeyStroage.Execute(
                collection.ToUpper(),
                id.ToUpper());
        }

        /// <summary>
        /// Update storage id.
        /// </summary>
        public static int UpdateKey(
            string collection,
            string oldid,
            string newid)
        {
            var count = UpdateKeyStroage.Execute(
                collection,
                oldid,
                newid);

            count =+ UpdateKeyWithTagsStroage.Execute(
                 collection.ToUpper(),
                 oldid.ToUpper(),
                 newid.ToUpper());

            return count;
        }

        /// <summary>
        /// Update the storage data of a storage id.
        /// </summary>
        public static int UpdateData(
            string collection,
            string id,
            string newdata)
        {
            return UpdateDataStroage.Execute(
                collection.ToUpper(),
                id.ToUpper(),
                newdata);
        }

        /// <summary>
        /// Update an existing storage index by storage id.
        /// </summary>
        public static int UpdateTagByKey(
            string collection,
            string id,
            string oldindex,
            string newindex)
        {
            return UpdateTagByKeyStroage.Execute(
                collection.ToUpper(),
                id.ToUpper(),
                oldindex.ToUpper(),
                newindex.ToUpper());
        }

        /// <summary>
        /// Update an existing single storage index.
        /// </summary>
        public static int UpdateTag(
            string collection,
            string oldIndex,
            string newIndex)
        {
            return UpdateTagStroage.Execute(
                collection.ToUpper(),
                oldIndex.ToUpper(),
                newIndex.ToUpper());
        }

        /// <summary>
        /// Delete a single storage id.
        /// </summary>
        public static int DeleteKey(
            string collection,
            string id)
        {
            // delete key
            var count = DeleteKeyStorage.Execute(
                collection,
                id);

            // delete all tags for a key
            count += DeleteTagsByKeyStroage.Execute(
                collection.ToUpper(),
                id.ToUpper());

            return count;
        }

        /// <summary>
        /// Delete a single storage index.
        /// </summary>
        public static int DeleteTag(
            string collection,
            string index)
        {
            return DeleteTagStroage.Execute(
                collection.ToUpper(),
                index.ToUpper());
        }

        /// <summary>
        /// Delete all storage indexes for a single storage id.
        /// </summary>
        public static int DeleteTagsByKey(
            string collection,
            string id)
        {
            var count = DeleteTagsByKeyStroage.Execute(
                collection.ToUpper(),
                id.ToUpper());

            return count;
        }

        /// <summary>
        /// Delete a single storage index for a single storage id.
        /// </summary>
        public static int DeleteTagByKey(
            string collection,
            string id,
            string index)
        {
            return DeleteTagByKeyStorage.Execute(
                collection.ToUpper(),
                id.ToUpper(),
                index.ToUpper());
        }

        /// <summary>
        /// List all storage ids marked for data retention.
        /// </summary>
        public static List<string> DataRetentionId()
        {
            return SelectRetentionStroage.Execute();
        }

        public static int InsertTag(
            string collection,
            string id,
            string index)
        {
            var timestamp = DateTime.UtcNow;

            return InsertTagStroage.Execute(
                timestamp,
                collection.ToUpper(),
                id.ToUpper(),
                index.ToUpper());
        }

        public static List<string> SelectKeyListRetention(
            string collection, 
            int days)
        {
            return SelectKeyListRetentionStorage.Execute(
                collection.ToUpper(), 
                days);
        }

        public static int TraceRetention(int days)
        {
            return TraceRetentionStorage.Execute(
                days);
        }
    }
}
