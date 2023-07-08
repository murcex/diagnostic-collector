namespace PlyQor.Audit.TestCases.PlyClient
{
    using PlyQor.Audit.Core;

    class PlyClientTestProvider
    {
        public static void Execute()
        {
            if (Configuration.BaselineTest)
            {
                BaselineTest.Execute();
            }

            if (Configuration.DocumentTest)
            {
                DocumentTest.Execute();
            }
        }

    }
}
