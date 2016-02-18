using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class SiteAdress : FullAuditedEntity, IMustHaveTenant
    {
        public string AdressL1 { get; set; }
        public string AdressL2 { get; set; }
        public string PhoneNumber { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
    }
}
