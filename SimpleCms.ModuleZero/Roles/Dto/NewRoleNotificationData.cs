using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Notifications;

namespace SimpleCms.ModuleZero.Roles.Dto
{
    [Serializable]
    public class NewRoleNotificationData : NotificationData
    {
        public string RoleName { get; set; }
        public string CreatorName { get; set; }
    }
}
