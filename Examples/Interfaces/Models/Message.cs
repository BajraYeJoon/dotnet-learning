using System;

namespace CSharpLearning.Examples.Interfaces
{
    // Base message class
    public abstract class Message
    {
        private static int _messageCounter = 1000;

        public string MessageId { get; }
        public User Sender { get; }
        public DateTime Timestamp { get; }
        public bool IsEdited { get; protected set; }

        protected Message(User sender)
        {
            MessageId = $"MSG{++_messageCounter}";
            Sender = sender;
            Timestamp = DateTime.Now;
            IsEdited = false;
        }

        public abstract string GetContent();
        public abstract string GetMessageType();

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {Sender.Name}: {GetContent()}" + (IsEdited ? " (edited)" : "");
        }
    }

    // Message priority enum
    public enum MessagePriority
    {
        Low,
        Normal,
        High,
        Urgent
    }

    // Text message implementation
    public class TextMessage : Message, IFormattableMessage, IArchivableMessage
    {
        private string _content;

        public TextMessage(User sender, string content) : base(sender)
        {
            _content = content;
        }

        public override string GetContent()
        {
            return _content;
        }

        public void EditContent(string newContent)
        {
            _content = newContent;
            IsEdited = true;
        }

        public override string GetMessageType()
        {
            return "Text";
        }

        public string GetArchiveMetadata()
        {
            return $"Text message from {Sender.Name} at {Timestamp:yyyy-MM-dd HH:mm:ss}";
        }

        DateTime IArchivableMessage.GetTimestamp()
        {
            return Timestamp;
        }

        User IArchivableMessage.GetSender()
        {
            return Sender;
        }
    }

    // Code message implementation with custom formatting
    public class CodeMessage : Message, IFormattableMessage, IArchivableMessage
    {
        private string _code;

        public CodeMessage(User sender, string code) : base(sender)
        {
            _code = code;
        }

        public override string GetContent()
        {
            return _code;
        }

        public override string GetMessageType()
        {
            return "Code";
        }

        // Override the default interface method
        public string FormatMessage()
        {
            return $"```\n{_code}\n```";
        }

        public string GetArchiveMetadata()
        {
            return $"Code snippet from {Sender.Name} at {Timestamp:yyyy-MM-dd HH:mm:ss}";
        }

        DateTime IArchivableMessage.GetTimestamp()
        {
            return Timestamp;
        }

        User IArchivableMessage.GetSender()
        {
            return Sender;
        }
    }

    // File message implementation
    public class FileMessage : Message, IArchivableMessage
    {
        public string FileName { get; }
        public long FileSize { get; }
        public string MimeType { get; }

        public FileMessage(User sender, string fileName, long fileSize, string mimeType) : base(sender)
        {
            FileName = fileName;
            FileSize = fileSize;
            MimeType = mimeType;
        }

        public override string GetContent()
        {
            return $"File: {FileName} ({FormatFileSize(FileSize)})";
        }

        public override string GetMessageType()
        {
            return "File";
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        public string GetArchiveMetadata()
        {
            return $"File '{FileName}' ({MimeType}) uploaded by {Sender.Name} at {Timestamp:yyyy-MM-dd HH:mm:ss}";
        }

        DateTime IArchivableMessage.GetTimestamp()
        {
            return Timestamp;
        }

        User IArchivableMessage.GetSender()
        {
            return Sender;
        }
    }
}