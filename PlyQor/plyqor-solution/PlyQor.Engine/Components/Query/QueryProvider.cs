namespace PlyQor.Engine.Components.Query
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Engine.Components.Query.Internals;
    using PlyQor.Engine.Components.Query.Internals.Metric;

    class QueryProvider
    {
        // Insert

        public static Dictionary<string, string> InsertKey(RequestManager requestManager)
        {
            return InsertKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> InsertTag(RequestManager requestManager)
        {
            return InsertTagQuery.Execute(requestManager);
        }

        // Select

        public static Dictionary<string, string> SelectKey(RequestManager requestManager)
        {
            return SelectKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> SelectTags(RequestManager requestManager)
        {
            return SelectTagsQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> SelectTagCount(RequestManager requestManager)
        {
            return SelectTagCountQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> SelectKeyList(RequestManager requestManager)
        {
            return SelectKeyListQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> SelectKeyTags(RequestManager requestManager)
        {
            return SelectKeyTagsQuery.Execute(requestManager);
        }

        // Update

        public static Dictionary<string, string> UpdateKey(RequestManager requestManager)
        {
            return UpdateKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> UpdateData(RequestManager requestManager)
        {
            return UpdateDataQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> UpdateKeyTag(RequestManager requestManager)
        {
            return UpdateKeyTagQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> UpdateTag(RequestManager requestManager)
        {
            return UpdateTagQuery.Execute(requestManager);
        }

        // Delete

        public static Dictionary<string, string> DeleteKey(RequestManager requestManager)
        {
            return DeleteKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DeleteTag(RequestManager requestManager)
        {
            return DeleteTagQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DeleteKeyTags(RequestManager requestManager)
        {
            return DeleteKeyTagsQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DeleteKeyTag(RequestManager requestManager)
        {
            return DeleteKeyTagQuery.Execute(requestManager);
        }

        // Retention

        public static Dictionary<string, string> DataRetention(RequestManager requestManager)
        {
            return DataRetentionQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> TraceRetention(RequestManager requestManager)
        {
            return TraceRetentionQuery.Execute(requestManager);
        }

        // Etc..

        public static Dictionary<string, string> InsertMetric(RequestManager requestManager)
        {
            return InsertMetricQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> PreMetricCollection(RequestManager requestManager)
        {
            return PreMetricCollectionQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> PostMetricCollection(RequestManager requestManager)
        {
            return PostMetricCollectionQuery.Execute(requestManager);
        }
    }
}
