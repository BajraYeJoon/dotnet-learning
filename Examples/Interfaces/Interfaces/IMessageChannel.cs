using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Interfaces
{
    // Base message channel interface - demonstrates contract-based programming
    public interface IMessageChannel
    {
        string Name { get; }
        string Description { get; }
        void SendMessage(User sender, string content);
        List<Message> GetMessages();
        string GetChannelInfo();
    }

    // Extended message channel interface - demonstrates interface inheritance
    public interface IAdvancedMessageChannel : IMessageChannel
    {
        void SendPriorityMessage(User sender, string content, MessagePriority priority);
        void EditMessage(string messageId, string newContent);
        void PinMessage(string messageId);
        void DeleteMessage(string messageId);
    }

    // Meeting capability interface - demonstrates interface segregation
    public interface IMeetingCapable
    {
        Meeting ScheduleMeeting(string title, DateTime startTime, TimeSpan duration, User organizer);
        void CancelMeeting(string meetingId);
        List<Meeting> GetUpcomingMeetings();
    }

    // File sharing capability interface - demonstrates interface segregation
    public interface IFileSharingCapable
    {
        void ShareFile(User uploader, string fileName, long fileSize, string mimeType);
        List<SharedFile> GetSharedFiles();
        void DeleteFile(string fileId);
    }

    // Message formatting interface with default implementation
    public interface IFormattableMessage
    {
        string GetContent();

        // Default interface method (C# 8.0+)
        string FormatMessage()
        {
            string content = GetContent();

            // Simple Markdown-like formatting
            content = content.Replace("*", "**"); // Bold
            content = content.Replace("_", "_");  // Italic

            return content;
        }
    }

    // Archivable message interface
    public interface IArchivableMessage
    {
        string GetContent();
        User GetSender();
        DateTime GetTimestamp();
        string GetMessageType();
        string GetArchiveMetadata();
    }
}