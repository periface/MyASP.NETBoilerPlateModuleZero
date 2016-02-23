using System;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace SimpleCms.ModuleZero.Notifications
{
    public interface INotificationsService : IApplicationService
    {

        Task RegisterToNotifications(long userId, int? tenantId, string notificationName);
        Task<bool> IsSuscribed(string notificationName, long userId);
        Task CheckAll(long userId);

        Task TriggerRoleCreatedNotification(long? userId, string roleName,string uniqueRoleName);
        Task TriggerRoleEditedNotification(long? userId, string roleName, string uniqueRoleName);
        Task TriggerRoleDeletedNotification(long? userId, string roleName, string uniqueRoleName);
        Task MarkAsReaded(Guid id);
    }
}
