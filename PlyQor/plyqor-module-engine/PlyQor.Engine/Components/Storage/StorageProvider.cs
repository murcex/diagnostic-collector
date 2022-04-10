namespace PlyQor.Engine.Components.Storage
{
    using PlyQor.Engine.Components.Storage.Internals;
    using System;
    using System.Collections.Generic;

    public class StorageProvider
    {
        /// <summary>
        /// Insert storage data for a single storage id, with storage indexes.
        /// </summary>
        public static int InsertKey(string container, string id, string data, List<string> indexes)
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
                    count =+ InsertTagStorage.Execute(
                        timestamp,
                        container.ToUpper(),
                        id.ToUpper(),
                        indexId.ToUpper());
                }
            }

            return count;
        }

        /// <summary>
        /// Select storage data for a single storage id.
        /// </summary>
        public static string SelectKey(string conatiner, string id)
        {
            return SelectKeyStorage.Execute(
                conatiner.ToUpper(),
                id.ToUpper());
        }

        /// <summary>
        /// List all storage tags of a container.
        /// </summary>
        public static List<string> SelectTags(string container)
        {
            return SelectTagsStorage.Execute(container.ToUpper());
        }

        /// <summary>
        /// Count all records of an storage tag, of a container.
        /// </summary>
        public static int SelectTagCount(string container, string index)
        {
            return SelectTagCountStorage.Execute(
                container.ToUpper(),
                index.ToUpper());
        }

        /// <summary>
        /// List a set number of storage ids of an storage tag, of a container.
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
        /// Select all the storage indexes that belong to a storage id.
        /// </summary>
        public static List<string> SelectTagsByKey(string container, string id)
        {
            return SelectTagsByKeyStorage.Execute(
                container.ToUpper(),
                id.ToUpper());
        }

        /// <summary>
        /// Update storage id.
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

            count =+ UpdateKeyWithTagsStorage.Execute(
                 container.ToUpper(),
                 oldid.ToUpper(),
                 newid.ToUpper());

            return count;
        }

        /// <summary>
        /// Update the storage data of a storage id.
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
        /// Update an existing storage index by storage id.
        /// </summary>
        public static int UpdateTagByKey(
            string container,
            string id,
            string oldindex,
            string newindex)
        {
            return UpdateTagByKeyStorage.Execute(
                container.ToUpper(),
                id.ToUpper(),
                oldindex.ToUpper(),
                newindex.ToUpper());
        }

        /// <summary>
        /// Update an existing single storage index.
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
        /// Delete a single storage id.
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
            count += DeleteTagsByKeyStorage.Execute(
                container.ToUpper(),
                id.ToUpper());

            return count;
        }

        /// <summary>
        /// Delete a single storage index.
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
        /// Delete all storage indexes for a single storage id.
        /// </summary>
        public static int DeleteTagsByKey(
            string container,
            string id)
        {
            var count = DeleteTagsByKeyStorage.Execute(
                container.ToUpper(),
                id.ToUpper());

            return count;
        }

        /// <summary>
        /// Delete a single storage index for a single storage id.
        /// </summary>
        public static int DeleteTagByKey(
            string container,
            string id,
            string index)
        {
            return DeleteTagByKeyStorage.Execute(
                container.ToUpper(),
                id.ToUpper(),
                index.ToUpper());
        }

        /// <summary>
        /// List all storage ids marked for data retention.
        /// </summary>
        public static List<string> DataRetentionId()
        {
            return SelectRetentionStorage.Execute();
        }

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

        public static List<string> SelectKeyListRetention(
            string container, 
            int days)
        {
            return SelectKeyListRetentionStorage.Execute(
                container.ToUpper(), 
                days);
        }

        public static int TraceRetention(int days)
        {
            return TraceRetentionStorage.Execute(
                days);
        }
    }
}
