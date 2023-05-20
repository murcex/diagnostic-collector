namespace PlyQor.Test.Utility
{
    public static class DataUtility
    {
        public static int GetWindow(int day)
        {
            int.TryParse(DateTime.UtcNow.AddDays(day).ToString("yyyyMMdd"), out var newWindow);

            return newWindow;
        }

        public static List<int> GetWindows(int day, int max, bool maxDirection, bool iterationDirection)
        {
            List<int> windows = new();

            var cycle = day;
            if (maxDirection)
            {
                while (cycle < max)
                {
                    int.TryParse(DateTime.UtcNow.AddDays(cycle).ToString("yyyyMMdd"), out var newWindow);

                    windows.Add(newWindow);

                    if (iterationDirection)
                    {
                        cycle++;
                    }
                    else
                    {
                        cycle--;
                    }
                }
            }
            else
            {
                while (cycle > max)
                {
                    int.TryParse(DateTime.UtcNow.AddDays(cycle).ToString("yyyyMMdd"), out var newWindow);

                    windows.Add(newWindow);

                    if (iterationDirection)
                    {
                        cycle++;
                    }
                    else
                    {
                        cycle--;
                    }
                }
            }

            return windows;
        }
    }
}
