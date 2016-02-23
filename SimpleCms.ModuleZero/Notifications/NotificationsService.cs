using System.Threading.Tasks;
using Abp.Notifications;
using SimpleCms.ModuleZero.Constants;
using SimpleCms.ModuleZero.Roles.Dto;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Notifications
{
    public class NotificationsService : INotificationsService
    {
        private readonly INotificationPublisher _notificationPublisher;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly UserManager _userManager;
        private readonly IUserNotificationManager _userNotificationManager;
        public NotificationsService(INotificationPublisher notificationPublisher, UserManager userManager, INotificationSubscriptionManager notificationSubscriptionManager, IUserNotificationManager userNotificationManager)
        {
            _notificationPublisher = notificationPublisher;
            _userManager = userManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _userNotificationManager = userNotificationManager;
        }

        public async Task TriggerRoleCreatedNotification(long? userId, string roleName)
        {
            var user = new User();
            if (userId != null)
            {
                user = await _userManager.GetUserByIdAsync((long)userId);
            }
            await _notificationPublisher.PublishAsync(ModuleZeroConstants.CreatedRoleNotificationName, new NewRoleNotificationData()
            {
                RoleName = roleName,
                CreatorName = user.UserName
            });
            //if (userId != null)
            //{
            //var notifications = await _userNotificationManager.GetUserNotificationsAsync((long)userId);
            //await _realTimeNotifier.SendNotificationsAsync(new UserNotification[]
            //{
            //  notifications.First()
            //});
            //}
        }

        public async Task RegisterToNotifications(long userId, int? tenantId,string notificationName)
        {
            await _notificationSubscriptionManager.SubscribeAsync(tenantId, userId, notificationName);
        }

        public async Task CheckAll(long userId)
        {
          await _userNotificationManager.UpdateAllUserNotificationStatesAsync(userId,UserNotificationState.Read);
        }
        public async Task<bool> IsSuscribed(string notificationName, long userId)
        {
            return await _notificationSubscriptionManager.IsSubscribedAsync(userId, notificationName);
        }
    }
}
