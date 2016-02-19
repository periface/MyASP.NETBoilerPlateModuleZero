using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    
    public class CategoryContent :FullAuditedEntity, IMustHaveTenant 
    {
        public string Lang { get; set; }
        public string CategoryName { get; set; }
        public string FriendlyUrl { get; set; }
        public virtual PageCategory Category { get; set; }
        public int TenantId { get; set; }
    }
}
