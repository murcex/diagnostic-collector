namespace PlyQor.Engine.Components.Query
{
    using System.Collections.Generic;
    using PlyQor.Models;
    using PlyQor.Engine.Components.Query.Internals;

    class QueryProvider
    {
        public static Dictionary<string, string> InsertKey(RequestManager requestManager)
        {
            return InsertKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> InsertTag(RequestManager requestManager)
        {
            return InsertTagQuery.Execute(requestManager);
        }

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

        public static Dictionary<string, string> SelectTagsByKey(RequestManager requestManager)
        {
            return SelectTagsByKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DataRetention(RequestManager requestManager)
        {
            return DataRetentionQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> UpdateKey(RequestManager requestManager)
        {
            return UpdateKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> UpdateData(RequestManager requestManager)
        {
            return UpdateDataQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> UpdateTagByKey(RequestManager requestManager)
        {
            return UpdateTagByKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> UpdateTag(RequestManager requestManager)
        {
            return UpdateTagQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DeleteKey(RequestManager requestManager)
        {
            return DeleteKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DeleteTag(RequestManager requestManager)
        {
            return DeleteTagQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DeleteTagsByKey(RequestManager requestManager)
        {
            return DeleteTagsByKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> DeleteTagByKey(RequestManager requestManager)
        {
            return DeleteTagByKeyQuery.Execute(requestManager);
        }

        public static Dictionary<string, string> TraceRetention(RequestManager requestManager)
        {
            return TraceRetentionQuery.Execute(requestManager);
        }
    }
}
