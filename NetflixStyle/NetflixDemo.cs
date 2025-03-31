using System;
using System.Collections.Generic;
using Spectre.Console;
using CSharpLearning.NetflixStyle.Models;
using UserProfile = CSharpLearning.NetflixStyle.Models.Profile; // Alias to avoid conflict

namespace CSharpLearning.NetflixStyle
{
    public class NetflixDemo
    {
        private static List<Content> _content = [];
        private static List<UserProfile> _profiles = new();
        private static UserProfile? _currentProfile;

        public static void RunDemo()
        {
            InitializeSampleContent();
            InitializeSampleProfiles();
            Console.WriteLine("=== Netflix-Style Content Manager Demo ===\n");

            bool keepRunning = true;
            while (keepRunning)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Netflix-Style Content Manager[/]")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Browse Content",
                            "View Profiles",
                            "Manage Watchlist",
                            "View Watch History",
                            "Exit"
                        }));

                switch (choice)
                {
                    case "Browse Content":
                        BrowseContent();
                        break;
                    case "View Profiles":
                        ViewProfiles();
                        break;
                    case "Manage Watchlist":
                        ManageWatchlist();
                        break;
                    case "View Watch History":
                        AnsiConsole.MarkupLine("[yellow]View Watch History selected - Coming soon![/]");
                        break;
                    case "Exit":
                        keepRunning = false;
                        break;
                }

                if (keepRunning)
                {
                    AnsiConsole.MarkupLine("\n[grey]Press any key to continue...[/]");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }

            Console.WriteLine("\n=== End of Netflix-Style Demo ===");
        }

        private static void InitializeSampleContent()
        {
            _content.Add(new Movie(
                "M001",
                "The Matrix",
                "A computer programmer discovers a mysterious world.",
                1999,
                ["Action", "Sci-Fi"],
                136
            ));

            _content.Add(new Series(
                "S001",
                "Stranger Things",
                "A group of kids encounter supernatural forces and secret government exploits.",
                2016,
                ["Drama", "Fantasy", "Horror"],
                4,
                8,
                50
            ));

            _content.Add(new Documentary(
                "D001",
                "Planet Earth",
                "An amazing look at nature and wildlife.",
                2006,
                ["Nature", "Educational"],
                550,
                "Nature & Wildlife"
            ));
        }

        private static void InitializeSampleProfiles()
        {
            _profiles.Add(new UserProfile("P1", "Adult Profile"));
            _profiles.Add(new UserProfile("P2", "Kids Profile", isKidsProfile: true));
            _currentProfile = _profiles[0]; // Set default profile
        }

        private static void BrowseContent()
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Title[/]")
                .AddColumn("[yellow]Year[/]")
                .AddColumn("[yellow]Description[/]")
                .AddColumn("[yellow]Duration[/]")
                .AddColumn("[yellow]Genres[/]")
                .AddColumn("[yellow]Rating[/]");

            foreach (var content in _content)
            {
                table.AddRow(
                    content.Title,
                    content.ReleaseYear.ToString(),
                    content.Description,
                    content.GetDuration(),
                    string.Join(", ", content.Genres),
                    content.Rating.ToString("0.0")
                );
            }

            AnsiConsole.Write(table);
        }

        private static void ViewProfiles()
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Name[/]")
                .AddColumn("[yellow]Type[/]")
                .AddColumn("[yellow]Watchlist Count[/]");

            foreach (var profile in _profiles)
            {
                table.AddRow(
                    profile == _currentProfile ? $"[green]{profile.Name}[/]" : profile.Name,
                    profile.IsKidsProfile ? "Kids" : "Adult",
                    profile.GetWatchlist().Count.ToString()
                );
            }

            AnsiConsole.Write(table);

            // Allow profile switching
            var profileChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Select an action[/]")
                    .AddChoices(new[] {
                        "Switch Profile",
                        "Create New Profile",
                        "Back"
                    }));

            switch (profileChoice)
            {
                case "Switch Profile":
                    SwitchProfile();
                    break;
                case "Create New Profile":
                    CreateNewProfile();
                    break;
            }
        }

        private static void SwitchProfile()
        {
            var profileNames = _profiles.Select(p => p.Name).ToArray();
            var selectedName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Select profile[/]")
                    .AddChoices(profileNames));

            _currentProfile = _profiles.First(p => p.Name == selectedName);
            AnsiConsole.WriteLine($"[green]Switched to profile: {_currentProfile.Name}[/]");
        }

        private static void CreateNewProfile()
        {
            var name = AnsiConsole.Ask<string>("[blue]Enter profile name:[/]");
            var isKids = AnsiConsole.Confirm("Is this a kids profile?");

            var profile = new UserProfile($"P{_profiles.Count + 1}", name, isKidsProfile: isKids);
            _profiles.Add(profile);
            AnsiConsole.WriteLine($"[green]Created new profile: {name}[/]");
        }


        private static void ManageWatchlist()
        {
            if (_currentProfile == null)
            {
                AnsiConsole.WriteLine("[red]Please select a profile first[/]");
                return;
            }

            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Watchlist Management[/]")
                    .AddChoices(new[] {
                        "View Watchlist",
                        "Add to Watchlist",
                        "Remove from Watchlist",
                        "Back"
                    }));

            switch (action)
            {
                case "View Watchlist":
                    DisplayWatchlist();
                    break;
                case "Add to Watchlist":
                    AddToWatchlist();
                    break;
                case "Remove from Watchlist":
                    RemoveFromWatchlist();
                    break;
            }
        }

        private static void DisplayWatchlist()
        {
            if (_currentProfile == null) return;

            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Title[/]")
                .AddColumn("[yellow]Duration[/]")
                .AddColumn("[yellow]Progress[/]");

            foreach (var content in _currentProfile.GetWatchlist())
            {
                table.AddRow(
                    content.Title,
                    content.GetDuration(),
                    $"{_currentProfile.GetWatchProgress(content)}%"
                );
            }

            AnsiConsole.Write(table);
        }
        private static void AddToWatchlist()
        {
            var content = AnsiConsole.Prompt(
                new SelectionPrompt<Content>()
                    .Title("[blue]Select content to add to watchlist[/]")
                    .AddChoices(_content));

            _currentProfile.AddToWatchlist(content);
            AnsiConsole.WriteLine($"[green]Added {content.Title} to {_currentProfile.Name}'s watchlist[/]");
        }

        private static void RemoveFromWatchlist()
        {
            var content = AnsiConsole.Prompt(
                new SelectionPrompt<Content>()
                    .Title("[blue]Select content to remove from watchlist[/]")
                    .AddChoices(_currentProfile.GetWatchlist()));

            _currentProfile.RemoveFromWatchlist(content);
            AnsiConsole.WriteLine($"[yellow]Removed {content.Title} from {_currentProfile.Name}'s watchlist[/]");
        }
    }
}

// Key OOP Concepts Used:
// - Inheritance: Content → Movie, Series, Documentary
// - Interfaces: IStreamable, IDownloadable, IRatable
// - Encapsulation: User profiles, watch history
// - Polymorphism: Different content behaviors
// - Common Pitfalls: Deep vs shallow copying for watchlists

// Core Features:
// - Browse different content types
// - Manage user profiles
// - Track watching progress
// - Create watchlists
// - Basic rating system