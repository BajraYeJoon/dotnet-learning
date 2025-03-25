
namespace CSharpLearning.Examples.Interfaces
{
    // Message processor interface - for comparison with abstract class
    public interface IMessageProcessor
    {
        void ProcessMessage(Message message);
        bool CanProcessMessage(Message message);
    }

    // Message formatter interface - for dependency injection
    public interface IMessageFormatter
    {
        string Format(string content);
    }
}