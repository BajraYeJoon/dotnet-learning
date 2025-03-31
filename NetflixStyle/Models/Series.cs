using System;
using CSharpLearning.NetflixStyle.Interfaces;
using Spectre.Console;

namespace CSharpLearning.NetflixStyle.Models
{
    public class Series : Content, IPlayable, IStreamable, IDownloadable
    {
        public int NumberOfSeasons { get; set; }
        public int EpisodesPerSeason { get; set; }
        public int EpisodeDurationMinutes { get; set; }
        public bool IsPlaying { get; private set; }
        public bool CanDownload => true;

        public Series(string id, string title, string description, int releaseYear, string[] genres, int numberOfSeasons, int episodesPerSeason, int episodeDurationMinutes) :
        base(id, title, description, releaseYear, genres)
        {
            NumberOfSeasons = numberOfSeasons;
            EpisodesPerSeason = episodesPerSeason;
            EpisodeDurationMinutes = episodeDurationMinutes;
            IsPlaying = false;
        }

        public override string GetDuration()
        {
            int totalMinutes = NumberOfSeasons * EpisodesPerSeason * EpisodeDurationMinutes;
            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;
            return $"{hours}h {minutes}m Total";
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

        //i downloabale intervention
        public long GetDownloadSize()
        {
            return NumberOfSeasons * EpisodesPerSeason * 500L * 1024 * 1024;

        }

        public string GetDownloadQuality()
        {
            return "1080p";
        }

        public void StartDownload()
        {
            AnsiConsole.WriteLine($"[green]Starting download of {Title}...[/]");
        }

        public void PauseDownload()
        {
            AnsiConsole.WriteLine($"[yellow]Pausing download of {Title}...[/]");
        }

        public void ResumeDownload()
        {
            AnsiConsole.WriteLine($"[green]Resuming download of {Title}...[/]");
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()} - {NumberOfSeasons} Seasons, {EpisodesPerSeason} Episodes per Season";

        }

    }


}