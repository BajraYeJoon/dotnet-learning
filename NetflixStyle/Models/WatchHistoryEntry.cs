using System;

namespace CSharpLearning.NetflixStyle.Models
{
    public class WatchHistoryEntry
    {
        public Content Content { get; }
        public DateTime WatchedDate { get; }
        public double WatchedPercentage { get; private set; }

        public WatchHistoryEntry(Content content, double watchedPercentage)
        {
            Content = content;
            WatchedDate = DateTime.Now;
            WatchedPercentage = Math.Clamp(watchedPercentage, 0, 100);
        }

        public void UpdateProgress(double newPercentage)
        {
            WatchedPercentage = Math.Clamp(newPercentage, 0, 100);
        }

        public override string ToString()
        {
            return $"{Content.Title} - {WatchedPercentage}% watched on {WatchedDate:g}";
        }
    }
}