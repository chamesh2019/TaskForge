using System;

namespace TaskForge.Services
{
    public class NotificationService : INotificationService
    {
        public event EventHandler<string>? NotificationTriggered;

        public void TriggerNotification(string message)
        {
            NotificationTriggered?.Invoke(this, message);
        }
    }
}
