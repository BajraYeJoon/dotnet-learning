using System;
using CSharpLearning.NetflixStyle.Interfaces;
using Spectre.Console;

namespace CSharpLearning.NetflixStyle.Models
{
    public class Movie : Content, IPlayable, IStreamable
    {

        // additional for movie only so
        public int DurationMinutes { get; set; }
        public bool IsPlaying { get; private set; }

        public Movie(
            string id,
            string title,
            string description,
            int releaseYear,
            string[] genres,
            int durationMinutes
        ) : base(id, title, description, releaseYear, genres)
        {
            DurationMinutes = durationMinutes;
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
            return $"{base.GetInfo()} - {GetDuration()}";
        }
    }

}
