using System.Collections.Generic;
using System.Text.RegularExpressions;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class Page : FullAuditedEntity<int> , IMustHaveTenant
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<PageTags> Tags { get; set; }
        public virtual PageCategory PageCategory { get; set; }
        public virtual  ICollection<PageContent> Content { get; set; } 
        
    }
}
