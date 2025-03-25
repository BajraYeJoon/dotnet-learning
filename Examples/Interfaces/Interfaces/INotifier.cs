using System;

namespace CSharpLearning.Examples.Interfaces
{
    // Email notification interface
    public interface IEmailNotifier
    {
        void SendEmailNotification(User recipient, string subject, string body);
    }

    // Push notification interface
    public interface IPushNotifier
    {
        void SendPushNotification(User recipient, string title, string message);
    }

    // Desktop notification interface
    public interface IDesktopNotifier
    {
        void ShowDesktopAlert(User recipient, string title, string message);
    }
}