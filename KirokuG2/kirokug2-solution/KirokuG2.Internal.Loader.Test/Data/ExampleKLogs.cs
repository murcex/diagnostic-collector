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
			return "##multi-log-start\r\n" +
				"#index=1\r\n" +
				"2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0\r\n" +
				"#index=2\r\n" +
				"2024-04-14T03:35:59.9913558Z,I,hylix-kiroku-injektr-eus2$Kiroku-Audit2\r\n" +
				"2024-04-14T03:35:59.9913584Z,T,testing info2\r\n" +
				"2024-04-14T03:35:59.9913675Z,M,2$test metric2$99.98\r\n" +
				"2024-04-14T03:35:59.9913729Z,B,A19D52FE$TestBlock2\r\n" +
				"2024-04-14T03:35:59.9913738Z,T,doing stuff inside the block A19D52FE\r\n" +
				"2024-04-14T03:35:59.9913745Z,SB,A19D52FE\r\n" +
				"2024-04-14T03:35:59.9913755Z,T,Test Method Info2\r\n" +
				"2024-04-14T03:35:59.9913758Z,E,testing error2\r\n" +
				"2024-04-14T03:35:59.9913762Z,SI,0\r\n" +
				"##multi-log-end";
		}

		// block mis-match
		public static string InstanceLogFailure_1()
		{
			return "2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C32FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}

		// incorrect type
		public static string InstanceLogFailure_2()
		{
			return "2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C32FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,X,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}

		// line break
		public static string InstanceLogFailure_3()
		{
			return "2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$te\r\n" +
				"st metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C32FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,X,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}

		// incorrect type
		public static string InstanceLogFailure_4()
		{
			return "2024-04-14T03:34:59.9913558Z,I,hylix-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff, inside, the block, A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,X,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}
	}
}
