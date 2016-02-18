using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleZero.Entities
{
    public class CustomSiteConfigProperties : FullAuditedEntity ,IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual ModuleZeroSiteConfiguration Configuration { get; set; }
        public virtual string HashValue()
        {
            return "";
        }

        public virtual string DeHashValue()
        {
            return "";
        }
        public virtual string HashValue(string code)
        {
            return "";
        }

        public virtual string DeHashValue(string code)
        {
            return "";
        }

    }
}
