using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus.Handlers;
using Abp.Notifications;
using SimpleCms.ModuleZero.Notifications.Dto;
using SimpleCms.ModuleZero.Roles.Dto;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero.Notifications
{
    public class NotificationsService : INotificationsService
    {
        private readonly IUserNotificationManager _userNotificationManager;
        private readonly IRealTimeNotifier _realTimeNotifier;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly UserManager _userManager;
        private const string CreatedRole = "CreatedRole";
        public NotificationsService(IUserNotificationManager userNotificationManager, IRealTimeNotifier realTimeNotifier, INotificationPublisher notificationPublisher, UserManager userManager)
        {
            _userNotificationManager = userNotificationManager;
            _realTimeNotifier = realTimeNotifier;
            _notificationPublisher = notificationPublisher;
            _userManager = userManager;
        }

        public async Task<IEnumerable<NotificationsOutputDto>> GetNotifications(long userId)
        {
            var notifications = await _userNotificationManager.GetUserNotificationsAsync(userId);
            var listOfNotifications = new List<NotificationsOutputDto>();
            foreach (var notification in notifications)
            {
                var message = "";
                var formatedMessage = "";
                if (notification.Notification.NotificationName == CreatedRole)
                {
                    message = "{0} created a new role:{1}";
                }
                formatedMessage = string.Format(message, notification.Notification.Data.Properties["CreatorName"], notification.Notification.Data.Properties["RoleName"]);

                listOfNotifications.Add(new NotificationsOutputDto()
                {
                    Message = formatedMessage,
                    Status = notification.State.ToString()
                });
            }
            return listOfNotifications;
        }

        public async Task RoleCreatedNotification(long? userId, string roleName)
        {
            var user = new User();
            if (userId != null)
            {
                user = await _userManager.GetUserByIdAsync((long)userId);
            }
            await _notificationPublisher.PublishAsync("CreatedRole", new NewRoleNotificationData()
            {
                RoleName = roleName,
                CreatorName = user.UserName
            });
            if (userId != null)
            {
                //var notifications = await _userNotificationManager.GetUserNotificationsAsync((long)userId);
                //await _realTimeNotifier.SendNotificationsAsync(new UserNotification[]
                //{
                //  notifications.First()
                //});
            }
        }
    }
}
