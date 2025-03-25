using System;

namespace CSharpLearning.Examples.Interfaces
{
    // Interface-based message processor
    public class RegularMessageProcessor : IMessageProcessor
    {
        public void ProcessMessage(Message message)
        {
            if (CanProcessMessage(message))
            {
                Console.WriteLine($"Processing message via interface: {message}");
                // Actual processing logic would go here
            }
            else
            {
                Console.WriteLine($"Cannot process message: {message.MessageId}");
            }
        }

        public bool CanProcessMessage(Message message)
        {
            // Simple validation logic
            return message != null && message.GetMessageType() == "Text";
        }
    }

    // Abstract class-based message processor
    public abstract class MessageProcessorBase
    {
        // Template method pattern
        public void ProcessMessage(Message message)
        {
            if (ValidateMessage(message))
            {
                Console.WriteLine($"Pre-processing message via abstract class: {message}");
                DoProcessMessage(message);
                Console.WriteLine("Post-processing complete");
            }
            else
            {
                Console.WriteLine($"Message validation failed: {message.MessageId}");
            }
        }

        // Common validation logic
        protected virtual bool ValidateMessage(Message message)
        {
            return message != null;
        }

        // Abstract method to be implemented by derived classes
        protected abstract void DoProcessMessage(Message message);
    }

    // Concrete implementation of abstract message processor
    public class PriorityMessageProcessor : MessageProcessorBase
    {
        protected override void DoProcessMessage(Message message)
        {
            Console.WriteLine($"Processing message with priority handling: {message}");
            // Priority-specific processing logic would go here
        }

        // Override validation to add additional checks
        protected override bool ValidateMessage(Message message)
        {
            return base.ValidateMessage(message) && message.Sender != null;
        }
    }
}