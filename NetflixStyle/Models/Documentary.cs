using CSharpLearning.NetflixStyle.Interfaces;
using Spectre.Console;

namespace CSharpLearning.NetflixStyle.Models
{
    public class Documentary : Content, IPlayable, IStreamable, IRatable
    {
        public int DurationMinutes { get; set; }
        public string Topic { get; set; }
        public bool IsPlaying { get; private set; }
        private Dictionary<int, double> _userRatings = new();


        public Documentary(string id, string title, string description, int releaseYear, string[] genres, int durationMinutes, string topic) : base(id, title, description, releaseYear, genres)
        {
            DurationMinutes = durationMinutes;
            Topic = topic;
            IsPlaying = false;
        }

        public override string GetDuration()
        {
            return $"{DurationMinutes} minutes";
        }

        public void Play()
        {
            IsPlaying = true;
            AnsiConsole.WriteLine($"[yellow]Playing {Title}...[/]");
        }

        public void Pause()
        {
            IsPlaying = false;
            AnsiConsole.WriteLine($"[yellow]Paused {Title}[/]");
        }

        public void Stop()
        {
            IsPlaying = false;
            AnsiConsole.WriteLine($"[yellow]Stopped {Title}[/]");
        }

        public double GetStreamQuality()
        {
            return 1080;
        }

        public bool IsAvailableInRegion(string region)
        {
            return true;
        }


        public string[] GetAvailableSubtitles()
        {
            return new[] { "English", "French", "Spanish" };
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()} - Topic: {Topic}, Duration: {GetDuration()}";

        }

        //i ratable intervention
        public double GetAverageRating()
        {
            if (_userRatings.Count == 0) return 0;
            return _userRatings.Values.Average();
        }

        public void AddUserRating(int userId, double rating)
        {
            if (rating < 0 || rating > 5) throw new ArgumentException("Rating must be between 0 and 5");

            _userRatings[userId] = rating;
            AnsiConsole.WriteLine($"[green]Added rating {rating} from user {userId}[/]");
        }

        public bool HasUserRated(int userId)
        {
            return _userRatings.ContainsKey(userId);
        }

        public string[] GetUserReviews()
        {
            return new[] { "Great documentary!", "I learned a lot.", "Not my favorite" };
        }


    }
}
