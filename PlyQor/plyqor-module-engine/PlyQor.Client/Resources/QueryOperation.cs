namespace PlyQor.Client.Resources
{
    public class QueryOperation
    {
        public static string InsertKey { get; } = "InsertKey";
        public static string InsertTag { get; } = "InsertTag";

        public static string SelectKey { get; } = "SelectKey";
        public static string SelectTags { get; } = "SelectTags";
        public static string SelectTagCount { get; } = "SelectTagCount";
        public static string SelectKeyList { get; } = "SelectKeyList";
        public static string SelectTagsByKey { get; } = "SelectTagsByKey";

        public static string UpdateKey { get; } = "UpdateKey";
        public static string UpdateData { get; } = "UpdateData";
        public static string UpdateTagByKey { get; } = "UpdateTagByKey";
        public static string UpdateTag { get; } = "UpdateTag";

        public static string DeleteKey { get; } = "DeleteKey";
        public static string DeleteTag { get; } = "DeleteTag";
        public static string DeleteTagsByKey { get; } = "DeleteTagsByKey";
        public static string DeleteTagByKey { get; } = "DeleteTagByKey";
    }
}
