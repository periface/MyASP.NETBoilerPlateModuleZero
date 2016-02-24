using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Notifications;

namespace NotificationSystem.Notifications
{
    public interface INotificationsService : IApplicationService
    {

        Task RegisterToNotifications(long userId, int? tenantId, string notificationName);
        Task<bool> IsSuscribed(string notificationName, long userId);
        Task CheckAll(long userId);
        /// <summary>
        /// Sends a generic localizable message notification
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="data"></param>
        /// <param name="severity"></param>
        /// <returns></returns>
        Task TriggerGenericNotification(string notificationName, NotificationData data, NotificationSeverity severity);
        Task MarkAsReaded(Guid id);
        Task UnRegisterToNotifications(long userId, string serviceName);
    }
}
