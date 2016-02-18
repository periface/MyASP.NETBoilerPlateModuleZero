using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class Menu : FullAuditedEntity ,IMustHaveTenant
    {
        public string MenuTitle { get; set; }
        public int MenuOrder { get; set; }
        public string Url { get; set; }
        public int TenantId { get; set; }
        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Menu ParentMenu { get; set; }
        public virtual ICollection<Menu> ChildMenus { get; set; } 
    }
}
