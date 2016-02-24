using System;
using System.Threading.Tasks;
using Abp.Notifications;

namespace NotificationSystem.Notifications
{
    public class NotificationsService :  INotificationsService
    {
        private readonly INotificationPublisher _notificationPublisher;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IUserNotificationManager _userNotificationManager;
        public NotificationsService(INotificationPublisher notificationPublisher, INotificationSubscriptionManager notificationSubscriptionManager, IUserNotificationManager userNotificationManager)
        {
            _notificationPublisher = notificationPublisher;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _userNotificationManager = userNotificationManager;
        }
        public async Task TriggerGenericNotification(string notificationName,NotificationData data, NotificationSeverity severity)
        {

            await _notificationPublisher.PublishAsync(notificationName, data, severity: severity);
        }

        public async Task MarkAsReaded(Guid id)
        {
            await _userNotificationManager.UpdateUserNotificationStateAsync(id, UserNotificationState.Read);
        }

        public async Task UnRegisterToNotifications(long userId,string serviceName)
        {
            await _notificationSubscriptionManager.UnsubscribeAsync(userId, serviceName);
        }

        public async Task RegisterToNotifications(long userId, int? tenantId, string notificationName)
        {
            await _notificationSubscriptionManager.SubscribeAsync(tenantId, userId, notificationName);
        }

        public async Task CheckAll(long userId)
        {
            await _userNotificationManager.UpdateAllUserNotificationStatesAsync(userId, UserNotificationState.Read);
        }
        public async Task<bool> IsSuscribed(string notificationName, long userId)
        {
            return await _notificationSubscriptionManager.IsSubscribedAsync(userId, notificationName);
        }
    }
}
