using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class Theme : FullAuditedEntity
    {
        public string ThemeName { get; set; }
        public string ThemeDescription { get; set; }
        public string ThemePreview { get; set; }
        public string UniqueFolderId { get; set; }
        public bool IsUnderConstruction { get; set; }
        public bool IsAvailable { get; set; }
    }
}
