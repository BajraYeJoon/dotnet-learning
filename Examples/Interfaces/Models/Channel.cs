using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLearning.Examples.Interfaces
{
    // Team channel implementation
    public class TeamChannel : IMessageChannel
    {
        private List<Message> _messages = new List<Message>();

        public string Name { get; }
        public string Description { get; }

        public TeamChannel(string name, string description)
        {
            Name = name;
            Description = description;
            Console.WriteLine($"Team channel created: {Name}");
        }

        public void SendMessage(User sender, string content)
        {
            TextMessage message = new TextMessage(sender, content);
            _messages.Add(message);
            Console.WriteLine($"Message sent to {Name} channel: {message}");
        }

        public List<Message> GetMessages()
        {
            return new List<Message>(_messages);
        }

        public string GetChannelInfo()
        {
            return $"Team Channel: {Name} - {Description} ({_messages.Count} messages)";
        }
    }

    // Direct message channel implementation
    public class DirectMessageChannel : IMessageChannel
    {
        private List<Message> _messages = new List<Message>();
        private User _user1;
        private User _user2;

        public string Name => $"DM: {_user1.Name} and {_user2.Name}";
        public string Description => "Direct message conversation";

        public DirectMessageChannel(User user1, User user2)
        {
            _user1 = user1;
            _user2 = user2;
            Console.WriteLine($"Direct message channel created between {_user1.Name} and {_user2.Name}");
        }

        public void SendMessage(User sender, string content)
        {
            // Validate sender is one of the participants
            if (sender.UserId != _user1.UserId && sender.UserId != _user2.UserId)
            {
                throw new InvalidOperationException("Only participants can send messages in a direct channel");
            }

            TextMessage message = new TextMessage(sender, content);
            _messages.Add(message);

            User recipient = (sender.UserId == _user1.UserId) ? _user2 : _user1;
            Console.WriteLine($"Direct message sent to {recipient.Name}: {message}");
        }

        public List<Message> GetMessages()
        {
            return new List<Message>(_messages);
        }

        public string GetChannelInfo()
        {
            return $"Direct Message: {_user1.Name} and {_user2.Name} ({_messages.Count} messages)";
        }
    }

    // Advanced team channel implementation
    public class AdvancedTeamChannel : IAdvancedMessageChannel
    {
        private List<Message> _messages = new List<Message>();
        private List<string> _pinnedMessageIds = new List<string>();

        public string Name { get; }
        public string Description { get; }

        public AdvancedTeamChannel(string name, string description)
        {
            Name = name;
            Description = description;
            Console.WriteLine($"Advanced team channel created: {Name}");
        }

        public void SendMessage(User sender, string content)
        {
            TextMessage message = new TextMessage(sender, content);
            _messages.Add(message);
            Console.WriteLine($"Message sent to {Name} channel: {message}");
        }

        public void SendPriorityMessage(User sender, string content, MessagePriority priority)
        {
            TextMessage message = new TextMessage(sender, content);
            _messages.Add(message);
            Console.WriteLine($"{priority} priority message sent to {Name} channel: {message}");
        }

        public void EditMessage(string messageId, string newContent)
        {
            var message = _messages.FirstOrDefault(m => m.MessageId == messageId) as TextMessage;
            if (message != null)
            {
                message.EditContent(newContent);
                Console.WriteLine($"Message {messageId} edited in {Name} channel");
            }
            else
            {
                Console.WriteLine($"Message {messageId} not found in {Name} channel");
            }
        }

        public void PinMessage(string messageId)
        {
            if (_messages.Any(m => m.MessageId == messageId) && !_pinnedMessageIds.Contains(messageId))
            {
                _pinnedMessageIds.Add(messageId);
                Console.WriteLine($"Message {messageId} pinned in {Name} channel");
            }
            else
            {
                Console.WriteLine($"Cannot pin message {messageId} in {Name} channel");
            }
        }

        public void DeleteMessage(string messageId)
        {
            var message = _messages.FirstOrDefault(m => m.MessageId == messageId);
            if (message != null)
            {
                _messages.Remove(message);
                _pinnedMessageIds.Remove(messageId);
                Console.WriteLine($"Message {messageId} deleted from {Name} channel");
            }
            else
            {
                Console.WriteLine($"Message {messageId} not found in {Name} channel");
            }
        }

        public List<Message> GetMessages()
        {
            return new List<Message>(_messages);
        }

        public string GetChannelInfo()
        {
            return $"Advanced Channel: {Name} - {Description} ({_messages.Count} messages, {_pinnedMessageIds.Count} pinned)";
        }
    }

    // Meeting channel implementation - demonstrates multiple interface implementation
    public class MeetingChannel : IMessageChannel, IMeetingCapable, IFileSharingCapable
    {
        private List<Message> _messages = new List<Message>();
        private List<Meeting> _meetings = new List<Meeting>();
        private List<SharedFile> _files = new List<SharedFile>();

        public string Name { get; }
        public string Description { get; }

        public MeetingChannel(string name, string description)
        {
            Name = name;
            Description = description;
            Console.WriteLine($"Meeting channel created: {Name}");
        }

        // IMessageChannel implementation
        public void SendMessage(User sender, string content)
        {
            TextMessage message = new TextMessage(sender, content);
            _messages.Add(message);
            Console.WriteLine($"Message sent to {Name} meeting channel: {message}");
        }

        public List<Message> GetMessages()
        {
            return new List<Message>(_messages);
        }

        public string GetChannelInfo()
        {
            return $"Meeting Channel: {Name} - {Description} ({_messages.Count} messages, {_meetings.Count} meetings)";
        }

        // IMeetingCapable implementation
        public Meeting ScheduleMeeting(string title, DateTime startTime, TimeSpan duration, User organizer)
        {
            Meeting meeting = new Meeting(title, startTime, duration, organizer);
            _meetings.Add(meeting);
            Console.WriteLine($"Meeting scheduled in {Name} channel: {meeting.Title} at {meeting.StartTime:g}");
            return meeting;
        }

        public void CancelMeeting(string meetingId)
        {
            var meeting = _meetings.FirstOrDefault(m => m.MeetingId == meetingId);
            if (meeting != null)
            {
                meeting.Cancel();
                Console.WriteLine($"Meeting {meetingId} cancelled in {Name} channel");
            }
            else
            {
                Console.WriteLine($"Meeting {meetingId} not found in {Name} channel");
            }
        }

        public List<Meeting> GetUpcomingMeetings()
        {
            return _meetings.Where(m => m.StartTime > DateTime.Now && !m.IsCancelled).ToList();
        }

        // IFileSharingCapable implementation
        public void ShareFile(User uploader, string fileName, long fileSize, string mimeType)
        {
            SharedFile file = new SharedFile(uploader, fileName, fileSize, mimeType);
            _files.Add(file);
            Console.WriteLine($"File shared in {Name} channel: {fileName} ({file.FormattedSize})");

            // Also add a message about the file
            FileMessage fileMessage = new FileMessage(uploader, fileName, fileSize, mimeType);
            _messages.Add(fileMessage);
        }

        public List<SharedFile> GetSharedFiles()
        {
            return new List<SharedFile>(_files);
        }

        public void DeleteFile(string fileId)
        {
            var file = _files.FirstOrDefault(f => f.FileId == fileId);
            if (file != null)
            {
                _files.Remove(file);
                Console.WriteLine($"File {fileId} deleted from {Name} channel");
            }
            else
            {
                Console.WriteLine($"File {fileId} not found in {Name} channel");
            }
        }
    }
}