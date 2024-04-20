namespace KirokuG2.Internal.Loader.Test.Data
{
    public class ExampleKLogs
    {
        public static string BasicInstanceLog()
        {
            return "2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
                "2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
                "2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
                "2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
                "2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
                "2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
                "2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
                "2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
                "2024-04-14T03:34:59.9913762Z,SI,0";
        }

        public static string MultiInstanceLog()
        {
			return "##multi-log-start" +
                "#index=1" +
                "2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
	            "2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
	            "2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
	            "2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
	            "2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
	            "2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
	            "2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
	            "2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
	            "2024-04-14T03:34:59.9913762Z,SI,0" +
				"##multi-log-end";
		}
    }
}
