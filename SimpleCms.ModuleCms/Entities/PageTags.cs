using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class PageTags : FullAuditedEntity , IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Tag { get; set; }
        public virtual ICollection<Page> Pages { get; set; }  
    }
}
