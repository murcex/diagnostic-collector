namespace PlyQor.Resources
{
    class SqlValues
    {
        // proc vars
        public static string TimeStamp { get; } = "dt_timestamp";
        public static string Container { get; } = "nvc_container";
        public static string Id { get; } = "nvc_id";
        public static string Data { get; } = "nvc_data";
        public static string Days { get; } = "i_days";
        public static string Top { get; } = "top";
        public static string Count { get; } = "i_count";
        public static string OldId { get; } = "nvc_old_id";
        public static string NewId { get; } = "nvc_new_id";
        public static string OldData { get; } = "nvc_old_data";
        public static string NewData { get; } = "nvc_new_data";

        // delete
        public static string DeleteKeyStorage { get; } = "usp_PlyQor_Data_DeleteKey";
        public static string DeleteKeyTagStorage { get; } = "usp_PlyQor_Tag_DeleteKeyTag";
        public static string DeleteKeyTagsStorage { get; } = "usp_PlyQor_Tag_DeleteKeyTags";
        public static string DeleteTagStorage { get; } = "usp_PlyQor_Tag_DeleteTag";

        // insert
        public static string InsertKeyStorage { get; } = "usp_PlyQor_Data_InsertKey";
        public static string InsertTagStorage { get; } = "usp_PlyQor_Tag_InsertTag";

        // select
        public static string SelectRetentionKeysStorage { get; } = "usp_PlyQor_Data_SelectRetentionKeys";
        public static string SelectKeyListStorage { get; } = "usp_PlyQor_Tag_SelectKeyList";
        public static string SelectKeyStorage { get; } = "usp_PlyQor_Data_SelectKey";
        public static string SelectTagCountStorage { get; } = "usp_PlyQor_Tag_SelectTagCount";
        public static string SelectKeyTagsStorage { get; } = "usp_PlyQor_Tag_SelectKeyTags";
        public static string SelectTagsStorage { get; } = "usp_PlyQor_Tag_SelectTags";

        // system / retention
        public static string TraceRetentionStorage { get; } = "usp_PlyQor_Trace_TraceRetention";

        // update
        public static string UpdateDataStorage { get; } = "usp_PlyQor_Data_UpdateData";
        public static string UpdateKeyStorage { get; } = "usp_PlyQor_Data_UpdateKey";
        public static string UpdateKeyTagsStorage { get; } = "usp_PlyQor_Tag_UpdateKeyTags";
        public static string UpdateKeyTagStorage { get; } = "usp_PlyQor_Tag_UpdateKeyTag";
        public static string UpdateTagStorage { get; } = "usp_PlyQor_Tag_UpdateTag";
    }
}
