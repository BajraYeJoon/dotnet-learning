using System;

namespace CSharpLearning.Examples.Interfaces
{
    // Message service with dependency injection
    public class MessageService
    {
        private readonly IMessageFormatter _formatter;

        // Constructor injection
        public MessageService(IMessageFormatter formatter)
        {
            _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public void FormatAndDisplayMessage(string content)
        {
            // Use the injected formatter
            string formattedContent = _formatter.Format(content);
            Console.WriteLine(formattedContent);
        }
    }
}