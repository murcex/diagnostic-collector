namespace PlyQor.AdminTool
{
    public class Configuration
    {
        public static string DatabaseConnection { get; set; }

        public static string UpsertStoredProcedure { get; } = "usp_PlyQor_Data_UpsertSystem";

        public static string SelectStoredProcedure { get; } = "usp_PlyQor_Data_SelectSystem";

        public static string Admin { get; } = "admin";

        public static string Database { get; } = "database";

        public static string ParameterTimeStamp { get; } = "dt_timestamp";

        public static string ParameterId { get; } = "nvc_id";

        public static string ParameterData { get; } = "nvc_data";

        public static string ContainersId { get; } = "CONTAINERS";
    }

    public enum ConfigurationType
    {
        ReadOnly,
        Template,
        Modify
    }
}
