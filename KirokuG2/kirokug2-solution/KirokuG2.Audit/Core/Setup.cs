namespace KirokuG2.Injektr.Core
{
    public class Setup
    {
        private static bool _initialized;

        public static void Execute()
        {
            if (!_initialized)
            {
                var kiroku = KManager.Configure(false);

                _initialized = kiroku;
            }
        }
    }
}
