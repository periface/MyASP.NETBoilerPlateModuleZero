using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace SimpleCms.ModuleZero.Entities
{
    public class OrganiztionUnitContent : FullAuditedEntity, IMustHaveTenant
    {
        public string Lang { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public OrganizationUnit Unit { get; set; }
        public int TenantId { get; set; }
    }
}
