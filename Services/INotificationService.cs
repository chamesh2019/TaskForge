using System;

namespace TaskForge.Services
{
    public interface INotificationService
    {
        event EventHandler<string> NotificationTriggered;
        void TriggerNotification(string message);
    }
}
