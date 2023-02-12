namespace PlyQor.Function.Processor.Core
{
    using PlyQor.Engine;

    class Initializer
    {
        private static bool _initialized = false;

        public static void Execute()
        {
            if (!_initialized)
            {
                _initialized = PlyQorManager.Initialize();
            }
        }
    }
}
