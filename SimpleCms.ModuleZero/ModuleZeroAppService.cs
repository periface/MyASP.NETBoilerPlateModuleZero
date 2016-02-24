using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.IdentityFramework;
using Abp.Notifications;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using NotificationSystem.Notifications;
using SimpleCms.MultiTenancy;
using SimpleCms.Users;

namespace SimpleCms.ModuleZero
{
    public class ModuleZeroAppService : ApplicationService , INotificable<LocalizableMessageNotificationData>
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }
        private readonly INotificationsService _notificationsService;

        protected ModuleZeroAppService()
        {
            var manager = IocManager.Instance;
            _notificationsService = manager.Resolve<INotificationsService>();
            LocalizationSourceName = SimpleCmsConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task SendNotification(LocalizableMessageNotificationData data, string notificationName,
            NotificationSeverity severity)
        {
            await _notificationsService.TriggerGenericNotification(notificationName,data,severity);
        }
    }
}
