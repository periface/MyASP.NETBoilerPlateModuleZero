using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Notifications;
using SimpleCms.ModuleZero.Notifications.Dto;

namespace SimpleCms.ModuleZero.Notifications
{
    public class NotificationsService : INotificationsService
    {
        private readonly IUserNotificationManager _userNotificationManager;
        private const string CreatedRole = "CreatedRole";
        public NotificationsService(IUserNotificationManager userNotificationManager)
        {
            _userNotificationManager = userNotificationManager;
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
                formatedMessage = string.Format(message,notification.Notification.Data.Properties["CreatorName"], notification.Notification.Data.Properties["RoleName"]);

                listOfNotifications.Add(new NotificationsOutputDto()
                {
                    Message = formatedMessage,
                    Status = notification.State.ToString()
                });
            }
            return listOfNotifications;
        }
    }
}
