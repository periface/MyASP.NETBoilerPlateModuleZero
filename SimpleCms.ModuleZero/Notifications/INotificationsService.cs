using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleZero.Notifications.Dto;

namespace SimpleCms.ModuleZero.Notifications
{
    public interface INotificationsService : IApplicationService
    {
        Task<IEnumerable<NotificationsOutputDto>> GetNotifications(long userId);
    }
}
