namespace PlyQor.Resources
{
    class SqlColumns
    {
        public static string TimeStamp { get; } = "dt_timestamp";
        public static string Collection { get; } = "nvc_collection";
        public static string Id { get; } = "nvc_id";
        public static string Data { get; } = "nvc_data";

        public static string Days { get; } = "i_days";
        public static string Top { get; } = "top";
        public static string Count { get; } = "i_count";
        public static string OldId { get; } = "nvc_old_id";
        public static string NewId { get; } = "nvc_new_id";
        public static string OldData { get; } = "nvc_old_data";
        public static string NewData { get; } = "nvc_new_data";

        public static string DeleteKeyStorage { get; } = "usp_PlyQor_Data_Delete";
        public static string DeleteTagByKeyStorage { get; } = "usp_PlyQor_Tag_Delete_Child";
        public static string DeleteTagsByKeyStroage { get; } = "usp_PlyQor_Tag_Delete_Parent";
        public static string DeleteTagStroage { get; } = "usp_PlyQor_Tag_Delete_Set";

        public static string InsertKeyStroage { get; } = "usp_PlyQor_Data_Insert";
        public static string InsertTagStroage { get; } = "usp_PlyQor_Tag_Insert";

        public static string SelectKeyListRetentionStorage { get; } = "usp_PlyQor_Data_Select_Retention-V2";
        public static string SelectKeyListStroage { get; } = "usp_PlyQor_Tag_List_Keys";
        public static string SelectKeyStroage { get; } = "usp_PlyQor_Data_Select";
        public static string SelectRetentionStroage { get; } = "usp_PlyQor_Data_Select_Retention";
        public static string SelectTagCountStroage { get; } = "usp_PlyQor_Tag_Count";
        public static string SelectTagsByKeyStroage { get; } = "usp_PlyQor_Tag_Select";
        public static string SelectTagsStroage { get; } = "usp_PlyQor_Tag_List_Distinct";

        public static string TraceRetentionStorage { get; } = "usp_PlyQor_Trace_Retention";

        public static string UpdateDataStroage { get; } = "usp_PlyQor_Data_Update_Data";
        public static string UpdateKeyStroage { get; } = "usp_PlyQor_Data_Update_Id";
        public static string UpdateKeyWithTagsStroage { get; } = "usp_PlyQor_Tag_Update_Id";
        public static string UpdateTagByKeyStroage { get; } = "usp_PlyQor_Tag_Update_Data";
        public static string UpdateTagStroage { get; } = "usp_PlyQor_Tag_Update_Data_Set";
    }
}
