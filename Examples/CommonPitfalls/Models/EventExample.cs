using System;

namespace CSharpLearning.Examples.CommonPitfalls
{
    // Event publisher class
    public class EventPublisher
    {
        // Event that can cause memory leaks if subscribers don't unsubscribe
        public event EventHandler<string> SampleEvent;

        public void RaiseEvent(string message)
        {
            Console.WriteLine($"Publisher: Raising event with message: {message}");
            SampleEvent?.Invoke(this, message);
        }
    }

    // Event subscriber class
    public class EventSubscriber
    {
        public string Name { get; }

        public EventSubscriber(string name)
        {
            Name = name;
        }

        public void Subscribe(EventPublisher publisher)
        {
            publisher.SampleEvent += HandleEvent;
            Console.WriteLine($"{Name}: Subscribed to event");
        }

        public void Unsubscribe(EventPublisher publisher)
        {
            publisher.SampleEvent -= HandleEvent;
            Console.WriteLine($"{Name}: Unsubscribed from event");
        }

        private void HandleEvent(object sender, string message)
        {
            Console.WriteLine($"{Name}: Received event with message: {message}");
        }
    }

    // Resource manager to demonstrate proper disposal
    public class ResourceManager : IDisposable
    {
        private bool _disposed = false;
        public string ResourceName { get; }

        public ResourceManager(string resourceName)
        {
            ResourceName = resourceName;
            Console.WriteLine($"Resource '{ResourceName}' acquired");
        }

        public void PerformOperation()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ResourceManager));

            Console.WriteLine($"Performing operation with resource '{ResourceName}'");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free managed resources
                    Console.WriteLine($"Releasing managed resources for '{ResourceName}'");
                }

                // Free unmanaged resources
                Console.WriteLine($"Releasing unmanaged resources for '{ResourceName}'");
                _disposed = true;
            }
        }

        ~ResourceManager()
        {
            Dispose(false);
        }
    }
}