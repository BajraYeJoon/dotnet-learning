using System;

namespace CSharpLearning.Examples.Interfaces
{
    // User class
    public class User
    {
        public string UserId { get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public DateTime LastActive { get; private set; }

        public User(string userId, string name, string email)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Status = UserStatus.Available;
            LastActive = DateTime.Now;
        }

        public void UpdateActivity()
        {
            LastActive = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Name} ({Email})";
        }
    }

    // User status enum
    public enum UserStatus
    {
        Available,
        Busy,
        DoNotDisturb,
        Away,
        Offline
    }
}