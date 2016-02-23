using System.Threading.Tasks;
using Abp.Application.Services;

namespace SimpleCms.ModuleZero.Notifications
{
    public interface INotificationsService : IApplicationService
    {
        Task TriggerRoleCreatedNotification(long? userId,string roleName);
        Task RegisterToNotifications(long userId, int? tenantId,string notificationName);
        Task<bool> IsSuscribed(string notificationName, long userId);
    }
}
