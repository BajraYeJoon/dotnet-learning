using System;
using System.Collections.Generic;

namespace CSharpLearning.Examples.Interfaces
{
    // Message archive system using interface-based programming
    public class MessageArchiveSystem
    {
        private List<IArchivableMessage> _archivedMessages = new List<IArchivableMessage>();

        public void ArchiveMessage(IArchivableMessage message)
        {
            _archivedMessages.Add(message);
            Console.WriteLine($"Archived {message.GetMessageType()} message from {message.GetSender().Name}");
        }

        public List<IArchivableMessage> GetArchivedMessages()
        {
            return [.. _archivedMessages];
        }

        public List<IArchivableMessage> SearchArchive(string keyword)
        {
            return _archivedMessages.FindAll(m => m.GetContent().Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        public List<IArchivableMessage> GetMessagesByType(string messageType)
        {
            return _archivedMessages.FindAll(m => m.GetMessageType() == messageType);
        }
    }
}