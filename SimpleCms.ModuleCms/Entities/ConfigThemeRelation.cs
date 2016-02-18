using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class ConfigThemeRelation : FullAuditedEntity, IMustHaveTenant
    {
        public int IdConfig { get; set; }
        [ForeignKey("IdConfig")]
        public virtual SiteConfig Config { get; set; }
        public int IdTheme { get; set; }
        [ForeignKey("IdTheme")]
        public virtual Theme SiteTheme { get; set; }
        public bool IsActive { get; set; }
        public int TenantId { get; set; }
    }
}
