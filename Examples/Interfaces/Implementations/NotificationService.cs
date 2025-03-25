using System;

namespace CSharpLearning.Examples.Interfaces
{
    // Notification service with explicit interface implementation
    public class NotificationService : IEmailNotifier, IPushNotifier, IDesktopNotifier
    {
        // Explicit implementation of IEmailNotifier
        void IEmailNotifier.SendEmailNotification(User recipient, string subject, string body)
        {
            Console.WriteLine($"EMAIL NOTIFICATION to {recipient.Email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine();
        }

        // Explicit implementation of IPushNotifier
        void IPushNotifier.SendPushNotification(User recipient, string title, string message)
        {
            Console.WriteLine($"PUSH NOTIFICATION to {recipient.Name}'s device");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine();
        }

        // Explicit implementation of IDesktopNotifier
        void IDesktopNotifier.ShowDesktopAlert(User recipient, string title, string message)
        {
            Console.WriteLine($"DESKTOP ALERT for {recipient.Name}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Message: {message}");
            Console.WriteLine();
        }
    }
}