namespace KirokuG2.Internal.Loader.Test.Data
{
	public class ExampleKLogs
	{
		public static string BasicInstanceLog()
		{
			return "2024-04-14T03:34:59.9913558Z,I,test-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
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
				"2024-04-14T03:34:59.9913558Z,I,test-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0\r\n" +
				"#index=2\r\n" +
				"2024-04-14T03:35:59.9913558Z,I,test-kiroku-injektr-eus2$Kiroku-Audit2\r\n" +
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
			return "2024-04-14T03:34:59.9913558Z,I,test-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C52FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}

		// incorrect type
		public static string InstanceLogFailure_2()
		{
			return "2024-04-14T03:34:59.9913558Z,I,test-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,X,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}

		// line break
		public static string InstanceLogFailure_3()
		{
			return "2024-04-14T03:34:59.9913558Z,I,test-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing info\r\n" +
					"2024-04-14T03:34:59.991\r\n" +
				"3675Z,M,2$test metric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B\r\n" +
				",A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing stuff" +
					"\r\n inside the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,E,testing error\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}

		// incorrect type
		public static string InstanceLogFailure_4()
		{
			return "2024-04-14T03:34:59.9913558Z,I,test-kiroku-injektr-wus3$Kiroku-Audit\r\n" +
				"2024-04-14T03:34:59.9913584Z,T,testing, info\r\n" +
				"2024-04-14T03:34:59.9913675Z,M,2$test me,tric$99.99\r\n" +
				"2024-04-14T03:34:59.9913729Z,B,A19C62FE$TestBlock\r\n" +
				"2024-04-14T03:34:59.9913738Z,T,doing, stuff inside, the block A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913745Z,SB,A19C62FE\r\n" +
				"2024-04-14T03:34:59.9913755Z,T,Test Method Info\r\n" +
				"2024-04-14T03:34:59.9913758Z,E,tes,ting er,ror\r\n" +
				"2024-04-14T03:34:59.9913762Z,SI,0";
		}

		// metrics
		public static string MetricInstanceLog()
		{
			return "2024-04-14T03:34:59.9913558Z,I,test-kiroku-injektr-wus3$Kiroku-Audit\r\n" +

				// pass positive
				"2024-04-14T03:34:59.9913675Z,M,2$test metric01$1\r\n" + // 1
				"2024-04-14T03:34:59.9913729Z,M,2$test metric02$1.0\r\n" + // 1.0
				"2024-04-14T03:34:59.9913675Z,M,2$test metric03$0123456789\r\n" + // 0123456789
				"2024-04-14T03:34:59.9913675Z,M,2$test metric04$.01\r\n" + // .01
				"2024-04-14T03:34:59.9913675Z,M,2$test metric05$0.02\r\n" + // 0.02
				"2024-04-14T03:34:59.9913675Z,M,2$test metric06$0123456789.01\r\n" + // 0123456789.01

				// pass negative
				"2024-04-14T03:34:59.9913675Z,M,2$test metric07$-1\r\n" + // -1
				"2024-04-14T03:34:59.9913729Z,M,2$test metric08$-1.0\r\n" + // -1.0
				"2024-04-14T03:34:59.9913675Z,M,2$test metric09$-0123456789\r\n" + // -0123456789
				"2024-04-14T03:34:59.9913675Z,M,2$test metric10$-.01\r\n" + // -.01
				"2024-04-14T03:34:59.9913675Z,M,2$test metric11$-0.02\r\n" + // -0.02
				"2024-04-14T03:34:59.9913675Z,M,2$test metric12$-0123456789.01\r\n" + // -0123456789.01

				// fail positive
				"2024-04-14T03:34:59.9913675Z,M,2$test metric13$12345678910\r\n" + // 11 digit whole - fail
				"2024-04-14T03:34:59.9913729Z,M,2$test metric14$12345678910.12\r\n" + // 11 digit left - fail
				"2024-04-14T03:34:59.9913729Z,M,2$test metric15$1234567891.123\r\n" + // 3 digit right - fail
				"2024-04-14T03:34:59.9913729Z,M,2$test metric16$12345678910.123\r\n" + // 3 digit right - fail

				// fail negative
				"2024-04-14T03:34:59.9913675Z,M,2$test metric17$-12345678910\r\n" + // 11 digit whole - fail
				"2024-04-14T03:34:59.9913729Z,M,2$test metric18$-12345678910.12\r\n" + // 11 digit left - fail
				"2024-04-14T03:34:59.9913729Z,M,2$test metric19$-1234567891.123\r\n" + // 3 digit right - fail
				"2024-04-14T03:34:59.9913729Z,M,2$test metric20$-12345678910.123\r\n" + // 3 digit right - fail

				// fail
				"2024-04-14T03:34:59.9913738Z,M,2$test metric21$test\r\n" + // string - fail
				"2024-04-14T03:34:59.9913584Z,M,2$test metric22$\r\n" + // null

				// dup check, pass
				"2024-04-14T03:34:59.9913745Z,M,2$test metric1$1\r\n" + // same metric name

				"2024-04-14T03:34:59.9913762Z,SI,0";
		}
	}
}
