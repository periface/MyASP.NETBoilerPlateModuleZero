using System.Threading.Tasks;
using Abp.Notifications;

namespace NotificationSystem.Notifications
{
    /// <summary>
    /// Must be resolved in application service base.
    /// This adds simplicity to the notification service 
    /// </summary>
    /// <typeparam name="TNotificationTypeData"></typeparam>
    public interface INotificable<in TNotificationTypeData>
    {
        Task SendNotification(TNotificationTypeData data, string notificationName, NotificationSeverity severity);
    }
}
