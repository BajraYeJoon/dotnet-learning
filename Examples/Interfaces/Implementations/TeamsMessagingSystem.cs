using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Interfaces
{
    // Main messaging system class
    public class TeamsMessagingSystem
    {
        private string _organizationName;
        private Dictionary<string, User> _users = new Dictionary<string, User>();
        private List<IMessageChannel> _channels = new List<IMessageChannel>();

        public TeamsMessagingSystem(string organizationName)
        {
            _organizationName = organizationName;
            Console.WriteLine($"Teams messaging system initialized for {_organizationName}");
        }

        public void AddUser(User user)
        {
            if (!_users.ContainsKey(user.UserId))
            {
                _users.Add(user.UserId, user);
                Console.WriteLine($"User added: {user.Name}");
            }
        }

        public void AddChannel(IMessageChannel channel)
        {
            _channels.Add(channel);
            Console.WriteLine($"Channel added: {channel.Name}");
        }

        public User GetUser(string userId)
        {
            if (_users.TryGetValue(userId, out User user))
            {
                return user;
            }
            return null;
        }

        public IMessageChannel GetChannel(string channelName)
        {
            return _channels.Find(c => c.Name == channelName);
        }

        public List<IMessageChannel> GetAllChannels()
        {
            return new List<IMessageChannel>(_channels);
        }
    }
}