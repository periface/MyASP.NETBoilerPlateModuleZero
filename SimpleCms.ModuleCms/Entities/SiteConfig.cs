using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class SiteConfig : FullAuditedEntity, IMustHaveTenant
    {
        public bool IsEnabled { get; set; }
        public bool AllowUsersRegistration { get; set; }
        public int TenantId { get; set; }
    }
}
