using EduHub.Application.Interfaces;

namespace EduHub.Infrastructure.Notifications;

public static class NotificationFactory
{
    public static INotificationService Create(string channel)
    {
        return channel.ToLowerInvariant() switch
        {
            "email" => new EmailNotificationService(),
            "sms" => new SmsNotificationService(),
            _ => throw new ArgumentException($"Unsupported channel: {channel}", nameof(channel))
        };
    }
}