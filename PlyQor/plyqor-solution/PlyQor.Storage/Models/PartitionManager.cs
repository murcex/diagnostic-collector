using PlyQor.Storage.Interfaces;

namespace PlyQor.Storage.Models
{
    public class PartitionManager : IPartitionManager
    {
        public List<int> FutureWindows()
        {
            List<int> windows = new();
            var day = 1;
            while (day < 11)
            {
                _ = int.TryParse(DateTime.UtcNow.AddDays(day).ToString("yyyyMMdd"), out var futureWindow);

                windows.Add(futureWindow);

                day++;
            }

            return windows;
        }

        public List<int> FindMissingWindows(List<int> windows, List<int> futureWindows)
        {
            List<int> missingWindows = new();

            foreach (var futureWindow in futureWindows)
            {
                if (!windows.Contains(futureWindow))
                {
                    missingWindows.Add(futureWindow);
                }
            }

            return missingWindows;
        }

        public int CutOffWindow(int retentionRange)
        {
            if (retentionRange == 0)
            {
                throw new InvalidOperationException("Retention range is invalid, must not be 0");
            }

            if (retentionRange > 0)
            {
                retentionRange *= -1;
            }

            _ = int.TryParse(DateTime.UtcNow.AddDays(retentionRange).ToString("yyyyMMdd"), out var cutoffwindow);

            return cutoffwindow;
        }

        public List<int> SelectRetentionWindows(List<int> partitions, int cutoff)
        {
            List<int> removeWindows = new();
            foreach (var partition in partitions)
            {
                if (partition < cutoff)
                {
                    removeWindows.Add(partition);
                }
            }

            return removeWindows.OrderByDescending(x => x).ToList();
        }
    }
}
