using System.Collections.Generic;
using Spectre.Console;
namespace CSharpLearning.NetflixStyle.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsKidsProfile { get; set; }
        public List<Content> _watchList = new();
        public List<WatchHistoryEntry> _watchHistory = new();


        public Profile(string id, string name, string avatarUrl = "default-avatar.png", bool isKidsProfile = false)
        {
            Id = id;
            Name = name;
            AvatarUrl = avatarUrl;
            IsKidsProfile = isKidsProfile;
        }



        public void AddToWatchlist(Content content)
        {
            if (!_watchList.Contains(content))
            {
                _watchList.Add(content);
                AnsiConsole.WriteLine($"[green]Added {content.Title} to watchlist[/]");
            }
        }

        public void RemoveFromWatchlist(Content content)
        {
            if (_watchList.Remove(content))
            {
                AnsiConsole.WriteLine($"[red]Removed {content.Title} from watchlist[/]");
            }
        }
        public IReadOnlyList<Content> GetWatchlist() => _watchList.AsReadOnly();
        public void AddToWatchHistory(Content content, double watchedPercentage)
        {
            var entry = new WatchHistoryEntry(content, watchedPercentage);
            _watchHistory.Add(entry);
        }
        public IReadOnlyList<WatchHistoryEntry> GetWatchHistory() => _watchHistory.AsReadOnly();

        public double GetWatchProgress(Content content)
        {
            var lastWatch = _watchHistory.FindLast(h => h.Content == content);
            return lastWatch?.WatchedPercentage ?? 0;
        }
        public override string ToString() => Name;


    }
}
